import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AddMentorComponent } from './components/add-mentor/add-mentor.component';
import { AddBatchesComponent } from './components/add-batches/add-batches.component';
import { GetBatchComponent } from './components/get-batch/get-batch.component';
import { MentorDashboardComponent } from './components/mentor-dashboard/mentor-dashboard.component';
import { BatchDashboardComponent } from './components/batch-dashboard/batch-dashboard.component';
import { TaskboardComponent } from './components/taskboard/taskboard.component';
import { EmployeeDashBoardComponent } from './components/employee-dash-board/employee-dash-board.component';
import { TaskSubmissionsComponent } from './components/task-submissions/task-submissions.component';

const routes: Routes = [
  {
    path:"Login",
    component:LoginComponent


    },
    {
    path:"AddMentor",
    component:AddMentorComponent
    },
    {
      path:"AddNewBatch",
      component:AddBatchesComponent
      },
      {
        path:"GetAllBatches",
        component:GetBatchComponent
      },
      {
        path:"Mentor_dashboard",
        component:MentorDashboardComponent
      },
      {
        path:"Batch_dashboard/:batchId",
        component:BatchDashboardComponent
      },
      {
        path:"Task_dashboard/:taskiId",
        component:TaskboardComponent
      },
      {
        path:"Employee_DashBoard",
        component:EmployeeDashBoardComponent
      },
      {
        path:"SubTaskSubmission/:subtaskid",
        component:TaskSubmissionsComponent
      }



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
