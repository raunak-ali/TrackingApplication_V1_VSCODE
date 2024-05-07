import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { GetUser } from 'src/app/Models/get-user';
import { GetBatchesService } from 'src/app/Services/get-batches.service';
import { GetMentorsService } from 'src/app/Services/get-mentors.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {


  allMentors!: GetUser[];
  constructor(private getbatchesservice: GetBatchesService,
    private loginservice :LoginService,
    private router: Router,
    private getMentorsService:GetMentorsService,
    private snackBar: MatSnackBar
    ){}
  ngOnInit(): void {
    this.fetchmentors();
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
  viewEmployee(userid:number) {
    this.router.navigate(['/UserProfile', userid]);
    }
}
