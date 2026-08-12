export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  position: string;
  departmentId: number;
  departmentName: string;
  salary: number;
  hireDate: string;
}

export interface CreateEmployee {
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  position: string;
  departmentId: number;
  salary: number;
  hireDate: string;
}
