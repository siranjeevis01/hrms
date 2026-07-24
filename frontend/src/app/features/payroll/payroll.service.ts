import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
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
    return this.http.get<PayrollSummary>(`${this.apiUrl}/runs`).pipe(
      catchError(() => of({
        totalEmployees: 0, processedAmount: 0, pendingAmount: 0,
        avgSalary: 0, totalDeductions: 0, totalAllowances: 0,
      }))
    );
  }

  runPayroll(month: number, year: number): Observable<PayrollRunResult> {
    return this.http.post<PayrollRunResult>(`${this.apiUrl}/process`, { month, year });
  }

  getPayslips(filters: PayslipFilters): Observable<PagedResult<Payslip>> {
    // Backend only supports per-employee/month/year lookup, return empty for list
    return of({ items: [], totalCount: 0, page: 1, pageSize: 10, totalPages: 0 });
  }

  getPayslip(employeeId: string, month: number, year: number): Observable<PayslipDetail> {
    return this.http.get<PayslipDetail>(`${this.apiUrl}/payslips/${employeeId}/${month}/${year}`);
  }

  downloadPayslip(employeeId: string, month: number, year: number): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/payslips/${employeeId}/${month}/${year}`, {
      responseType: 'blob',
    });
  }

  getSalaryStructures(): Observable<SalaryStructure[]> {
    // Backend doesn't have a list-all endpoint
    return of([]);
  }

  createSalaryStructure(command: { name: string; components: SalaryComponent[] }): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/employee-salary/assign`, command);
  }

  getSalaryComponents(): Observable<SalaryComponent[]> {
    return this.http.get<SalaryComponent[]>(`${this.apiUrl}/salary-components`).pipe(
      catchError(() => of([]))
    );
  }

  calculateTax(salary: number): Observable<TaxCalculation> {
    // Backend tax endpoint is config/declaration based, return empty
    return of({
      annualIncome: salary, taxableIncome: salary, oldRegimeTax: 0,
      newRegimeTax: 0, oldRegimeBreakdown: [], newRegimeBreakdown: [],
    });
  }

  getPayrollReports(filters: ReportFilters): Observable<PayrollReport[]> {
    // Backend has summary and cost-analysis, not a generic report list
    return this.http.get<PayrollReport[]>(`${this.apiUrl}/reports/summary`).pipe(
      catchError(() => of([]))
    );
  }
}
