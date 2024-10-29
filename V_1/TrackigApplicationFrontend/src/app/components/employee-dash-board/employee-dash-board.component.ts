import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { GetBatchesService } from 'src/app/Services/get-batches.service';
import { LoginService } from 'src/app/Services/login.service';
import { NavigationService } from 'src/app/Services/navigation.service';

@Component({
  selector: 'app-employee-dash-board',
  templateUrl: './employee-dash-board.component.html',
  styleUrls: ['./employee-dash-board.component.css']
})
export class EmployeeDashBoardComponent {
  currentUser!:any;
  navigationHistory: string[]=[];


  constructor(private getbatchesservice: GetBatchesService,
    private loginservice :LoginService,
    private router: Router,
    private navigationService: NavigationService
    ){
      this.navigationHistory = this.navigationService.getNavigationHistory();

      this.currentUser = this.loginservice.getUserFromSession();

      console.log(this.currentUser);

    }
  navigateTo(route: string): void {
    this.router.navigate([route]);
  }
  viewEmployee(userid:number) {
    this.router.navigate(['/UserProfile', userid]);
    }
    showNavigationHistory: boolean = false;

    toggleNavigationHistory() {
      this.showNavigationHistory = !this.showNavigationHistory;
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
  logout(): void {
    this.loginservice.clearUser();
    this.loginservice.clearToken();
    this.loginservice.clearcurrentUser();
    this.router.navigate(["Login"]);
    // Implement logout functionality
  }

}
