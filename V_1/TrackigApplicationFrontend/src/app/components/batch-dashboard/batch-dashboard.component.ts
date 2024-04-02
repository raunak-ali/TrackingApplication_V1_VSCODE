import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GetTask } from 'src/app/Models/get-task';
import { GetTasksService } from 'src/app/Services/get-tasks.service';

@Component({
  selector: 'app-batch-dashboard',
  templateUrl: './batch-dashboard.component.html',
  styleUrls: ['./batch-dashboard.component.css']
})
export class BatchDashboardComponent implements OnInit {
  batchId!: number;
  AllTasks!:GetTask[];
  constructor(private route: ActivatedRoute,private gettasksservice:GetTasksService) { }

  ngOnInit(): void {
    // Retrieve the batchId parameter from the route
    this.batchId = +this.route.snapshot.params['batchId'];
    this.FetchTasks();

    // Now you can use this.batchId in your component logic
    console.log('Batch ID:', this.batchId);
  }
  FetchTasks(){
    this.gettasksservice.Getall(this.batchId).subscribe(
      (data: GetTask[]) => {
        this.AllTasks = data;
        //this.createChart();
        console.log(this.AllTasks);
      },
      (error) => {
        console.error('Error fetching transactions:', error);
      }
    );
  }

}
