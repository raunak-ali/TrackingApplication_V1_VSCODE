import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { GetBatches } from 'src/app/Models/get-batches';
import { GetTask } from 'src/app/Models/get-task';
import { GetUser } from 'src/app/Models/get-user';
import { User } from 'src/app/Models/user';
import { AddnewTaskService } from 'src/app/Services/addnew-task.service';
import { GetBatchesService } from 'src/app/Services/get-batches.service';
import { GetTasksService } from 'src/app/Services/get-tasks.service';
import { GetUserByBatchService } from 'src/app/Services/get-user-by-batch.service';
import { GetUserTasksService } from 'src/app/Services/get-user-tasks.service';
import { GetUserinfoService } from 'src/app/Services/get-userinfo.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit{

  UserId!: number;
  User!:User;
  AllTasks!:GetTask[];
  AllEmployyes!:GetUser[];

  getMentorID!: any;
  allBatches!: GetBatches[];
  showForm: boolean = false;

  isDropdownOpen: boolean = false;

  // Add new properties
showBatches: boolean = false;
showTasks: boolean = false;
showSubtasks: { [taskId: number]: boolean } = {};

// Add new methods
toggleBatches(): void {
  this.showBatches = !this.showBatches;
  // Hide tasks and subtasks when toggling batches

}

toggleTasks(): void {
  this.showTasks = !this.showTasks;
  // Hide batches when toggling tasks

}

toggleSubtasks(taskId: number): void {
  if (this.showSubtasks[taskId] === undefined) {
    this.showSubtasks[taskId] = true;
  } else {
    this.showSubtasks[taskId] = !this.showSubtasks[taskId];
  }
}

  toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }



  currentUser = this.loginservice.getUserFromSession();
  constructor(private route: ActivatedRoute,
    private gettasksservice:GetTasksService,
    private getUserByBatchService:GetUserByBatchService,
    private fb: FormBuilder,
    private loginservice :LoginService,
    private addnewTaskService:AddnewTaskService,
    private router: Router,
      private getbatchesservice: GetBatchesService,
      private getUserinfoService:GetUserinfoService,
      private getUserTasksService :GetUserTasksService ) { }

    ngOnInit(): void {

      console.log(this.currentUser);

      // Retrieve the batchId parameter from the route
      this.UserId = this.currentUser.UserId;
      this.fetchuserinfo();
      this.fetchTransactions();
      this.fetchUsertasks();


    }

  //Use userid to get the user info
      //Get User table info

      fetchuserinfo():void{
        this.getUserinfoService.Getall(this.UserId).subscribe(
          (data: any) => {
            console.log('For User info',data);
            // Ensure data.$values exists and is an array before accessing it

              this.User = data;
              console.log(this.User);

          },
          (error) => {
            console.error('Error fetching batches:', error);
          }
        );

      }


      //get-batch->Using userid now
      fetchTransactions(): void {
        this.getbatchesservice.Getall(this.UserId).subscribe(
          (data: any) => {
            // Ensure data.$values exists and is an array before accessing it
            if (Array.isArray(data.$values)) {
              this.allBatches = data.$values;
              console.log(this.allBatches);
            } else {
              console.error('Unexpected data format:', data);
            }
          },
          (error) => {
            console.error('Error fetching batches:', error);
          }
        );
      }

      viewBatch(batchId: number): void {
        // Navigate to another component with batchId as a parameter
        this.router.navigate(['/Batch_dashboard', batchId]);
    }
      //

  //show all of the task the user has been assigned along with its submissions
  fetchUsertasks(){
    this.getUserTasksService.Getall(this.UserId).subscribe(
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



}
