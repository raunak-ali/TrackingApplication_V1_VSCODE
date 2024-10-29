import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { delay } from 'rxjs';
import { FeedBacks } from 'src/app/Models/feed-backs';
import { GetModules } from 'src/app/Models/get-modules';
import { AddModuleServicesService } from 'src/app/Services/add-module-services.service';
import { GetModuleFeedbackService } from 'src/app/Services/get-module-feedback.service';
import { GetModuleServicesService } from 'src/app/Services/get-module-services.service';
import { LoginService } from 'src/app/Services/login.service';
import { Comments } from '../task-submissions/task-submissions.component';
import { User } from 'src/app/Models/user';
import { GetUser } from 'src/app/Models/get-user';
import { GetTasksService } from 'src/app/Services/get-tasks.service';
import { GetTask } from 'src/app/Models/get-task';
import { GetFeedBacksService } from 'src/app/Services/get-feed-backs.service';
import { NavigationService } from 'src/app/Services/navigation.service';
import { UpdateFeedbackService } from 'src/app/Services/update-feedback.service';
import { AddFeedback } from 'src/app/Models/add-feedback';
import { ProcteredService } from 'src/app/Services/proctered.service';
import { GetProctered } from 'src/app/Models/get-proctered';
import { Role } from '../../Models/user';

@Component({
  selector: 'app-module-feedback',
  templateUrl: './module-feedback.component.html',
  styleUrls: ['./module-feedback.component.css']
})
export class ModuleFeedbackComponent implements OnInit {

  commentsOptions: string[] = Object.keys(Comments).filter(key => !isNaN(Number(Comments[key as keyof typeof Comments]))) as string[];

  error: string | undefined;
  allModules!: GetModules[];
  searchQuery: string = '';
  batchId = +this.route.snapshot.params['batchId'];
  currentUser = this.loginservice.getUserFromSession();
  AddNewModuleForm: any;
  showForm: boolean = false;
  allFeedbacks: FeedBacks[] = [];
  modules: any[] = []; // Assuming you have this array populated with module data
  selectedModules: any[] = [];
  // Create a Set to store unique user IDs
  uniqueUserIds = new Set<number>();
  allUsers: GetUser[] = []; // Assuming User is the type of your user objects
  AllTasks!:GetTask[];
  navigationHistory: string[]=[];
  showNavigationHistory: boolean = false;
  feedbacksForm: any;
  filteredArray!: { feedbackId: number; totalAverageRating: number; comments: number; description: string; user: {
    userId: number; name: string; total_Average_RatingStatus: number;
}; task: {
  userTaskID: number ,taskName: string;
}; }[];
allProcts:GetProctered[]=[];

  toggleNavigationHistory() {
    this.showNavigationHistory = !this.showNavigationHistory;
  }
  getLinkName(link: string): string {
    // Customize this function to format the link name as needed
    // For example, you might want to remove slashes or decode URL parts
    return link.replace(/\//g, ' ').trim();
  }

  // Iterate through allFeedbacks array to extract unique users
  moduleNames!: string[]
  onInputChange(): void {

      if (this.searchQuery.trim() === '') {
        this.selectedModules = this.allModules; // Display all batches when search query is empty
        this.moduleNames = this.selectedModules.map(module => module.moduleName);

      } else {
        this.selectedModules = this.allModules.filter(module =>
          module.moduleName.toLowerCase().includes(this.searchQuery.toLowerCase()));
          this.moduleNames = this.selectedModules.map(module => module.moduleName);
        }


  }
  filterModules(query: string): void {
    if (!query) {
      this.selectedModules = [];
    } else {
      this.selectedModules = this.modules.filter(module =>
        module.moduleName.toLowerCase().includes(query.toLowerCase())
      );
    }
  }

  getFeedbackRating(User: GetUser, module: GetModules): number | undefined {
    var currentfeedback = this.allFeedbacks.find(f => f.moduleId == module.moduleId && f.userId == User.userId);
    return currentfeedback?.totalAverageRating;
  }

  getFeedbackComment(User: GetUser, module: GetModules): string | undefined {
    var currentfeedback = this.allFeedbacks.find(f => f.moduleId == module.moduleId && f.userId == User.userId);
    if (currentfeedback) {
      return this.commentsOptions[currentfeedback.comments-1];
    }
    return undefined;
  }

  getFeedbackDescription(User: GetUser, module: GetModules): string | undefined {
    var currentfeedback = this.allFeedbacks.find(f => f.moduleId == module.moduleId && f.userId == User.userId);
    return currentfeedback?.description;
  }

  getModuleColumns(): string[] {
    return this.selectedModules.map(module => module.moduleName);
  }


  constructor(private getModuleServicesService: GetModuleServicesService,
    private loginservice: LoginService,
    private router: Router,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private addModuleServicesService: AddModuleServicesService,
    private getModuleFeedbackService: GetModuleFeedbackService,
    private gettasksservice:GetTasksService,
    private getFeedBacksService :GetFeedBacksService,

    private navigationService: NavigationService,
    private updateFeedbackService:UpdateFeedbackService ,
private procteredService:ProcteredService

  ) {



  }
  ngOnInit(): void {
    this.navigationHistory = this.navigationService.getNavigationHistory();

    this.fetchModules(); //Fetch All modules for the batch
    this.fetchProtcs();
    console.log('USERSSSSSSSSSSSSSSSSSSSSSSSss',this.allUsers);
  }
  fetchProtcs() {
    this.procteredService.GetProct().subscribe(
      (data: any) => {
        console.log(data);
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.allProcts = data.$values;


          console.log('a;;proct',this.allProcts);
        } else {
          console.error('Error Getting allProcts:', data);
          this.snackBar.open(`Error Getting allProcts:: ${{data}}`, 'Close', { duration: 3000 });
        }
      },
      (error) => {
        console.error('Error Getting allProcts:', error);
        this.snackBar.open(`Error Getting allProcts:: ${{error}}`, 'Close', { duration: 3000 });
      }
    );

  }
  ApproveARecord(proctid: number) {
    this.procteredService.ApproveProct(proctid).subscribe(
      (response: any) => {
        console.log('Record Approve', response);
        this.snackBar.open('Record Approve', 'Close', { duration: 3000 });

        // Optionally, you can navigate to another page or display a success message here
      },
      (error: any) => {
        console.log('Record   Not added Error:', error);
        this.snackBar.open(`Record Approve sucessfully`, 'Close', { duration: 3000 });
        // Handle error appropriately, such as displaying error messages to the user
      }
    );
    }
    downloadZipFileFromBase64(base64String: string, fileName: string) {
      // Decode the Base64 string to a byte array
      const byteCharacters = atob(base64String);
      const byteNumbers = new Array(byteCharacters.length);
      for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
      }
      const byteArray = new Uint8Array(byteNumbers);

      // Convert the byte array to a Blob
      const blob = new Blob([byteArray], { type: 'application/zip' });

      // Create a Blob URL for the Blob
      const blobUrl = URL.createObjectURL(blob);

      // Create an anchor element
      const a = document.createElement('a');

      // Set the href attribute of the anchor element to the Blob URL
      a.href = blobUrl;

      // Set the download attribute to specify the filename for the downloaded file
      a.download = fileName;

      // Programmatically click on the anchor element to trigger the download
      document.body.appendChild(a); // Required for Firefox
      a.click();

      // Cleanup: remove the anchor element and revoke the Blob URL
      document.body.removeChild(a);
      URL.revokeObjectURL(blobUrl);
    }
  fetchModules(): void {
    this.getModuleServicesService.GetModules(this.batchId).subscribe(
      (data: any) => {
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {


          this.allModules = data.$values;
          this.selectedModules = this.allModules;
          //Fetch All Feedbacks for each module
          this.allModules.forEach(async module => {
            this.fetchmodulefeedbacks(module.moduleId);
            await delay(1000);
          });
          console.log(this.allModules);


        }

        else {
          console.error('Unexpected data format:', data);
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
      }
    );
  }
  fetchmodulefeedbacks(moduleId: number): void {
    this.getModuleFeedbackService.GetModuleFeedback(moduleId).subscribe(
      (data: any) => {
        console.log(data);
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.allFeedbacks = [...this.allFeedbacks, ...data.$values];
          //Fetch All Feedbacks for each module
          this.fetchUsers()

          console.log("ALL FEEDBACK", this.allFeedbacks);
        } else {
          console.error('Unexpected data format:', data);
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
      }
    );
  }
fetchUsers(){
  this.allFeedbacks.forEach(feedback => {
    // Check if the user ID of the current feedback is not already in the Set
    var existing_user = this.allUsers.findIndex(user => user.userId == feedback.user.userId);
    if(this.currentUser.Role!=0){
    if (existing_user === -1) {
      this.allUsers.push(feedback.user);
    }}
    else{
      if(feedback.user.userId==this.currentUser.Userid){
      this.allUsers.push(feedback.user);}
    }
  }

  );
}
  navigateTo(route: string): void {
    this.router.navigate([route]);
  }

  logout(): void {
    this.loginservice.clearUser();
    this.loginservice.clearToken();
    this.loginservice.clearcurrentUser();
    this.router.navigate(["Login"]);
    // Implement logout functionality
  }

  navigateToDahsBoard() {
    if (this.currentUser.Role == 2) {
      this.router.navigate(['AdminDashboard']);
    }
    else if (this.currentUser.Role == 1) {
      this.router.navigate(['Mentor_dashboard']);
    }
    else {
      this.router.navigate(['Employee_DashBoard']);
    }
  }
  viewEmployee(userid: number) {
    this.router.navigate(['/UserProfile', userid]);
  }


  selectedModule: any = null; // Holds the selected module
  selectedTask:any=null;

  //Lets Make the Module and Task specfic feedbacks now
  //Fetch the tasks per module
  //fetch all the module by name
  //When the task id fetched ,then fetch the feedback fo that task
  fetchtaskfeedback(){

  }
  showTasks:boolean=false;
  onChangeModule(event: any) {
    var moduleId:number =event.target.value;

    // Handle the module change event here
    console.log('Selected Module ID:', moduleId);
    if(moduleId!=null){
    this.FetchTasks(moduleId);
  }
    this.showTasks=true;
    // Perform any actions you want to do when the module changes
  }
  onChangeTask(event: any){
    const taskid = event ? event.target.value : null;
    this.fetchfeedbacks(taskid)
  }
  AllTaskFeedback!:FeedBacks[];
  fetchfeedbacks(taskid:number){
    this.getFeedBacksService.GetAll(taskid).subscribe(
      (data: any) => {
        console.log(data);
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.message.value.$values)) {
          console.log("DATA", data.message.value.$values);
          this.AllTaskFeedback = data.message.value.$values;
          console.log(this.AllTaskFeedback);

        }

      },
      (error: any) => {
        console.error('Error fetching Feedbacks:', error);
      });
  }


  FetchTasks(moduleId:number){
    this.gettasksservice.Getall(moduleId).subscribe(
      (data: any) => {
        console.log(data);
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.AllTasks = data.$values;
          this.AllTasks=this.AllTasks.sort((a, b) => b.priority - a.priority);
          console.log(this.AllTasks);
        } else {
          console.error('Error Getting Tasks:', data);
          this.snackBar.open(`Error Getting Tasks:: ${{data}}`, 'Close', { duration: 3000 });
        }
      },
      (error) => {
        console.error('Error Getting Tasks:', error);
        this.snackBar.open(`Error Getting Tasks:: ${{error}}`, 'Close', { duration: 3000 });
      }
    );
  }
  //Now set the Feedback table to align for all modules in a batch for all employees

  //Send Email
  sendEmailtoemployee() :void {

    const addFeedbacks: any= this.convertFeedbacks(this.AllTaskFeedback);
      console.log("Submitted feedbacks:",addFeedbacks);
    // Call your service method to send data to the server, or perform other actions as needed
    // Here you can handle the submission of updated feedback data
    this.updateFeedbackService.SendEmailToEmployee(addFeedbacks).subscribe(
      (response: any) => {
        console.log('Feedbacks updated successfully:', response);
        this.snackBar.open('Feedback SUcessfully Sent to employees, Refresh page to view', 'Close', { duration: 3000 });

        // Optionally, you can navigate to another page or display a success message here
      },
      (error: any) => {
        console.log('Feedbacks  Not added Error:', error);
        this.snackBar.open(`Feedback  Added sucessfully`, 'Close', { duration: 3000 });
        // Handle error appropriately, such as displaying error messages to the user
      }
    );}


    convertFeedbacks(feedbacks: FeedBacks[]): { feedbacks: AddFeedback[] } {
      const addFeedbacks: AddFeedback[] = feedbacks.map((feedback) => ({
          feedbackId: feedback.feedbackId,
          totalAverageRating: feedback.totalAverageRating,
          comments: feedback.comments,
          description: feedback.description,
          user: {
              userId: feedback.user.userId,
              name: feedback.user.name,
              total_Average_RatingStatus: feedback.user.total_Average_RatingStatus,
          },
          userTask: {
              userTaskID: feedback.userTask.userTaskID,
              taskName: feedback.userTask.taskName,
          },
          module: feedback.module,
          ratings: null, // or undefined, depending on your requirements
          Submission_Count: feedback.submission_Count,
      }));

      return { feedbacks: addFeedbacks };
  }
    sendEmailtoMentor() :void {
      const addFeedbacks: any= this.convertFeedbacks(this.AllTaskFeedback);
      console.log("Submitted feedbacks:",addFeedbacks);
      // Call your service method to send data to the server, or perform other actions as needed
      // Here you can handle the submission of updated feedback data
      this.updateFeedbackService.SendEmailToMentor(addFeedbacks).subscribe(
        (response: any) => {
          console.log('Feedbacks updated successfully:', response);
          this.snackBar.open('Feedback SUcessfully Sent to mentor, Refresh page to view', 'Close', { duration: 3000 });

          // Optionally, you can navigate to another page or display a success message here
        },
        (error: any) => {
          console.log('Feedbacks  Not added Error:', error);
          this.snackBar.open(`Feedback  Added sucessfully`, 'Close', { duration: 3000 });
          // Handle error appropriately, such as displaying error messages to the user
        }
      );
}


//Get all Proctered Rows
//If the Approve button is clicked
}
