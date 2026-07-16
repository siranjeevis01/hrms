import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  LeaveRequest,
  LeaveBalance,
  LeaveType,
  Holiday,
  LeaveCalendarEntry,
  PagedResult,
  LeaveFilters,
  ApplyLeaveCommand,
} from './leave.models';

@Injectable({ providedIn: 'root' })
export class LeaveService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/leave`;

  applyLeave(command: ApplyLeaveCommand): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}`, command);
  }

  getMyLeaves(filters: LeaveFilters): Observable<PagedResult<LeaveRequest>> {
    let params = new HttpParams()
      .set('page', filters.page.toString())
      .set('pageSize', filters.pageSize.toString());
    if (filters.status) params = params.set('status', filters.status);
    if (filters.leaveType) params = params.set('leaveType', filters.leaveType);
    if (filters.startDate) params = params.set('startDate', filters.startDate);
    if (filters.endDate) params = params.set('endDate', filters.endDate);
    return this.http.get<PagedResult<LeaveRequest>>(`${this.apiUrl}/my`, { params });
  }

  cancelLeave(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/cancel`, {});
  }

  getTeamLeaves(filters: LeaveFilters): Observable<PagedResult<LeaveRequest>> {
    let params = new HttpParams()
      .set('page', filters.page.toString())
      .set('pageSize', filters.pageSize.toString());
    if (filters.status) params = params.set('status', filters.status);
    if (filters.departmentId) params = params.set('departmentId', filters.departmentId);
    if (filters.startDate) params = params.set('startDate', filters.startDate);
    if (filters.endDate) params = params.set('endDate', filters.endDate);
    return this.http.get<PagedResult<LeaveRequest>>(`${this.apiUrl}/team`, { params });
  }

  approveLeave(id: string, comments: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/approve`, { comments });
  }

  rejectLeave(id: string, comments: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/reject`, { comments });
  }

  getLeaveBalances(employeeId: string): Observable<LeaveBalance[]> {
    return this.http.get<LeaveBalance[]>(`${this.apiUrl}/balances/${employeeId}`);
  }

  getLeaveTypes(): Observable<LeaveType[]> {
    return this.http.get<LeaveType[]>(`${this.apiUrl}/types`);
  }

  getHolidays(year: number): Observable<Holiday[]> {
    return this.http.get<Holiday[]>(`${this.apiUrl}/holidays`, {
      params: { year: year.toString() },
    });
  }

  getLeaveCalendar(startDate: string, endDate: string): Observable<LeaveCalendarEntry[]> {
    return this.http.get<LeaveCalendarEntry[]>(`${this.apiUrl}/calendar`, {
      params: { startDate, endDate },
    });
  }
}
