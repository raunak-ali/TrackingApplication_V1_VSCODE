import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Role } from 'src/app/Models/user';
import { AddMentorService } from 'src/app/Services/add-mentor.service';

@Component({
  selector: 'app-add-mentor',
  templateUrl: './add-mentor.component.html',
  styleUrls: ['./add-mentor.component.css']
})

export class AddMentorComponent implements OnInit {

  AddMentorForm!: FormGroup;
  error: string | undefined;
  Role=Role;



  constructor(private fb: FormBuilder, private AddMentorservice: AddMentorService) { }

  ngOnInit(): void {
    this.AddMentorForm = this.fb.group({
      name: ['', Validators.required], // Required field
      role: [Role.Employee],
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
      earlierMentorName: [''],
      finalMentorName: [''],
      attendanceCount: [0],
      batches: [null]
    });

  }


  onSubmit(): void {
    if (this.AddMentorForm.valid) {
      const formData = this.AddMentorForm.value;
      this.AddMentorservice.Addmentor(formData).subscribe(
        (response: any) => {
          console.log('Mentor profile added successfully:', response);
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
