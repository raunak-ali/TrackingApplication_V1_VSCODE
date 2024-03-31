import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AddMentorComponent } from './components/add-mentor/add-mentor.component';
import { AddBatchesComponent } from './components/add-batches/add-batches.component';

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
      }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
