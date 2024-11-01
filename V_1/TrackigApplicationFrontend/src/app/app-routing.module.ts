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
import { TaskSubmissionsComponent, Comments } from './components/task-submissions/task-submissions.component';
import { ProfileComponent } from './components/profile/profile.component';
import { LayoutComponent } from './components/layout/layout.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { BatchEmployeeComponent } from './components/batch-employee/batch-employee.component';
import { CompileAndExecuteComponent } from './components/compile-and-execute/compile-and-execute.component';
import { ModuleDashboardComponent } from './components/module-dashboard/module-dashboard.component';
import { ModuleFeedbackComponent } from './components/module-feedback/module-feedback.component';

const routes: Routes = [
  {
    path: "Mentor_dashboard",
    component: MentorDashboardComponent, // Use the layout component as the root component
    children: [
      { path: "Batch_dashboard/:batchId", component:BatchDashboardComponent  },
      {path:"compile",component:CompileAndExecuteComponent}
      //Add other compoenents, i wanna show the header in

    ]
  },
  {path:"compile/:subtaskid",component:CompileAndExecuteComponent},
  {
    path:"Login",
    component:LoginComponent


    },
    {
      path:"AdminDashboard",
      component:AdminDashboardComponent
    },
    {
    path:"AddMentor",
    component:AddMentorComponent
    },
    {
      path:"Feedbacks/:taskid",
      component:BatchEmployeeComponent
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
        path:"Module_dashboard/:batchId",
        component:ModuleDashboardComponent
      },
      {
        path:"Module_Feedback/:batchId",
        component:ModuleFeedbackComponent
      },
      {
        path:"Batch_dashboard/:batchId/:moduleId",
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
      },
      {
        path:"UserProfile/:UserId",
        component:ProfileComponent
        },



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
