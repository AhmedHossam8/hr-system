import { Component, OnInit, signal, inject } from '@angular/core';
import { CurrencyPipe, DatePipe } from '@angular/common';

import { Employee } from '../models/employee';
import { EmployeeService } from '../services/employee.service';
import { EmployeeForm } from '../employee-form/employee-form';

@Component({
  selector: 'app-employee-list',
  imports: [CurrencyPipe, DatePipe, EmployeeForm],
  templateUrl: './employee-list.html',
  styleUrl: './employee-list.css',
})
export class EmployeeList implements OnInit {
  private readonly employeeService = inject(EmployeeService);

  employees = signal<Employee[]>([]);
  loading = signal(true);
  error = signal('');
  showForm = signal(false);

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.employeeService.getEmployees().subscribe({
      next: (data) => {
        this.employees.set(data);
        this.loading.set(false);
      },
      error: (err) => {
        this.error.set('Failed to load employees: ' + err.message);
        this.loading.set(false);
      },
    });
  }

  deleteEmployee(id: number): void {
    this.employeeService.deleteEmployee(id).subscribe({
      next: () => this.loadEmployees(),
      error: (err) => this.error.set('Failed to delete: ' + err.message),
    });
  }

  toggleForm(): void {
    this.showForm.update((visible) => !visible);
  }

  onEmployeeCreated(): void {
    this.loadEmployees();
    this.showForm.set(false);
  }
}
