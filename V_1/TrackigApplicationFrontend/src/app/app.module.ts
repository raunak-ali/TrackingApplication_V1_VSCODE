import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {MatCheckboxModule} from '@angular/material/checkbox';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginService } from './Services/login.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { AddMentorComponent } from './components/add-mentor/add-mentor.component';
import { AddBatchesComponent } from './components/add-batches/add-batches.component';
import { GetBatchComponent } from './components/get-batch/get-batch.component';
import { MentorDashboardComponent } from './components/mentor-dashboard/mentor-dashboard.component';
import { BatchDashboardComponent } from './components/batch-dashboard/batch-dashboard.component';
import { TaskboardComponent } from './components/taskboard/taskboard.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { EmployeeDashBoardComponent } from './components/employee-dash-board/employee-dash-board.component';
import { TaskSubmissionsComponent } from './components/task-submissions/task-submissions.component';
import { ProfileComponent } from './components/profile/profile.component';
import { LayoutComponent } from './components/layout/layout.component';
import { MatCardModule } from '@angular/material/card';

import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // Add this line for animations
import { MatFormFieldModule } from '@angular/material/form-field'; // Add this line for form fields
import { MatInputModule } from '@angular/material/input'; // Add this line for input fields
import { MatDatepickerModule } from '@angular/material/datepicker'; // Add this line for datepicker
import { MatNativeDateModule } from '@angular/material/core'; // Add this line for datepicker
import { MatButton, MatButtonModule } from '@angular/material/button'; // Add this line for buttons
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select'; // Import MatSelectModule
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { BatchEmployeeComponent } from './components/batch-employee/batch-employee.component';
import { CompileAndExecuteComponent } from './components/compile-and-execute/compile-and-execute.component';
import {MatTabsModule} from '@angular/material/tabs';


// Import jQuery



@NgModule({
  declarations: [

    AppComponent,
    LoginComponent,
    AddMentorComponent,
    AddBatchesComponent,
    GetBatchComponent,
    MentorDashboardComponent,
    BatchDashboardComponent,
    TaskboardComponent,
    EmployeeDashBoardComponent,
    TaskSubmissionsComponent,
    ProfileComponent,
    LayoutComponent,
    AdminDashboardComponent,
    BatchEmployeeComponent,
    CompileAndExecuteComponent,


  ],
  imports: [
    MatTabsModule,
    MatCheckboxModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    NgSelectModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatToolbarModule,
    MatMenuModule,
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule, // Add this line for animations
    MatFormFieldModule, // Add these lines for Angular Material modules
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatExpansionModule,
    MatSelectModule,
    MatListModule,
    MatIconModule, // Add MatSelectModule here
    MatTableModule,
    MatSnackBarModule

  ],
  providers: [LoginService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
