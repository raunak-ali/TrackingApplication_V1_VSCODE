import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Role } from 'src/app/Models/user';
import { AddMentorService } from 'src/app/Services/add-mentor.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-add-mentor',
  templateUrl: './add-mentor.component.html',
  styleUrls: ['./add-mentor.component.css']
})

export class AddMentorComponent implements OnInit {

  AddMentorForm!: FormGroup;
  error: string | undefined;
  Role=Role;



  constructor(private fb: FormBuilder, private AddMentorservice: AddMentorService,
    private router: Router, private loginservice :LoginService,) { }

  ngOnInit(): void {
    this.AddMentorForm = this.fb.group({
      name: ['', Validators.required], // Required field
      role: [1],
      domain: ['', Validators.required], // Required field
      jobTitle: [''],
      location: [''],
      phone: [''],
      isCr: [false],
      gender: [''],
      doj: [new Date(), Validators.required], // Required field
      capgeminiEmailId: ['', [Validators.required, Validators.email]], // Required and valid email
      grade: [''],
      totalAverageRatingStatus: [0],
      personalEmailId: [''],
      earlierMentorName: ['None'],
      finalMentorName: ['None'],
      attendanceCount: [0],
      batches: [null]
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
  onSubmit(): void {
    if (this.AddMentorForm.valid) {
      const formData = this.AddMentorForm.value;
      formData.role=Number(formData.role);
      this.AddMentorservice.Addmentor(formData).subscribe(
        (response: any) => {
          console.log('Mentor profile added successfully:', response);
          const userId = response.userId;
          this.router.navigate(['AddNewBatch', userId]);
          // Optionally, you can navigate to another page or display a success message here
        },
        (error: any) => {
          console.log('Mentor profile Not added Error:', error);
          // Handle error appropriately, such as displaying error messages to the user
        }
      );
    }
  }

}
