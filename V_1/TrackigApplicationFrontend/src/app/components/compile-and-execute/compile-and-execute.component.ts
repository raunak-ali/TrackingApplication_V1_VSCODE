import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CompileAndExecuteService } from 'src/app/Services/compile-and-execute.service';
import { LoginService } from 'src/app/Services/login.service';
import { User } from '../../Models/user';
import { SubTask } from 'src/app/Models/sub-task';

@Component({
  selector: 'app-compile-and-execute',
  templateUrl: './compile-and-execute.component.html',
  styleUrls: ['./compile-and-execute.component.css']
})
export class CompileAndExecuteComponent  implements OnInit {


  code: string = '';
  sampleInput: string = '';
  response: any;
  subtaskresponse:any;
  error!: string;
  subtaskid!: number;
  input: any;
  expectedoutput: any;
  actualoutput!: null;
  currentUser = this.loginservice.getUserFromSession();
  CurrentSubtask!:SubTask;


UserId=this.currentUser.UserId;
  ERROR: any;
  constructor(private route: ActivatedRoute,
    private fb: FormBuilder,
    private loginservice :LoginService,
    private router: Router,
    private compileAndExecuteService:CompileAndExecuteService) { }
  ngOnInit(): void {


    this.subtaskid = +this.route.snapshot.params['subtaskid'];
    console.log(this.currentUser);

    this.compileAndExecuteService.GetSubTask(this.subtaskid)   .subscribe(
      (res: any) => {
       this.CurrentSubtask=res;
        console.log("SubTask Object",res);
      },
      (error: HttpErrorResponse) => {
        this.subtaskresponse=null;
        if (error.status === 400) {

          this.subtaskresponse=error.error;
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
    this.compileAndExecuteService.CompileCode(this.code, this.sampleInput,this.subtaskid)
    .subscribe(
      (res: any) => {
        this.response = res.$values; // Assign the response to the property
        console.log("RES",res.$values);
        this.input=res.input;
        this.expectedoutput=res.expectedOutput;
        this.actualoutput=res.actualOutput;
        this.ERROR=null;
        console.log(this.input,this.expectedoutput,this.actualoutput);
      },
      (error: HttpErrorResponse) => {
        this.response=null;
        if (error.status === 400) {
          this.input=null;
          this.expectedoutput=null;
          this.actualoutput=null;
          this.ERROR=error.error;
          this.response=null;
          console.log("ERROR",this.ERROR); // Assign the error message to the property
        } else {
          this.ERROR = 'An error occurred. Please try again later.'; // Generic error message
        }
      }
    );
  }

  SubmitCode() {
    this.compileAndExecuteService.SubmitCode(this.code, this.sampleInput,this.subtaskid,this.UserId)
    .subscribe(
      (res: any) => {
        this.response = res.$values; // Assign the response to the property
        console.log('RES',res.$values);
        this.input=res.input;
        this.expectedoutput=res.expectedOutput;
        this.actualoutput=res.actualOutput;
      },
      (error: HttpErrorResponse) => {
        this.response=null;
        if (error.status === 400) {
          this.input=null;
          this.expectedoutput=null;
          this.actualoutput=null;
          this.error=error.error;
          console.log('ERROR',this.error);
          this.response = error.error;
          console.log('RESPONSE',this.response); // Assign the error message to the property
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


}
