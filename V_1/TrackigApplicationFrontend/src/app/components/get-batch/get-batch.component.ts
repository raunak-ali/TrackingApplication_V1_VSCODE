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
}
