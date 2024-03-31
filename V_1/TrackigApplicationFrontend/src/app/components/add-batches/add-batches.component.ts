import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { AddBatchesService } from 'src/app/Services/add-batches.service';
import { AddMentorService } from 'src/app/Services/add-mentor.service';
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



  constructor(private fb: FormBuilder, private Addbatchesservice: AddBatchesService, private loginservice: LoginService){}
  private existing_mentor?=this.loginservice.getUser();

  ngOnInit(): void {
    this.AddBatchForm = this.fb.group({
      MentorId: [''],
      Domain: ['', Validators.required],
      Description: ['', Validators.required],
      Employee_info_Excel_File: [null, Validators.required] ,
      Employee_info_Excel:[null]// A
    });

  }


  onSubmit(): void {
    if (this.AddBatchForm.valid) {

      const formData = this.AddBatchForm.value;
      if (this.existing_mentor != null) {
        // Check if this.existing_mentor is a valid number string
        const mentorId = Number(this.existing_mentor);
        if (!isNaN(mentorId)) {
          // If it's a valid number, assign it to formData.MentorId
          formData.MentorId = mentorId;
        } }
             formData.Employee_info_Excel_File=this.selectedFile;
      const file = formData.Employee_info_Excel_File as File;

      // Convert file to byte array
      this.fileToBase64(file).subscribe(byteArray => {
      formData.Employee_info_Excel = byteArray;


        // Set ValidationDocs to null
      formData.Employee_info_Excel_File=null;



      this.Addbatchesservice.Addmentor(formData).subscribe(
        (response: any) => {
          console.log('Mentor profile added successfully:', response);
          // Optionally, you can navigate to another page or display a success message here
        },
        (error: any) => {
          console.log('Mentor profile Not added Error:', error);
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


