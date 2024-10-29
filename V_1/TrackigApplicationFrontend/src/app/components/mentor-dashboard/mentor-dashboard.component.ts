import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { GetBatchesService } from 'src/app/Services/get-batches.service';
import { LoginService } from 'src/app/Services/login.service';
import { NavigationService } from 'src/app/Services/navigation.service';

@Component({
  selector: 'app-mentor-dashboard',
  templateUrl: './mentor-dashboard.component.html',
  styleUrls: ['./mentor-dashboard.component.css'],
  animations: [
    // Define your animations here
  ]
})

export class MentorDashboardComponent {
  currentUser = this.loginservice.getUserFromSession();
  navigationHistory: string[]=[];
  showNavigationHistory: boolean = false;

  toggleNavigationHistory() {
    this.showNavigationHistory = !this.showNavigationHistory;
  }
  constructor(private getbatchesservice: GetBatchesService,
    private loginservice :LoginService,
    private router: Router,
    private navigationService: NavigationService
    ){
      this.navigationHistory = this.navigationService.getNavigationHistory();

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
  viewEmployee(userid:number) {
    this.router.navigate(['/UserProfile', userid]);
    }
    getLinkName(link: string): string {
      // Customize this function to format the link name as needed
      // For example, you might want to remove slashes or decode URL parts
      return link.replace(/\//g, ' ').trim();
    }


}
