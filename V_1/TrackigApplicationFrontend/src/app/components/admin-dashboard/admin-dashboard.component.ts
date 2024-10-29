import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { GetUser } from 'src/app/Models/get-user';
import { GetBatchesService } from 'src/app/Services/get-batches.service';
import { GetMentorsService } from 'src/app/Services/get-mentors.service';
import { LoginService } from 'src/app/Services/login.service';
import { NavigationService } from 'src/app/Services/navigation.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  navigationHistory: string[]=[];
  currentUser = this.loginservice.getUserFromSession();

RedirectToNewMentor() {
  //AddMentor
  this.router.navigate(['/AddMentor']);
}
RedirectToNewBatch() {
  //AddNewBatch
  this.router.navigate(['/AddNewBatch']);
}

showNavigationHistory: boolean = false;

toggleNavigationHistory() {
  this.showNavigationHistory = !this.showNavigationHistory;
}
  allMentors!: GetUser[];
  filteredMentors: GetUser[] = [];
searchQuery: string = '';
  constructor(private getbatchesservice: GetBatchesService,
    private loginservice :LoginService,
    private router: Router,
    private getMentorsService:GetMentorsService,
    private snackBar: MatSnackBar,
    private navigationService: NavigationService

    ){}
  ngOnInit(): void {
    this.navigationHistory = this.navigationService.getNavigationHistory();

    this.fetchmentors();
  }
  fetchmentors() {
    this.getMentorsService. GetMentors().subscribe(
      (data: any) => {
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.allMentors = data.$values;
          console.log("All Mentors",this.allMentors);
          this.filteredMentors = this.allMentors; // Display all batches when search query is empty


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

filterBatches() {
  if (this.searchQuery.trim() === '') {
    this.filteredMentors = this.allMentors; // Display all batches when search query is empty
  } else {
    this.filteredMentors = this.allMentors.filter(user =>
      user.name.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
      user.domain.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }
}
}
