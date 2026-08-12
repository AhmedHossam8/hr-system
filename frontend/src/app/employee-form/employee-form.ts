import { Component, EventEmitter, OnInit, Output, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { CreateEmployee } from '../models/employee';
import { Department } from '../models/department';
import { DepartmentService } from '../services/department.service';
import { EmployeeService } from '../services/employee.service';

@Component({
  selector: 'app-employee-form',
  imports: [ReactiveFormsModule],
  templateUrl: './employee-form.html',
  styleUrl: './employee-form.css',
})
export class EmployeeForm implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly employeeService = inject(EmployeeService);
  private readonly departmentService = inject(DepartmentService);

  departments = signal<Department[]>([]);
  submitting = signal(false);
  message = signal('');

  @Output() employeeCreated = new EventEmitter<void>();

  employeeForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', Validators.required],
    position: ['', Validators.required],
    departmentId: [null as number | null, Validators.required],
    salary: [0, [Validators.required, Validators.min(0)]],
    hireDate: ['', Validators.required],
  });

  get firstName() {
    return this.employeeForm.controls.firstName;
  }
  get lastName() {
    return this.employeeForm.controls.lastName;
  }
  get email() {
    return this.employeeForm.controls.email;
  }
  get phone() {
    return this.employeeForm.controls.phone;
  }
  get position() {
    return this.employeeForm.controls.position;
  }
  get departmentId() {
    return this.employeeForm.controls.departmentId;
  }
  get salary() {
    return this.employeeForm.controls.salary;
  }
  get hireDate() {
    return this.employeeForm.controls.hireDate;
  }

  ngOnInit(): void {
    this.loadDepartments();
  }

  loadDepartments(): void {
    this.departmentService.getDepartments().subscribe({
      next: (data) => this.departments.set(data),
    });
  }

  onSubmit(): void {
    if (this.employeeForm.invalid) {
      return;
    }

    const value = this.employeeForm.value;
    const payload: CreateEmployee = {
      firstName: value.firstName ?? '',
      lastName: value.lastName ?? '',
      email: value.email ?? '',
      phone: value.phone ?? '',
      position: value.position ?? '',
      departmentId: Number(value.departmentId),
      salary: Number(value.salary),
      hireDate: value.hireDate ?? '',
    };

    this.submitting.set(true);
    this.message.set('');

    this.employeeService.createEmployee(payload).subscribe({
      next: () => {
        this.submitting.set(false);
        this.message.set('Employee created successfully!');
        this.employeeForm.reset();
        this.employeeCreated.emit();
      },
      error: (err) => {
        this.submitting.set(false);
        this.message.set('Failed to create employee: ' + err.message);
      },
    });
  }
}