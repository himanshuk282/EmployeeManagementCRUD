import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent {
  employeeDetails:Employee={
    id : '',
    name : '',
    email : '',
    phone : 0,
    salary : 0,
    department : ''
  };
  constructor(private router:Router, private route:ActivatedRoute,private employeeService:EmployeesService){}
  ngOnInit(){
    this.route.params.subscribe({
      next:(params)=>{
        //Get the Element from the Id
        const id = params['id'];

        if(id){
          //call api
          this.employeeService.getEmployee(id).subscribe({next:(response)=>{
            this.employeeDetails = response;
          }});
        }
      }
    });
  }
  updateEmployee(){
    this.employeeService.updateEmployee(this.employeeDetails.id,this.employeeDetails).subscribe({
      next:(response)=>{
        this.router.navigate(['employees']);
      },
      error:(response)=>{
        console.log(response);
      }
    });
  }
  deleteEmployee(id:string){
    this.employeeService.deleteEmployee(id).subscribe({
      next:(response)=>{
        this.router.navigate(['employees']);
      },
      error:(response)=>{
        console.log(response);
      }
    })
  }
}
