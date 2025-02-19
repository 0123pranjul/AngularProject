import { Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Employee } from '../../Models/employee';
import { EmployeeService } from '../../Services/employee.service';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent implements OnInit {
  @ViewChild('myModal') model: ElementRef | undefined;
  employeeList: Employee[] = [];
  empservice = inject(EmployeeService);

  employeeForm!: FormGroup;

  constructor(private fb: FormBuilder) { }
  initializationForm() {
    this.employeeForm = this.fb.group({
      id: [0],
      name: ['', [Validators.required]],
      email: ['', [Validators.required]],
      mobile: ['', [Validators.required]],
      age: ['', [Validators.required]],
      salary: ['', [Validators.required]],
      status: [false, [Validators.required]]
    });
  }
  ngOnInit(): void {
    this.initializationForm();
    this.getEmployees();
  }
  openModel() {
    const empModel = document.getElementById('myModal');
    if (empModel != null) {
      empModel.style.display = 'block';
    }
  }
  closeModel() {
    this.initializationForm();
    if (this.model != null) {
      this.model.nativeElement.style.display = 'none'
    }

  }
  getEmployees() {
    this.empservice.getAllEmployees().subscribe((res) => {
      this.employeeList = res;
    })
  }

  formValues: any;
  onSubmit() {
    console.log(this.employeeForm.value);
    if (this.employeeForm.invalid) {
      alert('please Fill All Field');
      return;
    }
    if(this.employeeForm.value.id==0){
      this.formValues = this.employeeForm.value;
      this.empservice.addEmployee(this.formValues).subscribe((res) => {
        alert('Employee Added Successfully');
        this.getEmployees();
        this.employeeForm.reset();
        this.closeModel();
      });
    }
    else{
      this.formValues = this.employeeForm.value;
      this.empservice.updateEmployee(this.formValues).subscribe((res) => {
        alert('Employee Update Successfully');
        this.getEmployees();
        this.employeeForm.reset();
        this.closeModel();
      });
    }
   
  }
  onDelete(id: number){
    this.empservice.deleteEmployee(id).subscribe((res)=>{
      alert("Employee Deleted Successfully");
      this.getEmployees();
    });
  }
  OnEdit(Employee : Employee){
    this.openModel();
    this.employeeForm.patchValue(Employee)
  }
}
