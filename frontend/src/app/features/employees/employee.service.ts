import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import {
  EmployeeListDto,
  EmployeeDto,
  CreateEmployeeCommand,
  UpdateEmployeeCommand,
  PromoteEmployeeCommand,
  TransferEmployeeCommand,
  TerminateEmployeeCommand,
  PagedResult,
  DocumentDto,
  HistoryDto,
  SalaryStructure,
} from './employee.models';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/employees/Employees`;

  getEmployees(params: {
    page?: number;
    pageSize?: number;
    search?: string;
    department?: string;
    designation?: string;
    branch?: string;
    status?: string;
    joinDateFrom?: string;
    joinDateTo?: string;
  }): Observable<PagedResult<EmployeeListDto>> {
    let httpParams = new HttpParams()
      .set('pageNumber', (params.page ?? 1).toString())
      .set('pageSize', (params.pageSize ?? 10).toString());
    if (params.search) httpParams = httpParams.set('searchTerm', params.search);
    if (params.department) httpParams = httpParams.set('departmentId', params.department);
    if (params.designation) httpParams = httpParams.set('designationId', params.designation);
    if (params.status) httpParams = httpParams.set('status', params.status);

    return this.http.get<PagedResult<EmployeeListDto>>(this.apiUrl, { params: httpParams }).pipe(
      catchError(() => of({ items: [], totalCount: 0, page: 1, pageSize: 10, totalPages: 0 }))
    );
  }

  getEmployee(id: string): Observable<EmployeeDto> {
    return this.http.get<EmployeeDto>(`${this.apiUrl}/${id}`);
  }

  createEmployee(command: CreateEmployeeCommand): Observable<string> {
    return this.http.post<string>(this.apiUrl, command);
  }

  updateEmployee(id: string, command: UpdateEmployeeCommand): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, command);
  }

  deleteEmployee(id: string): Observable<void> {
    return this.changeStatus(id, { status: 'Terminated' } as any);
  }

  promoteEmployee(id: string, command: PromoteEmployeeCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/promote`, command);
  }

  transferEmployee(id: string, command: TransferEmployeeCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/transfer`, command);
  }

  terminateEmployee(id: string, command: TerminateEmployeeCommand): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/terminate`, command);
  }

  changeStatus(id: string, command: any): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/status`, command);
  }

  getEmployeeDocuments(id: string): Observable<DocumentDto[]> {
    return this.http.get<DocumentDto[]>(`${environment.apiUrl}/api/employees/EmployeeDocuments/employee/${id}`).pipe(
      catchError(() => of([]))
    );
  }

  uploadDocument(id: string, file: File): Observable<void> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<void>(`${environment.apiUrl}/api/employees/EmployeeDocuments/employee/${id}`, formData);
  }

  getEmployeeHistory(id: string): Observable<HistoryDto[]> {
    return this.http.get<HistoryDto[]>(`${environment.apiUrl}/api/employees/EmployeeHistory/employee/${id}`).pipe(
      catchError(() => of([]))
    );
  }

  getEmployeeSalary(id: string): Observable<SalaryStructure> {
    return this.http.get<SalaryStructure>(`${environment.apiUrl}/api/employees/EmployeeSalary/employee/${id}`);
  }
}
