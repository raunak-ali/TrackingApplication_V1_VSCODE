import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FeedBacks } from 'src/app/Models/feed-backs';
import { AddEmployeesToBatchService } from 'src/app/Services/add-employees-to-batch.service';
import { AddnewTaskService } from 'src/app/Services/addnew-task.service';
import { GetAllEmployeesService } from 'src/app/Services/get-all-employees.service';
import { GetFeedBacksService } from 'src/app/Services/get-feed-backs.service';
import { GetTasksService } from 'src/app/Services/get-tasks.service';
import { GetUserByBatchService } from 'src/app/Services/get-user-by-batch.service';
import { LoginService } from 'src/app/Services/login.service';
import { RemoveEmployeesFromBatchService } from 'src/app/Services/remove-employees-from-batch.service';
import { Comments } from '../task-submissions/task-submissions.component';
import { User } from '../../Models/user';
import { UpdateFeedbackService } from 'src/app/Services/update-feedback.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NavigationService } from 'src/app/Services/navigation.service';

@Component({
  selector: 'app-batch-employee',
  templateUrl: './batch-employee.component.html',
  styleUrls: ['./batch-employee.component.css']
})
export class BatchEmployeeComponent implements OnInit {
  navigationHistory: string[]=[];
toggleEditMode() {
throw new Error('Method not implemented.');
}
showNavigationHistory: boolean = false;

toggleNavigationHistory() {
  this.showNavigationHistory = !this.showNavigationHistory;
}

  taskid!:number;
  AllFeedBacks!:FeedBacks[];
  currentUser = this.loginservice.getUserFromSession();
  editable:boolean=false;
  commentsOptions: string[] =  Object.keys(Comments).filter(key => !isNaN(Number(Comments[key as keyof typeof Comments]))) as string[];
  editMode: boolean = false;
  ditMode: boolean = false; // Edit mode flag
  feedbacksForm!: FormGroup;
  filteredArray!: { feedbackId: number; totalAverageRating: number; comments: number; description: string; user: {
    userId: number; name: string; total_Average_RatingStatus: number;
}; task: {
  userTaskID: number ,taskName: string;
}; }[];
//Fetch all employees, and thier info
//Make this info editable
//Make a final Feedback for this employee with comments'
//And make taht editable as well please
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
  private getFeedBacksService :GetFeedBacksService,
  private updateFeedbackService:UpdateFeedbackService ,
  private snackBar: MatSnackBar,
  private navigationService: NavigationService

  ) { }
  ngOnInit(): void {
    this.navigationHistory = this.navigationService.getNavigationHistory();

    feedbacksForm: FormGroup;
    this.taskid = +this.route.snapshot.params['taskid'];
    console.log(this.taskid);
    this.fetchfeedbacks();
    this.initForm();



  }
  trackByFn(index: number, feedback: any): number {
    return feedback.feedbackId; // Use a unique identifier property of your feedback item
  }
  fetchfeedbacks(){
    this.getFeedBacksService.GetAll(this.taskid).subscribe(
      (data: any) => {
        console.log(data);
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.message.value.$values)) {
          console.log("DATA", data.message.value.$values);
          this.AllFeedBacks = data.message.value.$values;
          console.log(this.AllFeedBacks);
          this.filteredArray = this.AllFeedBacks.map(item => {
            const {
              feedbackId,
              totalAverageRating,
              comments,
              description,
              submission_Count,
              user: { userId,name, total_Average_RatingStatus },
              userTask: { userTaskID,taskName }
            } = item;


            return {
              feedbackId,
              totalAverageRating,
              comments,
              description,
              submission_Count,
              user: { userId,name, total_Average_RatingStatus},
              task: { userTaskID,taskName }
            };
          });

          console.log(this.filteredArray);
          this.feedbacksForm = this.fb.group({
            feedbacks: this.fb.array([])
          });
          this.initForm();
        } else {
          console.error('Unexpected data format:', data);
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
      });
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
  initForm(): void {

    if (this.filteredArray) {
      this.filteredArray.forEach(feedback => {
        console.log(this.currentUser.UserId);
        if (this.currentUser.Role==0){
        if (feedback.user.userId==this.currentUser.UserId){
        const { feedbackId, totalAverageRating, comments, description, user, task } = feedback;
        const newUser = { userId:user.userId,name: user.name, total_Average_RatingStatus: user.total_Average_RatingStatus };
        const newTask = { userTaskID:task.userTaskID,taskName: task.taskName };
        this.addFeedback({
          feedbackId,
          totalAverageRating,
          comments,
          description,
          user: newUser,
          userTask: newTask
        });}}
        else{
          const { feedbackId, totalAverageRating, comments, description, user, task } = feedback;
          const newUser = { userId:user.userId,name: user.name, total_Average_RatingStatus: user.total_Average_RatingStatus };
          const newTask = { userTaskID:task.userTaskID,taskName: task.taskName };
          this.addFeedback({
            feedbackId,
            totalAverageRating,
            comments,
            description,
            user: newUser,
            userTask: newTask
          });
        }
      });
    }

    console.log('Controls',this.feedbacks.controls);
  }

  get feedbacks(): FormArray {
    return this.feedbacksForm.get('feedbacks') as FormArray;
  }

  addFeedback(feedback: any): void {
    this.feedbacks.push(this.createFeedbackGroup(feedback));
  }

  createFeedbackGroup(feedback: FeedBacks): FormGroup {
    return this.fb.group({
      feedbackId: feedback.feedbackId,
      taskId: feedback.taskId,
      userId: feedback.userId,
      userTask: feedback.userTask,
      user: feedback.user,
      ratings: feedback.ratings,
      totalAverageRating: feedback.totalAverageRating,
      comments: feedback.comments,
      description: feedback.description
    });
  }

  submitFeedbacks(): void {

    console.log("Submitted feedbacks:", this.feedbacksForm.value);
    // Call your service method to send data to the server, or perform other actions as needed
    // Here you can handle the submission of updated feedback data
    this.updateFeedbackService.Getall(this.feedbacksForm.value).subscribe(
      (response: any) => {
        console.log('Feedbacks updated successfully:', response);
        this.snackBar.open('Feedback SUcessfully added, Refresh page to view', 'Close', { duration: 3000 });

        // Optionally, you can navigate to another page or display a success message here
      },
      (error: any) => {
        console.log('Feedbacks  Not added Error:', error);
        this.snackBar.open(`Feedback  Added sucessfully`, 'Close', { duration: 3000 });
        // Handle error appropriately, such as displaying error messages to the user
      }
    );

  }
  sendEmailtoemployee() :void {

    console.log("Submitted feedbacks:", this.feedbacksForm.value);
    // Call your service method to send data to the server, or perform other actions as needed
    // Here you can handle the submission of updated feedback data
    this.updateFeedbackService.SendEmailToEmployee(this.feedbacksForm.value).subscribe(
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



    sendEmailtoMentor() :void {

      console.log("Submitted feedbacks:", this.feedbacksForm.value);
      // Call your service method to send data to the server, or perform other actions as needed
      // Here you can handle the submission of updated feedback data
      this.updateFeedbackService.SendEmailToMentor(this.feedbacksForm.value).subscribe(
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
}
