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
  private apiUrl = `${environment.apiUrl}/api/leave/LeaveApplications`;
  private approvalUrl = `${environment.apiUrl}/api/leave/LeaveApproval`;
  private balanceUrl = `${environment.apiUrl}/api/leave/LeaveBalance`;

  applyLeave(command: ApplyLeaveCommand): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/apply`, command);
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
    return this.http.get<PagedResult<LeaveRequest>>(`${this.approvalUrl}/pending`, { params });
  }

  approveLeave(id: string, comments: string): Observable<void> {
    return this.http.post<void>(`${this.approvalUrl}/${id}/approve`, { comments });
  }

  rejectLeave(id: string, comments: string): Observable<void> {
    return this.http.post<void>(`${this.approvalUrl}/${id}/reject`, { comments });
  }

  getLeaveBalances(employeeId: string): Observable<LeaveBalance[]> {
    return this.http.get<LeaveBalance[]>(`${this.balanceUrl}?employeeId=${employeeId}`);
  }

  getLeaveTypes(): Observable<LeaveType[]> {
    return this.http.get<LeaveType[]>(`${environment.apiUrl}/api/leave/LeaveTypes`);
  }

  getHolidays(year: number): Observable<Holiday[]> {
    return this.http.get<Holiday[]>(`${environment.apiUrl}/api/leave/HolidayCalendar`, {
      params: { year: year.toString() },
    });
  }

  getLeaveCalendar(startDate: string, endDate: string): Observable<LeaveCalendarEntry[]> {
    return this.http.get<LeaveCalendarEntry[]>(`${environment.apiUrl}/api/leave/LeaveReport/calendar`, {
      params: { startDate, endDate },
    });
  }
}
