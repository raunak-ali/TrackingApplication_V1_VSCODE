import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from 'src/app/Services/login.service';
import { Role } from '../../Models/user';
import { Router } from '@angular/router';
import { ResetPasswordService } from 'src/app/Services/reset-password.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent  implements OnInit {
  loginForm!: FormGroup;
  showemailform:boolean=false;
  //checkotp!:FormGroup;
  jwtToken: string | undefined;
  error: string | undefined;
  resetPasswordForm!: FormGroup;
  //isPasswordRequired: boolean = false; // Add this line
  //otpResponse!: string;
  resetPanelExpanded = false;
  resetPasswordFormVisible = false;

  emailForm!: FormGroup;


  constructor(private fb: FormBuilder,
    private loginservice: LoginService,
    private router: Router,
    private resetPasswordService :ResetPasswordService,


    private snackBar: MatSnackBar
    ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      UserName: ['', Validators.required],
      Password: ['', Validators.required]
    });
    this.resetPasswordForm = this.fb.group({
      username: ['', Validators.required],
      oldpassword: ['', Validators.required],
      newpassword: ['', Validators.required]
    });
    this.emailForm = this.fb.group({
      capgeminiid: ['', [Validators.required, Validators.email]]
    });

  }
  setRole(role: string): void {
    sessionStorage.setItem('Role', role);
  }
  onSubmit(): void {
    if (this.loginForm.valid) {
      const formData = this.loginForm.value;
      this.loginservice.login(formData).subscribe(
        (response) => {
          this.jwtToken = "Logged in sucessfully";
          var User=this.loginservice.getcurrentUser();
          if(User&&User.Role==1){
            this.jwtToken="IT IS A MENTOR";
            this.setRole("Mentor");
            this.router.navigate(['/Mentor_dashboard']);
          }
          else if(User&&User.Role==0){
            this.setRole("Employee");
            this.jwtToken="IT IS Not a Mentor";
            this.router.navigate(['Employee_DashBoard']);
          }
          else{
            this.router.navigate(['/AdminDashboard']);
            this.setRole("Admin");
          }

          this.error = undefined;
          //this.router.navigate(['/GetTransactions']);

        },
        (error) => {
          this.error = error.error.message;
          this.jwtToken = undefined;
        }
      );
    }
  }
SendResetPasswordEmail():void{
  const { capgeminiid } = this.emailForm.value;
  this.resetPasswordService.sendemail(capgeminiid).subscribe(
    (response: any) => {
      // Optionally, you can navigate to another page or display a success message here
      console.log(response);
      this.resetPasswordFormVisible = true;
    },
    (error: any) => {
      console.log(error);
      this.snackBar.open('Error sending reset password email', 'Close', { duration: 3000 });
      // Handle error appropriately, such as displaying error messages to the user
    }
  );
}


ChecksForResetPassword():void{
  const { username, oldpassword, newpassword } = this.resetPasswordForm.value;
  this.resetPasswordService.resetpassword(username, oldpassword, newpassword).subscribe(
    (response: any) => {
      this.snackBar.open('Password changed successfully', 'Close', { duration: 3000 });
      // Optionally, you can navigate to another page or display a success message here
      this.collapseResetPanel();
    },
    (error: any) => {
      this.snackBar.open('Password not changed', 'Close', { duration: 3000 });
      // Handle error appropriately, such as displaying error messages to the user
    }
  );
}
expandResetPanel(): void {
  this.resetPanelExpanded = true;
}

collapseResetPanel(): void {
  this.resetPanelExpanded = false;
}

}
