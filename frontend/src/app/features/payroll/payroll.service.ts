import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  PayrollSummary,
  Payslip,
  PayslipDetail,
  PayrollRunResult,
  SalaryStructure,
  SalaryComponent,
  TaxCalculation,
  PayrollReport,
  PagedResult,
  PayslipFilters,
  ReportFilters,
} from './payroll.models';

@Injectable({ providedIn: 'root' })
export class PayrollService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/payroll`;

  getPayrollSummary(): Observable<PayrollSummary> {
    return this.http.get<PayrollSummary>(`${this.apiUrl}/runs`);
  }

  runPayroll(month: number, year: number): Observable<PayrollRunResult> {
    return this.http.post<PayrollRunResult>(`${this.apiUrl}/process`, { month, year });
  }

  getPayslips(filters: PayslipFilters): Observable<PagedResult<Payslip>> {
    let params = new HttpParams()
      .set('page', filters.page.toString())
      .set('pageSize', filters.pageSize.toString())
      .set('month', filters.month.toString())
      .set('year', filters.year.toString());
    if (filters.departmentId) params = params.set('departmentId', filters.departmentId);
    if (filters.employeeSearch) params = params.set('employeeSearch', filters.employeeSearch);
    return this.http.get<PagedResult<Payslip>>(`${this.apiUrl}/payslips`, { params });
  }

  getPayslip(id: string): Observable<PayslipDetail> {
    return this.http.get<PayslipDetail>(`${this.apiUrl}/payslips/${id}`);
  }

  downloadPayslip(id: string): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/payslips/${id}/download`, {
      responseType: 'blob',
    });
  }

  getSalaryStructures(): Observable<SalaryStructure[]> {
    return this.http.get<SalaryStructure[]>(`${this.apiUrl}/employee-salary`);
  }

  createSalaryStructure(command: { name: string; components: SalaryComponent[] }): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/employee-salary`, command);
  }

  getSalaryComponents(): Observable<SalaryComponent[]> {
    return this.http.get<SalaryComponent[]>(`${this.apiUrl}/salary-components`);
  }

  calculateTax(salary: number): Observable<TaxCalculation> {
    return this.http.get<TaxCalculation>(`${this.apiUrl}/tax`, {
      params: { salary: salary.toString() },
    });
  }

  getPayrollReports(filters: ReportFilters): Observable<PayrollReport[]> {
    let params = new HttpParams()
      .set('startDate', filters.startDate)
      .set('endDate', filters.endDate)
      .set('reportType', filters.reportType);
    if (filters.departmentId) params = params.set('departmentId', filters.departmentId);
    return this.http.get<PayrollReport[]>(`${this.apiUrl}/reports`, { params });
  }
}
