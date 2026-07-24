import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import {
  UserProfile,
  EmergencyContact,
  EmployeeSkill,
  EmployeeDocument,
  LeaveSummary,
  AttendanceSummary,
  ProfileSettings,
  SessionInfo,
  UpdateProfileRequest,
  ChangePasswordRequest,
} from './profile.models';

@Injectable({ providedIn: 'root' })
export class ProfileService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/identity/Users/me`;
  private employeeApi = `${environment.apiUrl}/api/employees`;

  getProfile(): Observable<UserProfile> {
    return this.http.get<any>(this.apiUrl).pipe(
      map((data) => this.mapProfile(data)),
      catchError(() => of({
        id: '', employeeId: '', firstName: '', lastName: '', email: '', phone: '',
        avatar: '', designation: '', department: '', branch: '', dateOfJoining: '',
        dateOfBirth: '', gender: '', bloodGroup: '', address: '', city: '',
        state: '', country: '', postalCode: '', reportingManager: '',
        employmentType: '', status: '',
      }))
    );
  }

  updateProfile(request: UpdateProfileRequest): Observable<UserProfile> {
    return this.http.put<any>(this.apiUrl, request).pipe(
      map((data) => this.mapProfile(data)),
      catchError(() => this.getProfile())
    );
  }

  uploadAvatar(file: File): Observable<{ url: string }> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<{ url: string }>(`${this.apiUrl}/avatar`, formData);
  }

  getEmergencyContacts(): Observable<EmergencyContact[]> {
    return this.http.get<any>(`${this.employeeApi}/EmergencyContacts`).pipe(
      map((data) => {
        if (Array.isArray(data)) return data.map((c: any) => this.mapEmergencyContact(c));
        if (data?.items) return data.items.map((c: any) => this.mapEmergencyContact(c));
        return [];
      }),
      catchError(() => of([]))
    );
  }

  addEmergencyContact(contact: Partial<EmergencyContact>): Observable<EmergencyContact> {
    return this.http.post<EmergencyContact>(`${this.employeeApi}/EmergencyContacts`, contact);
  }

  updateEmergencyContact(id: string, contact: Partial<EmergencyContact>): Observable<EmergencyContact> {
    return this.http.put<EmergencyContact>(`${this.employeeApi}/EmergencyContacts/${id}`, contact);
  }

  deleteEmergencyContact(id: string): Observable<void> {
    return this.http.delete<void>(`${this.employeeApi}/EmergencyContacts/${id}`);
  }

  getSkills(): Observable<EmployeeSkill[]> {
    return this.http.get<any>(`${this.employeeApi}/EmployeeSkills`).pipe(
      map((data) => {
        if (Array.isArray(data)) return data.map((s: any) => this.mapSkill(s));
        if (data?.items) return data.items.map((s: any) => this.mapSkill(s));
        return [];
      }),
      catchError(() => of([]))
    );
  }

  addSkill(skill: Partial<EmployeeSkill>): Observable<EmployeeSkill> {
    return this.http.post<EmployeeSkill>(`${this.employeeApi}/EmployeeSkills`, skill);
  }

  deleteSkill(id: string): Observable<void> {
    return this.http.delete<void>(`${this.employeeApi}/EmployeeSkills/${id}`);
  }

  getDocuments(): Observable<EmployeeDocument[]> {
    return this.http.get<any>(`${this.employeeApi}/EmployeeDocuments`).pipe(
      map((data) => {
        if (Array.isArray(data)) return data;
        if (data?.items) return data.items;
        return [];
      }),
      catchError(() => of([]))
    );
  }

  getLeaveSummary(): Observable<LeaveSummary[]> {
    return this.http.get<any>(`${environment.apiUrl}/api/leave/leavebalance`).pipe(
      map((data) => {
        if (Array.isArray(data)) return data.map((l: any) => this.mapLeaveSummary(l));
        if (data?.balances) return data.balances.map((l: any) => this.mapLeaveSummary(l));
        return [];
      }),
      catchError(() => of([]))
    );
  }

  getAttendanceSummary(): Observable<AttendanceSummary> {
    return this.http.get<any>(`${environment.apiUrl}/api/attendance/attendance/summary`).pipe(
      map((data) => this.mapAttendanceSummary(data)),
      catchError(() => of({
        present: 0, absent: 0, late: 0, wfh: 0,
        totalWorkingDays: 0, attendancePercentage: 0,
      }))
    );
  }

  getSettings(): Observable<ProfileSettings> {
    return this.http.get<ProfileSettings>(`${this.apiUrl}/settings`).pipe(
      catchError(() => of({
        notifications: [],
        theme: 'light' as const,
        language: 'en',
        timezone: 'UTC',
        dateFormat: 'MMM d, yyyy',
      }))
    );
  }

  updateSettings(settings: Partial<ProfileSettings>): Observable<ProfileSettings> {
    return this.http.put<ProfileSettings>(`${this.apiUrl}/settings`, settings);
  }

  changePassword(request: ChangePasswordRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/api/identity/auth/change-password`, request);
  }

  getSessions(): Observable<SessionInfo[]> {
    return this.http.get<SessionInfo[]>(`${this.apiUrl}/sessions`).pipe(
      catchError(() => of([]))
    );
  }

  revokeSession(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/sessions/${id}`);
  }

  revokeAllSessions(): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/sessions`);
  }

  private mapProfile(data: any): UserProfile {
    if (!data) return {
      id: '', employeeId: '', firstName: '', lastName: '', email: '', phone: '',
      avatar: '', designation: '', department: '', branch: '', dateOfJoining: '',
      dateOfBirth: '', gender: '', bloodGroup: '', address: '', city: '',
      state: '', country: '', postalCode: '', reportingManager: '',
      employmentType: '', status: '',
    };
    return {
      id: data.id ?? data.Id ?? '',
      employeeId: data.employeeId ?? data.EmployeeId ?? '',
      firstName: data.firstName ?? data.FirstName ?? '',
      lastName: data.lastName ?? data.LastName ?? '',
      email: data.email ?? data.Email ?? '',
      phone: data.phone ?? data.Phone ?? data.phoneNumber ?? data.PhoneNumber ?? '',
      avatar: data.avatar ?? data.Avatar ?? data.profilePictureUrl ?? data.ProfilePictureUrl ?? '',
      designation: data.designation ?? data.Designation ?? '',
      department: data.department ?? data.Department ?? '',
      branch: data.branch ?? data.Branch ?? '',
      dateOfJoining: data.dateOfJoining ?? data.DateOfJoining ?? '',
      dateOfBirth: data.dateOfBirth ?? data.DateOfBirth ?? '',
      gender: data.gender ?? data.Gender ?? '',
      bloodGroup: data.bloodGroup ?? data.BloodGroup ?? '',
      address: data.address ?? data.Address ?? '',
      city: data.city ?? data.City ?? '',
      state: data.state ?? data.State ?? '',
      country: data.country ?? data.Country ?? '',
      postalCode: data.postalCode ?? data.PostalCode ?? '',
      reportingManager: data.reportingManager ?? data.ReportingManager ?? '',
      employmentType: data.employmentType ?? data.EmploymentType ?? '',
      status: data.status ?? data.Status ?? '',
    };
  }

  private mapEmergencyContact(data: any): EmergencyContact {
    return {
      id: data.id ?? data.Id ?? '',
      name: data.name ?? data.Name ?? '',
      relationship: data.relationship ?? data.Relationship ?? '',
      phone: data.phone ?? data.Phone ?? '',
      email: data.email ?? data.Email ?? '',
      address: data.address ?? data.Address ?? '',
    };
  }

  private mapSkill(data: any): EmployeeSkill {
    return {
      id: data.id ?? data.Id ?? '',
      name: data.name ?? data.Name ?? data.skillName ?? data.SkillName ?? '',
      level: data.level ?? data.Level ?? 'beginner',
      yearsOfExperience: data.yearsOfExperience ?? data.YearsOfExperience ?? 0,
      endorsed: data.endorsed ?? data.Endorsed ?? false,
      endorsements: data.endorsements ?? data.Endorsements ?? 0,
    };
  }

  private mapLeaveSummary(data: any): LeaveSummary {
    return {
      totalLeaves: data.totalLeaves ?? data.TotalLeaves ?? data.allocated ?? data.Allocated ?? 0,
      usedLeaves: data.usedLeaves ?? data.UsedLeaves ?? data.used ?? data.Used ?? 0,
      pendingLeaves: data.pendingLeaves ?? data.PendingLeaves ?? data.pending ?? data.Pending ?? 0,
      balance: data.balance ?? data.Balance ?? data.remaining ?? data.Remaining ?? 0,
      leaveType: data.leaveType ?? data.LeaveType ?? data.leaveTypeName ?? data.LeaveTypeName ?? '',
    };
  }

  private mapAttendanceSummary(data: any): AttendanceSummary {
    if (!data) {
      return { present: 0, absent: 0, late: 0, wfh: 0, totalWorkingDays: 0, attendancePercentage: 0 };
    }
    return {
      present: data.present ?? data.Present ?? 0,
      absent: data.absent ?? data.Absent ?? 0,
      late: data.late ?? data.Late ?? 0,
      wfh: data.wfh ?? data.Wfh ?? 0,
      totalWorkingDays: data.totalWorkingDays ?? data.TotalWorkingDays ?? 0,
      attendancePercentage: data.attendancePercentage ?? data.AttendancePercentage ?? 0,
    };
  }
}
