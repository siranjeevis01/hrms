import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  DashboardStats,
  Activity,
  Event,
  ChartData,
  PayrollSummary,
} from './dashboard.models';

@Injectable({ providedIn: 'root' })
export class DashboardService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/dashboard`;

  getDashboardStats(): Observable<DashboardStats> {
    return this.http.get<DashboardStats>(`${this.apiUrl}/stats`);
  }

  getRecentActivities(): Observable<Activity[]> {
    return this.http.get<Activity[]>(`${this.apiUrl}/activities`);
  }

  getUpcomingEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.apiUrl}/events`);
  }

  getEmployeeChart(): Observable<ChartData> {
    return this.http.get<ChartData>(`${this.apiUrl}/charts/employees`);
  }

  getAttendanceChart(): Observable<ChartData> {
    return this.http.get<ChartData>(`${this.apiUrl}/charts/attendance`);
  }

  getPayrollSummary(): Observable<PayrollSummary> {
    return this.http.get<PayrollSummary>(`${this.apiUrl}/payroll-summary`);
  }
}
