import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { GetBatchesService } from 'src/app/Services/get-batches.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-mentor-dashboard',
  templateUrl: './mentor-dashboard.component.html',
  styleUrls: ['./mentor-dashboard.component.css'],
  animations: [
    // Define your animations here
  ]
})
export class MentorDashboardComponent {

  constructor(private getbatchesservice: GetBatchesService,
    private loginservice :LoginService,
    private router: Router){}
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


}
