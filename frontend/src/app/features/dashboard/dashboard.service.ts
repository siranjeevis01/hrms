import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
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
    return this.http.get<any>(`${this.apiUrl}/dashboards/stats`).pipe(
      map((data) => this.mapDashboardStats(data)),
      catchError(() => of({
        totalEmployees: 0,
        activeEmployees: 0,
        newHires: 0,
        attritionRate: 0,
        avgAttendance: 0,
        pendingLeaves: 0,
        openPositions: 0,
        totalDepartments: 0,
      }))
    );
  }

  getRecentActivities(): Observable<Activity[]> {
    return this.http.get<any>(`${this.apiUrl}/analytics`).pipe(
      map((data) => {
        if (Array.isArray(data)) return data;
        if (data?.activities) return data.activities;
        if (data?.recentActivities) return data.recentActivities;
        return [];
      }),
      catchError(() => of([]))
    );
  }

  getUpcomingEvents(): Observable<Event[]> {
    return this.http.get<any>(`${this.apiUrl}/dashboards`).pipe(
      map((data) => {
        if (Array.isArray(data)) return data;
        if (data?.events) return data.events;
        if (data?.upcomingEvents) return data.upcomingEvents;
        return [];
      }),
      catchError(() => of([]))
    );
  }

  getEmployeeChart(): Observable<ChartData> {
    return this.http.get<any>(`${this.apiUrl}/analytics`).pipe(
      map((data) => this.mapChartData(data, 'employee')),
      catchError(() => of({ labels: [], datasets: [] }))
    );
  }

  getAttendanceChart(): Observable<ChartData> {
    return this.http.get<any>(`${this.apiUrl}/analytics`).pipe(
      map((data) => this.mapChartData(data, 'attendance')),
      catchError(() => of({ labels: [], datasets: [] }))
    );
  }

  getPayrollSummary(): Observable<PayrollSummary> {
    return this.http.get<any>(`${this.apiUrl}/dashboards/stats`).pipe(
      map((data) => this.mapPayrollSummary(data)),
      catchError(() => of({
        totalPayroll: 0,
        averageSalary: 0,
        highestSalary: 0,
        departmentWise: [],
        month: new Date().toISOString().slice(0, 7),
      }))
    );
  }

  private mapDashboardStats(data: any): DashboardStats {
    if (!data) {
      return {
        totalEmployees: 0, activeEmployees: 0, newHires: 0,
        attritionRate: 0, avgAttendance: 0, pendingLeaves: 0,
        openPositions: 0, totalDepartments: 0,
      };
    }
    return {
      totalEmployees: data.totalEmployees ?? data.TotalEmployees ?? 0,
      activeEmployees: data.activeEmployees ?? data.ActiveEmployees ?? 0,
      newHires: data.newHires ?? data.NewHires ?? 0,
      attritionRate: data.attritionRate ?? data.AttritionRate ?? 0,
      avgAttendance: data.avgAttendance ?? data.AvgAttendance ?? 0,
      pendingLeaves: data.pendingLeaves ?? data.PendingLeaves ?? 0,
      openPositions: data.openPositions ?? data.OpenPositions ?? 0,
      totalDepartments: data.totalDepartments ?? data.TotalDepartments ?? 0,
    };
  }

  private mapChartData(data: any, type: string): ChartData {
    if (!data) return { labels: [], datasets: [] };

    // If the API returns ChartData format directly
    if (data.labels && data.datasets) {
      return {
        labels: data.labels || [],
        datasets: (data.datasets || []).map((ds: any) => ({
          label: ds.label || '',
          data: ds.data || [],
          backgroundColor: ds.backgroundColor,
          borderColor: ds.borderColor,
          borderWidth: ds.borderWidth,
        })),
      };
    }

    // Try to extract department distribution from analytics data
    if (type === 'employee' && (data.departmentDistribution || data.departmentWise)) {
      const dist = data.departmentDistribution || data.departmentWise;
      if (Array.isArray(dist)) {
        return {
          labels: dist.map((d: any) => d.department || d.name || d.label || ''),
          datasets: [{
            label: 'Employees',
            data: dist.map((d: any) => d.count || d.employeeCount || d.value || 0),
          }],
        };
      }
    }

    // Try to extract attendance trend
    if (type === 'attendance' && (data.attendanceTrend || data.monthlyAttendance)) {
      const trend = data.attendanceTrend || data.monthlyAttendance;
      if (Array.isArray(trend)) {
        return {
          labels: trend.map((t: any) => t.month || t.label || ''),
          datasets: [{
            label: 'Attendance %',
            data: trend.map((t: any) => t.percentage || t.count || t.value || 0),
          }],
        };
      }
    }

    return { labels: [], datasets: [] };
  }

  private mapPayrollSummary(data: any): PayrollSummary {
    if (!data) {
      return {
        totalPayroll: 0, averageSalary: 0, highestSalary: 0,
        departmentWise: [], month: new Date().toISOString().slice(0, 7),
      };
    }
    return {
      totalPayroll: data.totalPayroll ?? data.TotalPayroll ?? 0,
      averageSalary: data.averageSalary ?? data.AverageSalary ?? 0,
      highestSalary: data.highestSalary ?? data.HighestSalary ?? 0,
      departmentWise: (data.departmentWise ?? data.DepartmentWise ?? []).map((d: any) => ({
        department: d.department ?? d.Department ?? '',
        totalSalary: d.totalSalary ?? d.TotalSalary ?? 0,
        employeeCount: d.employeeCount ?? d.EmployeeCount ?? 0,
      })),
      month: data.month ?? data.Month ?? new Date().toISOString().slice(0, 7),
    };
  }
}
