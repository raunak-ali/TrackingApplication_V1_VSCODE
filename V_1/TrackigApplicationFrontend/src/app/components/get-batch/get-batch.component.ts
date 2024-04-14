import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetBatchesService } from '../../Services/get-batches.service';
import { LoginService } from 'src/app/Services/login.service';
import { GetBatches } from 'src/app/Models/get-batches';
import { Router } from '@angular/router';

@Component({
  selector: 'app-get-batch',
  templateUrl: './get-batch.component.html',
  styleUrls: ['./get-batch.component.css']
})
export class GetBatchComponent implements OnInit {

  error: string | undefined;
  allBatches!: GetBatches[];
  getMentorID!: any;
  currentUser = this.loginservice.getUserFromSession();


  constructor( private getbatchesservice: GetBatchesService,
    private loginservice :LoginService,
    private router: Router) { }

  ngOnInit(): void {
    this.getMentorID=this.loginservice.getUser();
    if (this.getMentorID != null) {
      // Check if this.existing_mentor is a valid number string
      this.getMentorID = Number(this.getMentorID);
      if (!isNaN(this.getMentorID)) {
        this.getMentorID=this.getMentorID;
      }
    }
   this.fetchTransactions();
  }

  fetchTransactions(): void {
    this.getbatchesservice.Getall(this.getMentorID).subscribe(
      (data: any) => {
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.allBatches = data.$values;
          console.log(this.allBatches);
        } else {
          console.error('Unexpected data format:', data);
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
      }
    );
  }

  viewBatch(batchId: number): void {
    // Navigate to another component with batchId as a parameter
    this.router.navigate(['/Batch_dashboard', batchId]);
}

addBatch(userid: number): void {
  // Navigate to another component with batchId as a parameter
  this.router.navigate(['/AddNewBatch', userid]);
}
addMentor(): void {
  // Navigate to another component with batchId as a parameter
  this.router.navigate(['/AddMentor']);
}
toggleDetails(batch: any): void {
  batch.showDetails = !batch.showDetails; // Toggle the showDetails property
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
