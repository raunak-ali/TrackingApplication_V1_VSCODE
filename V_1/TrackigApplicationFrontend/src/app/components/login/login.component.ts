import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from 'src/app/Services/login.service';
import { Role } from '../../Models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent  implements OnInit {
  loginForm!: FormGroup;
  //checkotp!:FormGroup;
  jwtToken: string | undefined;
  error: string | undefined;
  //isPasswordRequired: boolean = false; // Add this line
  //otpResponse!: string;


  constructor(private fb: FormBuilder,
    private loginservice: LoginService,
    private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      UserName: ['', Validators.required],
      Password: ['', Validators.required]
    });

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
            this.router.navigate(['/Mentor_dashboard']);
          }
          else{
            this.jwtToken="IT IS Not a Mentor";
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
}

