import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  AttendanceRecord,
  AttendanceSummary,
  AttendanceReport,
  MarkAttendanceRequest,
  PagedResult,
  AttendanceFilters,
  ReportFilters,
} from './attendance.models';

@Injectable({ providedIn: 'root' })
export class AttendanceService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/attendance/Attendance`;

  getAttendance(filters: AttendanceFilters): Observable<PagedResult<AttendanceRecord>> {
    let params = new HttpParams()
      .set('page', filters.page.toString())
      .set('pageSize', filters.pageSize.toString());
    if (filters.startDate) params = params.set('startDate', filters.startDate);
    if (filters.endDate) params = params.set('endDate', filters.endDate);
    if (filters.departmentId) params = params.set('departmentId', filters.departmentId);
    if (filters.status) params = params.set('status', filters.status);
    if (filters.employeeId) params = params.set('employeeId', filters.employeeId);
    return this.http.get<PagedResult<AttendanceRecord>>(`${this.apiUrl}`, { params });
  }

  getMyAttendance(date: string): Observable<AttendanceRecord> {
    return this.http.get<AttendanceRecord>(`${this.apiUrl}/my`, {
      params: { date },
    });
  }

  markCheckIn(request: MarkAttendanceRequest): Observable<AttendanceRecord> {
    return this.http.post<AttendanceRecord>(`${this.apiUrl}/check-in`, request);
  }

  markCheckOut(location?: string, latitude?: number, longitude?: number): Observable<void> {
    let params = new HttpParams();
    if (location) params = params.set('location', location);
    if (latitude) params = params.set('latitude', latitude.toString());
    if (longitude) params = params.set('longitude', longitude.toString());
    return this.http.post<void>(`${this.apiUrl}/check-out`, {}, { params });
  }

  getTeamAttendance(managerId: string, date: string): Observable<AttendanceRecord[]> {
    return this.http.get<AttendanceRecord[]>(`${this.apiUrl}/team`, {
      params: { managerId, date },
    });
  }

  getAttendanceSummary(month: number, year: number): Observable<AttendanceSummary> {
    return this.http.get<AttendanceSummary>(`${this.apiUrl}/summary`, {
      params: { month: month.toString(), year: year.toString() },
    });
  }

  getAttendanceReports(filters: ReportFilters): Observable<AttendanceReport[]> {
    let params = new HttpParams()
      .set('startDate', filters.startDate)
      .set('endDate', filters.endDate)
      .set('reportType', filters.reportType);
    if (filters.departmentId) params = params.set('departmentId', filters.departmentId);
    if (filters.employeeId) params = params.set('employeeId', filters.employeeId);
    return this.http.get<AttendanceReport[]>(`${this.apiUrl}/report`, { params });
  }
}
