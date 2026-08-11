import { Component } from '@angular/core';
import { EmployeeList } from './employee-list/employee-list';

@Component({
  selector: 'app-root',
  imports: [EmployeeList],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = 'HR System';
}
