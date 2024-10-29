import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { GetModules } from 'src/app/Models/get-modules';
import { AddModuleServicesService } from 'src/app/Services/add-module-services.service';
import { GetModuleServicesService } from 'src/app/Services/get-module-services.service';
import { LoginService } from 'src/app/Services/login.service';
import { NavigationService } from 'src/app/Services/navigation.service';

@Component({
  selector: 'app-module-dashboard',
  templateUrl: './module-dashboard.component.html',
  styleUrls: ['./module-dashboard.component.css']
})
export class ModuleDashboardComponent implements OnInit {


  error: string | undefined;
  allModules!: GetModules[];
  filteredModules: GetModules[] = [];
  searchQuery: string = '';
  batchId = +this.route.snapshot.params['batchId'];
  currentUser = this.loginservice.getUserFromSession();
  AddNewModuleForm: any;
  showForm:boolean=false;
  navigationHistory: string[]=[];
  @ViewChild('noModules') noModules!: TemplateRef<any>;


  constructor(private getModuleServicesService: GetModuleServicesService,
    private loginservice: LoginService,
    private router: Router,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private addModuleServicesService:AddModuleServicesService,
    private navigationService: NavigationService


  ) { }
  showNavigationHistory: boolean = false;

toggleNavigationHistory() {
  this.showNavigationHistory = !this.showNavigationHistory;
}
  ngOnInit(): void {

    this.navigationHistory = this.navigationService.getNavigationHistory();


    //Fetch the Batchid
    //this.batchId = +this.route.snapshot.params['batchId'];

    //Send the batchid to the get moduleservice

    this.fetchModules();
    //Assigned the modules to AllModules



    //Add new Module Form
    this.AddNewModuleForm = this.fb.group({
      ModuleName: ['',Validators.required],
      Description: ['',Validators.required],
      BatchId:[this.batchId]
    })
  }






  viewFeedback() {
    this.router.navigate(['/Module_Feedback', this.batchId]);
    }



  fetchModules(): void {
    this.getModuleServicesService.GetModules(this.batchId).subscribe(
      (data: any) => {
        // Ensure data.$values exists and is an array before accessing it
        if (Array.isArray(data.$values)) {
          this.allModules = data.$values;
          this.filteredModules = this.allModules;

          console.log(this.allModules);
        } else {
          console.error('Unexpected data format:', data);
        }
      },
      (error) => {
        console.error('Error fetching batches:', error);
      }
    );
  }

  viewTasks(moduleid: any) {
    this.router.navigate(['/Batch_dashboard', this.batchId, moduleid]);
  }




  filterModules() {
    if (this.searchQuery.trim() === '') {
      this.filteredModules = this.allModules; // Display all batches when search query is empty
    } else {
      this.filteredModules = this.allModules.filter(module =>
        module.moduleName.toLowerCase().includes(this.searchQuery.toLowerCase()));
    }
  }


  navigateTo(route: string): void {
    this.router.navigate([route]);
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
  viewEmployee(userid:number) {
    this.router.navigate(['/UserProfile', userid]);
    }

    OnSubmit(){
      if (this.AddNewModuleForm.valid) {

        this.addModuleServicesService.AddnewModule(this.AddNewModuleForm.value).subscribe(
          (response: any) => {
            console.log('Module added successfully:', response);
            this.snackBar.open(`Module added Sucessfully,Refresh page to view:`, 'Close', { duration: 3000 });
            window.location.reload();


            // Optionally, you can navigate to another page or display a success message here
          },
          (error: any) => {
            console.log('Module Not added Error:', error);
            this.snackBar.open(`Module Not added Error:`, 'Close', { duration: 3000 });
            // Handle error appropriately, such as displaying error messages to the user
          }
        );
      }

    }
    getLinkName(link: string): string {
      // Customize this function to format the link name as needed
      // For example, you might want to remove slashes or decode URL parts
      return link.replace(/\//g, ' ').trim();
    }
}
