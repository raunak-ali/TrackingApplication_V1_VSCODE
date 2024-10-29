import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AddTask, priority } from 'src/app/Models/add-task';
import { GetTask } from 'src/app/Models/get-task';
import { GetUser } from 'src/app/Models/get-user';
import { User } from 'src/app/Models/user';
import { AddEmployeesToBatchService } from 'src/app/Services/add-employees-to-batch.service';
import { AddnewTaskService } from 'src/app/Services/addnew-task.service';
import { GetAllEmployeesService } from 'src/app/Services/get-all-employees.service';
import { GetBatchesService } from 'src/app/Services/get-batches.service';
import { GetTasksService } from 'src/app/Services/get-tasks.service';
import { GetUserByBatchService } from 'src/app/Services/get-user-by-batch.service';
import { LoginService } from 'src/app/Services/login.service';
import { NavigationService } from 'src/app/Services/navigation.service';
import { RemoveEmployeesFromBatchService } from 'src/app/Services/remove-employees-from-batch.service';


@Component({
  selector: 'app-batch-dashboard',
  templateUrl: './batch-dashboard.component.html',
  styleUrls: ['./batch-dashboard.component.css']
})
export class BatchDashboardComponent implements OnInit {


showEmployee: boolean=false;



  @ViewChild('assignedTo')assignedToSelect!: ElementRef;
  priorities :string[]= Object.keys(priority).filter(key => !isNaN(Number(priority[key as keyof typeof priority]))) as string[];
  batchId!: number;
  moduleId!:number;
  AllTasks:GetTask[]=[];
  AllEmployyes!:GetUser[];
  AddNewTaskForm!: FormGroup;
  getMentorID!: any;
  showForm: boolean = false;
  addTask!:AddTask;
  isDropdownOpen: boolean = false;
  currentUser = this.loginservice.getUserFromSession();
  FetchedEmployees!: User[];
  USeExistingUser: boolean=false;
  AddUserToBatchForm!: FormGroup;
AddnewEmpoyee: any;
  navigationHistory: string[]=[];
  navigateTo(route: string): void {
    this.router.navigate([route]);
  }
  showNavigationHistory: boolean = false;

toggleNavigationHistory() {
  this.showNavigationHistory = !this.showNavigationHistory;
}
getLinkName(link: string): string {
  // Customize this function to format the link name as needed
  // For example, you might want to remove slashes or decode URL parts
  return link.replace(/\//g, ' ').trim();
}
  logout(): void {
    this.loginservice.clearUser();
    this.loginservice.clearToken();
    this.loginservice.clearcurrentUser();
    this.router.navigate(["Login"]);
    // Implement logout functionality
  }

  toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }


  constructor(private route: ActivatedRoute,
    private gettasksservice:GetTasksService,
    private getUserByBatchService:GetUserByBatchService,
    private fb: FormBuilder,
    private loginservice :LoginService,
    private addnewTaskService:AddnewTaskService,
    private router: Router,
    private getAllEmployees:GetAllEmployeesService,
    private addnewemployeetoBatch:AddEmployeesToBatchService,
    private removeEmployeesFromBatchService:RemoveEmployeesFromBatchService,
    private snackBar: MatSnackBar,
    private navigationService: NavigationService
    ) { }
    AddMentorForm!: FormGroup;
  ngOnInit(): void {
    this.navigationHistory = this.navigationService.getNavigationHistory();

    this.GetAllEmployees();
    console.log(this.currentUser);

    // Retrieve the batchId parameter from the route
    this.batchId = +this.route.snapshot.params['batchId'];
    this.moduleId = +this.route.snapshot.params['moduleId'];
    this.getMentorID=this.loginservice.getUser();
    if (this.getMentorID != null) {
      // Check if this.existing_mentor is a valid number string
      this.getMentorID = Number(this.getMentorID);
      if (!isNaN(this.getMentorID)) {
        this.getMentorID=this.getMentorID;
      }
    }
    this.AddUserToBatchForm=this.fb.group({
      EmplloyeesToAdd:['']
    })
    this.AddMentorForm = this.fb.group({
      name: ['', Validators.required], // Required field
      role: [0],
      domain: ['', Validators.required], // Required field
      jobTitle: [''],
      location: [''],
      phone: [''],
      isCr: [false],
      gender: [''],
      doj: [new Date(), Validators.required], // Required field
      capgeminiEmailId: ['', [Validators.required, Validators.email]], // Required and valid email
      grade: [''],
      totalAverageRatingStatus: [0],
      personalEmailId: [''],
      earlierMentorName: ['None'],
      finalMentorName: ['None'],
      attendanceCount: [0],
      batches: [null]
    });
    if (this.assignedToSelect) {
      // Apply the custom logic
      this.assignedToSelect.nativeElement.addEventListener('change', (event: { target: HTMLOptionElement; }) => {
        const selectedOption = event.target as HTMLOptionElement;
        const parentLabel = selectedOption.parentElement?.parentElement;
        if (parentLabel) {
          parentLabel.classList.add('selected-optgroup');
        }
      });
    }


    this.FetchTasks();
    this. GetEmployeeByBatch();
    this.AddNewTaskForm = this.fb.group({
      UserTaskID:[1],
      TaskName: ['',Validators.required],
      Description: ['',Validators.required],
      Priority: ['', Validators.required],
      DeadLine: ['', [Validators.required, this.deadlineGreaterThanToday()]],
      Status: [0],
      AssignedBy: [this.getMentorID],
      AssignedTo: [[], Validators.required],
      BatchId: [this.batchId],
      Comments: [''],
      ModuleId:[this.moduleId]
    })

    // Now you can use this.batchId in your component logic
    console.log('Batch ID:', this.batchId);
  }
  deadlineGreaterThanToday(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const today = new Date();
      const deadlineDate = new Date(control.value);

      if (deadlineDate <= today) {
        return { deadlineNotGreaterThanToday: true };
      }
      return null;
    };
  }
  FetchTasks(){
    this.gettasksservice.Getall(this.moduleId).subscribe(
      (data: any) => {
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.AllTasks = data.$values;
          this.AllTasks=this.AllTasks.sort((a, b) => b.priority - a.priority);
          console.log('ALLTAsks',this.AllTasks);
        } else {
          console.error('Unexpected data format:', data);
          this.snackBar.open(`Error fetching batches:: ${{data}}`, 'Close', { duration: 3000 });
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
        this.snackBar.open(`Error fetching batches:: ${{error}}`, 'Close', { duration: 3000 });
      }
    );
  }
  AddNewTask(){
    // Get the selected user IDs from the form control
    const selectedUserIds = this.AddNewTaskForm.get('AssignedTo')?.value;

    // Perform further actions with the selected user IDs, such as submitting them to a server

    console.log(selectedUserIds); // Check if the selectedUserIds are correctly captured

    if (this.AddNewTaskForm.valid) {
      this.addTask = this.AddNewTaskForm.value;
      const assignedToIds: number[] = this.addTask.AssignedTo as unknown as number[];

      //this.addTask.AssignedTo=Number(this.addTask.AssignedTo);
      this.addnewTaskService.AddTask(this.addTask).subscribe(
        (response: any) => {
          console.log('Task profile added successfully:', response);
          this.snackBar.open(`Task Added Sucessfully,Refresh page to view:`, 'Close', { duration: 3000 });
          //this.router.navigate(['/Batch_dashboard', this.batchId]);

          // Optionally, you can navigate to another page or display a success message here
        },
        (error: any) => {
          console.log('Task profile Not added Error:', error);
          this.snackBar.open(`Task profile Not added Error:`, 'Close', { duration: 3000 });
          // Handle error appropriately, such as displaying error messages to the user
        }
      );
    }

  }
  GetEmployeeByBatch(){
    this.getUserByBatchService.Getall(this.batchId).subscribe(
      (data: any) => {
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.AllEmployyes = data.$values;
          this.AllEmployyes=this.AllEmployyes.sort((a, b) => b.total_Average_RatingStatus - a.total_Average_RatingStatus);
          console.log(this.AllEmployyes);
        } else {
          console.error('Unexpected data format:', data);

        this.snackBar.open(`Unexpected data format: ${{data}}`, 'Close', { duration: 3000 });
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
        this.snackBar.open(`Error fetching batches: ${{error}}`, 'Close', { duration: 3000 });
      }
    );
  }
  viewBatch(taskiId: number): void {
    // Navigate to another component with batchId as a parameter
    this.router.navigate(['/Task_dashboard', taskiId]);
}


updateAssignedTo(userId: number, event: Event) {
  const checkbox = event.target as HTMLInputElement;
  const assignedToControl = this.AddNewTaskForm.get('AssignedTo') as FormArray;
  if (checkbox.checked) {
    assignedToControl.push(new FormControl(userId));
  } else {
    const index = assignedToControl.value.indexOf(userId);
    assignedToControl.removeAt(index);
  }
}

viewEmployee(userid:number) {
  this.router.navigate(['/UserProfile', userid]);
  }
  navigateToDahsBoard(){
    if(this.currentUser.Role==2){
      this.router.navigate(['AdminDashboard']);
    }
    else if(this.currentUser.Role==1){
      this.router.navigate(['Mentor_dashboard']);
    }
    else{
      this.router.navigate(['Employee_DashBoard']);
    }
  }
  AddEmployee() {
    let formData=null;
    if (this.AddMentorForm.valid) {
    formData = this.AddMentorForm.value;
      formData.role=Number(formData.role);

}
    else {
      formData=this.AddUserToBatchForm.value;
      formData=formData.EmplloyeesToAdd;
      delete formData.$id;
      delete formData.userId;
      delete formData.userName;
      delete formData.password;
      const user: User = Object.assign({}, formData);
      formData=user;




    }

    this.addnewemployeetoBatch.addEmployee(formData,this.batchId).subscribe(
      (response: any) => {
        console.log('New Employee added successfully:', response);
        this.snackBar.open(`New Employee Added Sucessfully,Refresh page to view: ${{response}}`, 'Close', { duration: 3000 });

        // Optionally, you can navigate to another page or display a success message here
      },
      (error: any) => {
        console.log('Employee Not added Error:', error);
        this.snackBar.open(`Employee Not added Error:: ${{error}}`, 'Close', { duration: 3000 });
        // Handle error appropriately, such as displaying error messages to the user
      }
    );
//Call The Service here and send the batchid and the form data


    }

    GetAllEmployees(){
      this.getAllEmployees.GetEmp().subscribe(
        (data: any) => {
          // Ensure data.$values exists and is an array before accessing it
          if (Array.isArray(data.$values)) {
            this.FetchedEmployees = data.$values;
            console.log('All Employees',this.FetchedEmployees);

          } else {
            console.error('Unexpected data format:', data);
          }
        },
        (error) => {
          console.error('Error fetching batches:', error);
          this.snackBar.open(`Error fetching batches:: ${{error}}`, 'Close', { duration: 3000 });
        }
      );

      //Code to fetch all employees
    }
    RemoveEmployee(Userid: number) {
      this.removeEmployeesFromBatchService.RemoveEmployee(Userid,this.batchId).subscribe(
        (response: any) => {
          console.log('Employee Removed successfully:', response);
          this.snackBar.open(`Employee Removed Sucessfully,Refresh page to view: ${{response}}`, 'Close', { duration: 3000 });

          // Optionally, you can navigate to another page or display a success message here
        },
        (error: any) => {
          console.log('Employee Removed added Error:', error);
          this.snackBar.open(`Employee Removed added Error: ${{error}}`, 'Close', { duration: 3000 });
          // Handle error appropriately, such as displaying error messages to the user
        }
      );

      }

//FeedbackView
viewFeedbacks(taskid: number) {
  this.router.navigate(['/Feedbacks', taskid]);
  }


}
