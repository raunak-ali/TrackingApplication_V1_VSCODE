import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GetSubTask } from 'src/app/Models/get-sub-task';
import { SubTask } from 'src/app/Models/sub-task';
import { AddsubtaskServiceService } from 'src/app/Services/addsubtask-service.service';
import { GetSubTasksByTaskService } from 'src/app/Services/get-sub-tasks-by-task.service';
import { LoginService } from '../../Services/login.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Location } from '@angular/common';

//All Task details
//->(Like thier substask)
//->THe rating of each of these Subtasks
//->Overall Feedbcak of this Entire Task(In detail)
@Component({
  selector: 'app-taskboard',
  templateUrl: './taskboard.component.html',
  styleUrls: ['./taskboard.component.css']
})
export class TaskboardComponent implements OnInit {


  form!: FormGroup;
  taskId!: number;
  selectedFile: any;
  AllSubtasks!:SubTask[];
  userid!:any;
  showForm: boolean = false;
  currentUser = this.loginservice.getUserFromSession();

  constructor(
    private snackBar: MatSnackBar,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private addsubtaskServiceservice:AddsubtaskServiceService,
    private getSubTasksByTaskService:GetSubTasksByTaskService,
    private loginservice:LoginService,
    private router: Router,
    private location: Location) {}

  ngOnInit(): void {
    this.taskId = +this.route.snapshot.params['taskiId'];
    this.userid=this.loginservice.getUser();
    this.initializeForm();
    this.getSubTaskByTask();
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
  initializeForm(): void {
//AddSubtaskForm
    this.form = this.formBuilder.group({
      Title: ['', Validators.required],
      Description: ['', Validators.required],
      TaskId: this.taskId,
      FileUploadTaskFileUpload: [null], // Assuming this is for file uploads in Angular
      FileUploadTaskPdf: [null], // Assuming this is for binary data or string (like base64) for PDFs
      FileName:['',Validators.required],
      TestCases:[''], // Initialize TestCases as an empty FormArray
      TestCasesArray: this.formBuilder.array([]), // Initialize TestCasesString as an empty string
      isCodingProblem:[true]

    });
  }
  // Add a method to get TestCases FormArray
// Add a method to get TestCasesArray FormArray
get testCasesArray(): FormArray {
  return this.form.get('TestCasesArray') as FormArray;
}

addTestCase(): void {
  this.testCasesArray.push(this.createTestCase());
}

createTestCase(): FormGroup {
  return this.formBuilder.group({
    SampleInput: ['', Validators.required],
    ExpectedOutput: ['', Validators.required]
  });
}

onSubmit(): void {
  if (this.form.valid) {
    // Stringify the array value
    const testCasesString = JSON.stringify(this.testCasesArray.value);

    // Update the TestCases field in the form with the string representation
    this.form.patchValue({
      TestCases: testCasesString
    });
      this.form.removeControl('TestCasesArray');

      const formData=this.form.value;
      formData.FileUploadTaskFileUpload=this.selectedFile;
      const file = formData.FileUploadTaskFileUpload as File;

      // Convert file to byte array
      this.fileToBase64(file).subscribe(byteArray => {
      formData.FileUploadTaskPdf = byteArray;


        // Set ValidationDocs to null
      formData.FileUploadTaskFileUpload=null;



      this.addsubtaskServiceservice.Addsubtask(formData).subscribe(
        (response: any) => {
          console.log('Subtask profile added successfully:', response);
          this.snackBar.open(`Subtask profile added successfully:`, 'Close', { duration: 3000 });

          window.location.reload();

          // Optionally, you can navigate to another page or display a success message here
        },
        (error: any) => {
          console.log('Subtask profile Not added Error:', error);
          this.snackBar.open(`Error  ${{error}}`, 'Close', { duration: 3000 });


          // Handle error appropriately, such as displaying error messages to the user
        }
      );
    });
      // Handle form submission here, e.g., send data to backend
      console.log(this.form.value);
    } else {
      // Form is invalid, display validation errors
      console.log('Form is invalid');
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

  getSubTaskByTask(){
    this.getSubTasksByTaskService.Getall(this.taskId).subscribe(
      (data: any) => {
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.AllSubtasks = data.$values;
          console.log(this.AllSubtasks);
        } else {
          console.error('Unexpected data format:', data);
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
        this.snackBar.open(`Errro : ${{error}}`, 'Close', { duration: 3000 });

      }
    );

  }

  downloadFileFromByteArrayString(byteArrayString: string, fileName: string | null) {
    const byteArray = Uint8Array.from(atob(byteArrayString), c => c.charCodeAt(0));
    const blob = new Blob([byteArray]);
    const url = URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    if (fileName) { // Check if fileName is not null or undefined
      link.download = fileName;
    }
    document.body.appendChild(link);
    link.click();
    URL.revokeObjectURL(url);
    document.body.removeChild(link);
}

  downloadFile(byteArray: string | Uint8Array | undefined, fileName: string) {
    if (byteArray) {
      const byteArrayString = typeof byteArray === 'string' ? byteArray : this.uint8ArrayToBase64(byteArray);
      this.downloadFileFromByteArrayString(byteArrayString, fileName);
    } else {
      console.error("Byte array is undefined.");
    }
  }

  uint8ArrayToBase64(array: Uint8Array): string {
    let binary = '';
    for (let i = 0; i < array.length; i++) {
      binary += String.fromCharCode(array[i]);
    }
    return window.btoa(binary);
  }

  SubTaskSubmissions(subTaskId: any) {

    this.router.navigate(['/SubTaskSubmission',subTaskId]);


  }
  toggleForm(): void {
    this.showForm = !this.showForm;
  }
}
