import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
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

  getMyAttendance(date: string): Observable<AttendanceRecord> {
    return this.http.get<AttendanceRecord>(`${this.apiUrl}/my-attendance/${date}`);
  }

  getAttendance(filters: AttendanceFilters): Observable<PagedResult<AttendanceRecord>> {
    let params = new HttpParams()
      .set('page', filters.page.toString())
      .set('pageSize', filters.pageSize.toString());
    if (filters.startDate) params = params.set('startDate', filters.startDate);
    if (filters.endDate) params = params.set('endDate', filters.endDate);
    if (filters.departmentId) params = params.set('departmentId', filters.departmentId);
    if (filters.status) params = params.set('status', filters.status);
    if (filters.employeeId) params = params.set('employeeId', filters.employeeId);
    return this.http.get<PagedResult<AttendanceRecord>>(this.apiUrl, { params }).pipe(
      catchError(() => of({ items: [], totalCount: 0, page: 1, pageSize: 10, totalPages: 0 }))
    );
  }

  getTodayAttendance(employeeId: string): Observable<AttendanceRecord> {
    return this.http.get<AttendanceRecord>(`${this.apiUrl}/today/${employeeId}`);
  }

  markCheckIn(request: MarkAttendanceRequest): Observable<AttendanceRecord> {
    return this.http.post<AttendanceRecord>(`${this.apiUrl}/check-in`, request);
  }

  markCheckOut(location?: string, latitude?: number, longitude?: number): Observable<void> {
    const body: any = {};
    if (location) body.location = location;
    if (latitude) body.latitude = latitude;
    if (longitude) body.longitude = longitude;
    return this.http.post<void>(`${this.apiUrl}/check-out`, body);
  }

  getTeamAttendance(managerId: string, date: string): Observable<AttendanceRecord[]> {
    return this.http.get<AttendanceRecord[]>(`${this.apiUrl}/team`, {
      params: { managerId, date },
    });
  }

  getAttendanceSummary(month: number, year: number): Observable<AttendanceSummary> {
    return this.http.get<AttendanceSummary>(`${this.apiUrl}/summary`, {
      params: { month: month.toString(), year: year.toString() },
    }).pipe(
      catchError(() => of({
        totalWorkingDays: 0, present: 0, absent: 0,
        late: 0, earlyExit: 0, overtime: 0, wfh: 0, halfDay: 0,
      }))
    );
  }

  getMonthlyAttendance(employeeId: string, year: number, month: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/monthly/${employeeId}`, {
      params: { year: year.toString(), month: month.toString() },
    });
  }

  getAttendanceReports(filters: ReportFilters): Observable<AttendanceReport[]> {
    let params = new HttpParams()
      .set('startDate', filters.startDate)
      .set('endDate', filters.endDate)
      .set('reportType', filters.reportType);
    if (filters.departmentId) params = params.set('departmentId', filters.departmentId);
    if (filters.employeeId) params = params.set('employeeId', filters.employeeId);
    return this.http.get<AttendanceReport[]>(`${this.apiUrl}/report`, { params }).pipe(
      catchError(() => of([]))
    );
  }
}
