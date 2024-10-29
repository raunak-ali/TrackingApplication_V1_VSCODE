import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetBatchesService } from '../../Services/get-batches.service';
import { LoginService } from 'src/app/Services/login.service';
import { GetBatches } from 'src/app/Models/get-batches';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Role } from '../../Models/user';
import { GetMentorsService } from 'src/app/Services/get-mentors.service';
import { GetUser } from 'src/app/Models/get-user';
import { NavigationService } from 'src/app/Services/navigation.service';

@Component({
  selector: 'app-get-batch',
  templateUrl: './get-batch.component.html',
  styleUrls: ['./get-batch.component.css']
})
export class GetBatchComponent implements OnInit {
  navigationHistory: string[]=[];
onMentorSelectChange() {
  this.filteredBatches = this.allBatches.filter(batch =>
    batch.mentorId==this.selectedMentor);


}

  error: string | undefined;
  allBatches!: GetBatches[];
  filteredBatches: GetBatches[] = [];
searchQuery: string = '';
  getMentorID!: any;
  currentUser = this.loginservice.getUserFromSession();
  allMentors!: GetUser[];
  selectedMentor!:number;


  constructor( private getbatchesservice: GetBatchesService,
    private loginservice :LoginService,
    private router: Router,
    private snackBar: MatSnackBar,
    private getMentorsService:GetMentorsService,
    private navigationService: NavigationService


    ) {
      this.navigationHistory = this.navigationService.getNavigationHistory();

     }

    showNavigationHistory: boolean = false;

    toggleNavigationHistory() {
      this.showNavigationHistory = !this.showNavigationHistory;
    }
  ngOnInit(): void {
    this.getMentorID=this.loginservice.getUser();
    if (this.getMentorID != null) {
      // Check if this.existing_mentor is a valid number string
      this.getMentorID = Number(this.getMentorID);
      if (!isNaN(this.getMentorID)) {
        this.getMentorID=this.getMentorID;
      }
    }

   if(this.currentUser.Role == 0){
   this.fetchallBathes();}
   else if(this.currentUser.Role==1){
    this.fetchTransactions();
   }
   else{
    this.fetchBatchesforAdmin();
    this.fetchmentors();
   }



  }
  fetchBatchesforAdmin() {
    this.getbatchesservice.getBatch().subscribe(
      (data: any) => {
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.allBatches = data.$values;
          this.filteredBatches = this.allBatches;
          console.log(this.allBatches);
        } else {
          console.error('Unexpected data format:', data);
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
        this.snackBar.open(`Error Fetching Batches: ${{error}}`, 'Close', { duration: 3000 });

      }
    );
  }

  fetchallBathes() {
   this.getbatchesservice.Getall(this.getMentorID).subscribe(
    (data: any) => {
      // Ensure data.$values exists and is an array before accessing it
      if (Array.isArray(data.$values)) {
        this.allBatches = data.$values;
        this.filteredBatches = this.allBatches;

        console.log(this.allBatches);
      } else {
        console.error('Unexpected data format:', data);
      }
    },
    (error) => {
      console.error('Error fetching batches:', error);
      this.snackBar.open(`Error Fetching Batches: ${{error}}`, 'Close', { duration: 3000 });

    }
  );
  }

  fetchTransactions(): void {
    this.getbatchesservice.Getall(this.getMentorID).subscribe(
      (data: any) => {
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.allBatches = data.$values;
          this.filteredBatches = this.allBatches;

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
    this.router.navigate(['/Module_dashboard', batchId]);
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

filterBatches() {
  if (this.searchQuery.trim() === '') {
    this.filteredBatches = this.allBatches; // Display all batches when search query is empty
  } else {
    this.filteredBatches = this.allBatches.filter(batch =>
      batch.batchName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
      batch.domain.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }
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
        this.snackBar.open(`Unexpected format: ${{data}}`, 'Close', { duration: 3000 });

      }
    },
    (error) => {
      console.error('Error fetching batches:', error);
      this.snackBar.open(`Error fetching batches: ${{error}}`, 'Close', { duration: 3000 });


    }
  );
}

}
