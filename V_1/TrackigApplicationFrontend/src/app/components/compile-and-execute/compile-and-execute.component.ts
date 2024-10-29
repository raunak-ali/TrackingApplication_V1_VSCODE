import { HttpErrorResponse } from '@angular/common/http';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CompileAndExecuteService } from 'src/app/Services/compile-and-execute.service';
import { LoginService } from 'src/app/Services/login.service';
import { User } from '../../Models/user';
import { SubTask } from 'src/app/Models/sub-task';
import { HostListener } from '@angular/core';
import html2canvas from 'html2canvas';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NavigationService } from 'src/app/Services/navigation.service';
import * as JSZip from 'jszip';
import { AddProctered } from 'src/app/Models/add-proctered';
import { ProcteredService } from 'src/app/Services/proctered.service';
declare var MediaRecorder: any;

@Component({
  selector: 'app-compile-and-execute',
  templateUrl: './compile-and-execute.component.html',
  styleUrls: ['./compile-and-execute.component.css']
})
export class CompileAndExecuteComponent implements OnInit {

  mediaRecorder: any;
  chunks: any[] = [];
  recordedVideoUrl: SafeUrl | undefined;
  submitting = false;
  code: string = '';
  sampleInput: string = '';
  response: any;
  subtaskresponse: any;
  error!: string;
  subtaskid!: number;
  input: any;
  expectedoutput: any;
  actualoutput!: null;
  currentUser = this.loginservice.getUserFromSession();
  currenntUserid=this.loginservice.getUser();
  CurrentSubtask!: SubTask;
  Violations!:string|null;
  addproctered!:AddProctered;
  currennt_Userid!:number;




  UserId = this.currentUser.UserId;
  ERROR: any;
  navigationHistory: string[]=[];
  constructor(private route: ActivatedRoute,
    private fb: FormBuilder,
    private loginservice: LoginService,
    private router: Router,
    private compileAndExecuteService: CompileAndExecuteService,
    private sanitizer: DomSanitizer
    , private snackBar: MatSnackBar,
    private navigationService: NavigationService,
    private procteredService:ProcteredService

  ) { }
  ngOnInit(): void {

    this.navigationHistory = this.navigationService.getNavigationHistory();
    if (this.currenntUserid != null) {
      // Check if this.existing_mentor is a valid number string
      this.currennt_Userid = Number(this.currenntUserid);
    }
    this.subtaskid = +this.route.snapshot.params['subtaskid'];
    console.log(this.currentUser);

    this.compileAndExecuteService.GetSubTask(this.subtaskid).subscribe(
      (res: any) => {
        this.CurrentSubtask = res;
        console.log("SubTask Object", res);

        if (this.CurrentSubtask && this.CurrentSubtask.isProctored === true) {
          this.startScreenRecording();
        }
      },
      (error: HttpErrorResponse) => {
        this.subtaskresponse = null;
        if (error.status === 400) {

          this.subtaskresponse = error.error;
          console.log(this.subtaskresponse); // Assign the error message to the property
        } else {
          this.error = 'An error occurred. Please try again later.'; // Generic error message
        }
      }
    );

    const codeTextarea = document.getElementById('codeTextarea') as HTMLTextAreaElement;
    const lineNumbersContainer = document.getElementById('lineNumbers');

    if (codeTextarea && lineNumbersContainer) { // Null check
      codeTextarea.addEventListener('input', () => {
        const lines = codeTextarea.value.split('\n').length;
        lineNumbersContainer.innerHTML = '';
        for (let i = 1; i <= lines; i++) {
          lineNumbersContainer.innerHTML += i + '<br>';
        }
      });
    }
    //Screen Recording Should start as soon a sthe page is loaded



  }
  showNavigationHistory: boolean = false;

  toggleNavigationHistory() {
    this.showNavigationHistory = !this.showNavigationHistory;
  }
  viewEmployee(userid:number) {
    this.router.navigate(['/UserProfile', userid]);
    }

    navigateToDahsBoard(){
      if(this.currentUser.Role==2){
        this.router.navigate(['AdminDashboard']);
      }
      else if(this.currentUser.Role==1){
        this.router.navigate(['Mentor_dashboard']);
      }
      else{
        this.router.navigate(['Employee_DashBoard']);
      }
    }
  startScreenRecording() {
    navigator.mediaDevices.getDisplayMedia({ video: true }).then(stream => {
      this.mediaRecorder = new MediaRecorder(stream);
      this.mediaRecorder.ondataavailable = (event: { data: any; }) => {
        this.chunks.push(event.data);
      };
      this.mediaRecorder.start();
    }).catch(error => {
      console.error('Error accessing display media:', error);
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
  CompileCode() {
    // Call the service method to submit the code and sample input
    this.compileAndExecuteService.CompileCode(this.code, this.sampleInput, this.subtaskid)
      .subscribe(
        (res: any) => {
          this.response = res.$values; // Assign the response to the property
          console.log("RES", res.$values);
          this.input = res.input;
          this.expectedoutput = res.expectedOutput;
          this.actualoutput = res.actualOutput;
          this.ERROR = null;
          console.log(this.input, this.expectedoutput, this.actualoutput);
        },
        (error: HttpErrorResponse) => {
          this.response = null;
          if (error.status === 400) {
            this.input = null;
            this.expectedoutput = null;
            this.actualoutput = null;
            this.ERROR = error.error;
            this.response = null;
            console.log("ERROR", this.ERROR); // Assign the error message to the property
          } else {
            this.ERROR = 'An error occurred. Please try again later.'; // Generic error message
          }
        }
      );
  }
recodingblob!:Blob;
  SubmitCode() {
    if(this.CurrentSubtask.isProctored==true){
    //Stop the screen Recording
    if (this.mediaRecorder && this.mediaRecorder.state !== 'inactive') {
      this.mediaRecorder.stop();
      this.mediaRecorder.onstop = () => {
        const blob = new Blob(this.chunks, { type: 'video/webm' });
        this.recodingblob=blob;
        this.recordedVideoUrl = this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(blob));
        //this.screenshots.push(blob);
      };
    }}
    //Send the Submitted code to the backend
    this.compileAndExecuteService.SubmitCode(this.code, this.sampleInput, this.subtaskid, this.UserId)
      .subscribe(
        (res: any) => {
          this.response = res.$values; // Assign the response to the property
          console.log('RES', res.$values);
          this.input = res.input;
          this.expectedoutput = res.expectedOutput;
          this.actualoutput = res.actualOutput;
          this.router.navigate(['/SubTaskSubmission',this.subtaskid]);

        },
        (error: HttpErrorResponse) => {
          this.response = null;
          if (error.status === 400) {
            this.input = null;
            this.expectedoutput = null;
            this.actualoutput = null;
            this.error = error.error;
            console.log('ERROR', this.error);
            this.response = error.error;
            console.log('RESPONSE', this.response); // Assign the error message to the property
          } else {
            this.error = 'An error occurred. Please try again later.'; // Generic error message
          }
        }
      );
  }

  compareStrings(expected: string, actual: string): boolean {
    // Convert the strings to lowercase and remove spaces
    const expectedCleaned = expected.toLowerCase().replace(/\s/g, '');
    const actualCleaned = actual.toLowerCase().replace(/\s/g, '');
    // Compare the cleaned strings
    return expectedCleaned === actualCleaned;
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

  //All of the triggers Added For the Assesment

  screenshots: Blob[] = [];

  @HostListener('window:blur')
  onTabSwitch() {
    if(this.CurrentSubtask.isProctored==true){
    console.log('User switched tab', this.screenshots);
    this.takeScreenshot();
    this.snackBar.open(`User Has Switched Tab and the Notification will be Sent to the Mentor`, 'Close', { duration: 3000 });
    }
  }
  @HostListener('document:contextmenu', ['$event'])
  onRightClick(event: MouseEvent) {
    if(this.CurrentSubtask.isProctored==true){
    console.log('User used right-click', this.screenshots);
    this.takeScreenshot();
    this.snackBar.open(`User Has Tried Using the Right Click and the Notification will be Sent to the Mentor`, 'Close', { duration: 3000 });

    event.preventDefault(); // Prevent default context menu
  }
  }

  @HostListener('document:paste', ['$event'])
  onPaste(event: ClipboardEvent) {
    if(this.CurrentSubtask.isProctored==true){
    console.log('User pasted content', this.screenshots);
    this.snackBar.open(`User Has tried Pasting Content and the Notification will be Sent to the Mentor`, 'Close', { duration: 3000 });

    this.takeScreenshot();
    event.preventDefault(); // Prevent default paste behavior
  }
}
  createZip(screenshots: Blob[], videoBlob: Blob): Promise<void> {
    return new Promise((resolve, reject) => {
      const zip = new JSZip();

      // Add screenshots to the ZIP file
      screenshots.forEach((screenshot, index) => {
        zip.file(`screenshot_${index + 1}.png`, screenshot);
      });

      // Add video to the ZIP file
      zip.file('recorded_video.mp4', videoBlob);

      // Generate the ZIP file asynchronously
      zip.generateAsync({ type: 'uint8array' })
        .then((zipData: Uint8Array) => {
          const dataArray = Array.from(zipData);

          const base64String = this.uint8ArrayToBase64(zipData);

          this.Violations = base64String;
          console.log("Violations",this.Violations);
          resolve(); // Resolve the Promise when ZIP generation is complete
        })
        .catch((error:any) => {
          reject(error); // Reject the Promise if there's an error
        });
    });


}


  takeScreenshot() {
    if(this.CurrentSubtask.isProctored==true){
    html2canvas(document.body).then(canvas => {
      canvas.toBlob(blob => {
        if (blob) {
          this.screenshots.push(blob);
          if(this.screenshots.length==4){
            //Do an auto submit
            //and save its submission with rating value=0
            //var temp=this.code;
            //this.code="";
            this.SubmitCode();
            //Make the violations File
              //Combine a Bolb of screenshots and recordings
              //Make a File of the Blob
              this.createZip(this.screenshots, this.recodingblob)
  .then(() => {
    // Code will proceed here only after createZip has completed execution
    // Access the generated ZIP data or perform further operations
     //Make a object f AddProctered
     this.addproctered = new AddProctered(); // Initialize addproctered

     console.log('USER ID',this.currennt_Userid);
      this.addproctered.userId=this.currennt_Userid;
      this.addproctered.subtaskid=this.subtaskid;
      this.addproctered.violations=this.Violations;

     //Send it To the AddProctered Service

     this.procteredService.AddProct(this.addproctered).subscribe(
       (response: any) => {
         console.log('addproctered updated successfully:', response);
         //this.router.navigate(['/SubTaskSubmission',this.subtaskid]);

         this.snackBar.open('addproctered SUcessfully Sent to mentor, Refresh page to view', 'Close', { duration: 3000 });

         // Optionally, you can navigate to another page or display a success message here
       },
       (error: any) => {
         console.log('addproctered  Not added Error:', error);
         this.snackBar.open(`addproctered  Added sucessfully`, 'Close', { duration: 3000 });
         // Handle error appropriately, such as displaying error messages to the user
       }
     );

  })
  .catch(error => {
    // Handle any errors that occur during createZip execution
    console.error('Error creating ZIP file:', error);
  });


            //and save a record of the submission with the actual rating value and the voilations file

            //These tasksubmission need to be approved by the mentor for the actual rating value to reflect
          }
        } else {
          console.error('Failed to capture screenshot.');
        }
      });
    }).catch(error => {
      console.error('Error capturing screenshot:', error);
    });}

  }




}
