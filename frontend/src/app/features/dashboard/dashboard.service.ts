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
    return this.http.get<DashboardStats>(`${this.apiUrl}/Dashboards/stats`);
  }

  getRecentActivities(): Observable<Activity[]> {
    return this.http.get<Activity[]>(`${this.apiUrl}/Analytics`);
  }

  getUpcomingEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.apiUrl}/Dashboards`);
  }

  getEmployeeChart(): Observable<ChartData> {
    return this.http.get<ChartData>(`${this.apiUrl}/Analytics`);
  }

  getAttendanceChart(): Observable<ChartData> {
    return this.http.get<ChartData>(`${this.apiUrl}/Analytics`);
  }

  getPayrollSummary(): Observable<PayrollSummary> {
    return this.http.get<PayrollSummary>(`${this.apiUrl}/Dashboards/stats`);
  }
}
