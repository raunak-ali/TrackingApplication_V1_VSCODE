import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AddTask } from 'src/app/Models/add-task';
import { GetTask } from 'src/app/Models/get-task';
import { GetUser } from 'src/app/Models/get-user';
import { User } from 'src/app/Models/user';
import { AddnewTaskService } from 'src/app/Services/addnew-task.service';
import { GetBatchesService } from 'src/app/Services/get-batches.service';
import { GetTasksService } from 'src/app/Services/get-tasks.service';
import { GetUserByBatchService } from 'src/app/Services/get-user-by-batch.service';
import { LoginService } from 'src/app/Services/login.service';


@Component({
  selector: 'app-batch-dashboard',
  templateUrl: './batch-dashboard.component.html',
  styleUrls: ['./batch-dashboard.component.css']
})
export class BatchDashboardComponent implements OnInit {
  @ViewChild('assignedTo')assignedToSelect!: ElementRef;
  batchId!: number;
  AllTasks!:GetTask[];
  AllEmployyes!:GetUser[];
  AddNewTaskForm!: FormGroup;
  getMentorID!: any;
  showForm: boolean = false;
  addTask!:AddTask;


  constructor(private route: ActivatedRoute,
    private gettasksservice:GetTasksService,
    private getUserByBatchService:GetUserByBatchService,
    private fb: FormBuilder,
    private loginservice :LoginService,
    private addnewTaskService:AddnewTaskService,
    private router: Router) { }

  ngOnInit(): void {


    // Retrieve the batchId parameter from the route
    this.batchId = +this.route.snapshot.params['batchId'];
    this.getMentorID=this.loginservice.getUser();
    if (this.getMentorID != null) {
      // Check if this.existing_mentor is a valid number string
      this.getMentorID = Number(this.getMentorID);
      if (!isNaN(this.getMentorID)) {
        this.getMentorID=this.getMentorID;
      }
    }
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
      DeadLine: ['', Validators.required],
      Status: [0],
      AssignedBy: [this.getMentorID],
      AssignedTo: this.fb.array([]),
      BatchId: [this.batchId],
      Comments: ['']
    })

    // Now you can use this.batchId in your component logic
    console.log('Batch ID:', this.batchId);
  }
  FetchTasks(){
    this.gettasksservice.Getall(this.batchId).subscribe(
      (data: any) => {
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.AllTasks = data.$values;
          console.log(this.AllTasks);
        } else {
          console.error('Unexpected data format:', data);
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
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
          // Optionally, you can navigate to another page or display a success message here
        },
        (error: any) => {
          console.log('Task profile Not added Error:', error);
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
          console.log(this.AllEmployyes);
        } else {
          console.error('Unexpected data format:', data);
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
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

}
