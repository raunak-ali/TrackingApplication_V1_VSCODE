import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CompileAndExecuteService } from 'src/app/Services/compile-and-execute.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-compile-and-execute',
  templateUrl: './compile-and-execute.component.html',
  styleUrls: ['./compile-and-execute.component.css']
})
export class CompileAndExecuteComponent  implements OnInit {

  code: string = '';
  sampleInput: string = '';

  constructor(private route: ActivatedRoute,
    private fb: FormBuilder,
    private loginservice :LoginService,
    private router: Router,
    private compileAndExecuteService:CompileAndExecuteService) { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
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
  submitCode() {
    // Call the service method to submit the code and sample input
    this.compileAndExecuteService.CompileCode(this.code, this.sampleInput)
      .subscribe(response => {
        // Handle the response from the service
        console.log(response); // Replace with your logic to handle the response
      });
  }
}
