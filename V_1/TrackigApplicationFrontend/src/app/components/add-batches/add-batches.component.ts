import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GetUser } from 'src/app/Models/get-user';
import { User } from 'src/app/Models/user';
import { AddBatchesService } from 'src/app/Services/add-batches.service';
import { AddMentorService } from 'src/app/Services/add-mentor.service';
import { GetMentorsService } from 'src/app/Services/get-mentors.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-add-batches',
  templateUrl: './add-batches.component.html',
  styleUrls: ['./add-batches.component.css']
})
export class AddBatchesComponent implements OnInit {

  AddBatchForm!: FormGroup;
  error: string | undefined;
  selectedFile: any;
  mentorid!: number;
  allMentors!:GetUser[];



  constructor(private fb: FormBuilder, private Addbatchesservice: AddBatchesService, private loginservice: LoginService
    ,private route: ActivatedRoute,private getMentorsService:GetMentorsService,
    private router: Router){}
  private existing_mentor?=this.loginservice.getUser();

  ngOnInit(): void {
this.fetchmentors();
    this.AddBatchForm = this.fb.group({
      MentorId: [''],
      Domain: ['', Validators.required],
      Description: ['', Validators.required],
      Employee_info_Excel_File: [null] ,
      Employee_info_Excel:[null]// A
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
  fetchmentors() {
    this.getMentorsService. GetMentors().subscribe(
      (data: any) => {
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.allMentors = data.$values;
          console.log("All Mentors",this.allMentors);
        } else {
          console.error('Unexpected data format:', data);
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
      }
    );
  }


  onSubmit(): void {
    if (this.AddBatchForm.valid) {

      const formData = this.AddBatchForm.value;
      console.log(formData);

             formData.Employee_info_Excel_File=this.selectedFile;
      const file = formData.Employee_info_Excel_File as File;

      // Convert file to byte array
      this.fileToBase64(file).subscribe(byteArray => {
      formData.Employee_info_Excel = byteArray;


        // Set ValidationDocs to null
      formData.Employee_info_Excel_File=null;



      this.Addbatchesservice.Addmentor(formData).subscribe(
        (response: any) => {
          console.log('Batches profile added successfully:', response);
          // Optionally, you can navigate to another page or display a success message here
        },
        (error: any) => {
          console.log('Batches profile Not added Error:', error);
          // Handle error appropriately, such as displaying error messages to the user
        }
      );
    });
    }
  }

  fileToBase64(file: File): Observable<string> {
    return new Observable<string>(observer => {
      const reader = new FileReader();
      reader.onload = () => {
        const base64String = reader.result as string;
        observer.next(base64String);
        observer.complete();
      };
      reader.onerror = error => {
        observer.error(error);
      };
      reader.readAsDataURL(file);
    });
  }
  onFileChange(event: any): void {
    const inputElement = event.target;
    if (inputElement.files && inputElement.files.length > 0) {
      const file = inputElement.files[0];
      this.selectedFile = file;
      // Update form value with the selected file
      //this.accountProfileForm.get('validationDocs')?.setValue(file);
    }
  }

}


