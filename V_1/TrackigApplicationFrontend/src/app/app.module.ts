import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

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

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AddMentorComponent,
    AddBatchesComponent,
    GetBatchComponent,
    MentorDashboardComponent,
    BatchDashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
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
