import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginService } from 'src/app/Services/login.service';
import { TaskSubmissionsService } from '../../Services/task-submissions.service';
import { FormBuilder, Validators } from '@angular/forms';
import { AddTaskSubmissions, StatusEnum } from 'src/app/Models/add-task-submissions';
import { Observable } from 'rxjs';
import { AddTaskSubmissionsService } from 'src/app/Services/add-task-submissions.service';
import { AddNewRatingService } from 'src/app/Services/add-new-rating.service';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-task-submissions',
  templateUrl: './task-submissions.component.html',
  styleUrls: ['./task-submissions.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed,void', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],

})


export class TaskSubmissionsComponent implements OnInit {
  //taskId!: number;
  commentsOptions: string[] =  Object.keys(Comments).filter(key => !isNaN(Number(Comments[key as keyof typeof Comments]))) as string[];
  userid!: any;
  subtaskid!: number;
  allSubmissions: any;
  submissionDetails: any;
  rating: any;
  statusOptions!: StatusEnum[];
  addTaskForm: any;
  selectedFile: any;
  role!: string | null;
  combinedData: any[] = [];
  ratingform: any;
  ratingValue: any;
  comments: any;

  currentUser = this.loginservice.getUserFromSession();
  constructor(private loginservice: LoginService,
    private router: Router,
    private route: ActivatedRoute,
    private taskSubmissionsService: TaskSubmissionsService,
    private formBuilder: FormBuilder,
    private addTaskSubmissionsService: AddTaskSubmissionsService,
    private ratingService: AddNewRatingService) { }
  ngOnInit(): void {

    this.statusOptions = Object.values(StatusEnum);
    this.subtaskid = +this.route.snapshot.params['subtaskid'];
    this.userid = this.loginservice.getUser();
    if (this.userid != null) {
      // Check if this.existing_mentor is a valid number string
      this.userid = Number(this.userid);
      if (!isNaN(this.userid)) {
        this.userid = this.userid;
      }
    }
    this.role = sessionStorage.getItem('Role');
    if (this.role == "Employee") {

      //If the emember logged is is employee follow this logic
      this.FetchAsllSubmissiosnOfThatTask();
      this.buildForm();
    }
    else {
      this.buildratingform();

      //If the Logged in member is s Mentor or an Admin the  follow this Logic
      this.FetchSubmissiosnsOfThatTAsk()

    }
  }
  submitRating(taskSubmissionId: number, ratedTo: number): void {


    // Set the value of TaskSubmissionId and RatedTo fields in the form
    this.ratingform.patchValue({
      TaskSubmissionId: taskSubmissionId,
      RatedTo: ratedTo,
      Comments: parseInt(this.ratingform.get('Comments').value, 10)
    });

    // Call the rating service method to submit the rating
    this.ratingService.AddRating(this.ratingform.value).subscribe(
      (response: any) => {
        console.log('Submission added successfully:', response);
        // Optionally, you can navigate to another page or display a success message here
      },
      (error: any) => {
        console.log('Submissions Not added Error:', error);
        // Handle error appropriately, such as displaying error messages to the user
      }
    );

  }

  buildratingform() {
    this.ratingform = this.formBuilder.group({
      RatedBy: [this.userid],
      RatedTo: [],
      TaskSubmissionId: [null, Validators.required],
      RatingValue: [null, Validators.required],
      Comments: [null, Validators.required]
    });
  }

  FetchSubmissiosnsOfThatTAsk() {
    this.taskSubmissionsService.GetAllForMentor(this.subtaskid).subscribe(
      (response) => {
        // Parse the JSON response
        // Parse the JSON response
        const result = JSON.parse(response.result);


        // Iterate over each object in the result array
        for (const item of result) {
          // Extract SubmissionDetails and Rating from each item
          const submissionDetails = item.SubmissionDetails;
          const rating = item.Rating;

          // Push the combined object into the array
          this.combinedData.push({ submissionDetails, rating });
        }

        console.log(this.combinedData);

        // Handle the data as needed
        // For example, you can assign them to component properties
        //this.submissionDetails =combinedData;

        // Handle the data as needed
      },
      (error) => {
        console.error(error); // Handle errors here
      }
    );
    //Fetch the submissions of all the Users for thsi particular task

  }
  FetchAsllSubmissiosnOfThatTask() {
    //Fetch the submission made by the logged in user for the selected task
    this.taskSubmissionsService.Getall(this.subtaskid, this.userid).subscribe(
      (response) => {
        // Parse the JSON response
        const result = JSON.parse(response.result);

        // Extract SubmissionDetails and Rating
        this.submissionDetails = result.SubmissionDetails;
        this.rating = result.Rating;

        console.log(this.submissionDetails);
        console.log(this.rating);

        // Handle the data as needed
      },
      (error) => {
        console.error(error); // Handle errors here
      }
    );
  }

  buildForm(): void {
    this.addTaskForm = this.formBuilder.group({
      submittedFileName: [null, Validators.required],
      FileUpload: [null],
      FileUploadSubmission: [null],

    });
  }

  onSubmit(): void {
    if (this.addTaskForm.valid) {
      const formData = this.addTaskForm.value;
      formData.UserId = this.userid;
      formData.subtaskid = this.subtaskid;
      formData.status = 0;
      formData.SubTaskSubmitteddOn = new Date();

      console.log(formData);

      formData.FileUpload = this.selectedFile;
      const file = formData.FileUpload as File;

      // Convert file to byte array
      this.fileToBase64(file).subscribe(byteArray => {
        formData.FileUploadSubmission = byteArray;


        // Set ValidationDocs to null
        formData.FileUpload = null;



        this.addTaskSubmissionsService.AddSubmission(formData).subscribe(
          (response: any) => {
            console.log('Submission added successfully:', response);
            // Optionally, you can navigate to another page or display a success message here
          },
          (error: any) => {
            console.log('Submissions Not added Error:', error);
            // Handle error appropriately, such as displaying error messages to the user
          }
        );
      });
    }
  }
  // Send formData to your API or perform other actions

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
  showRatingForms: { [key: string]: boolean } = {};

  showRatingForm(taskSubmissionId: number, userId: number): void {
    this.showRatingForms[taskSubmissionId + userId] = !this.showRatingForms[taskSubmissionId + userId];
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
  dataSource = this.combinedData;
  displayedColumns: string[] = ['SubmittedBy', 'File Upload' ];

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

}
export enum Comments {
  Average = 0,
  VeryGood = 1,
  Good = 2,
  BelowAverage = 3,
  Bad = 4
}

