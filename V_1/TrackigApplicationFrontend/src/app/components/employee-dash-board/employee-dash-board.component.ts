import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { GetBatchesService } from 'src/app/Services/get-batches.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-employee-dash-board',
  templateUrl: './employee-dash-board.component.html',
  styleUrls: ['./employee-dash-board.component.css']
})
export class EmployeeDashBoardComponent {
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
