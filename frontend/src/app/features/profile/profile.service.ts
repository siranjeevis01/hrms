import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
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

  getProfile(): Observable<UserProfile> {
    return this.http.get<UserProfile>(this.apiUrl);
  }

  updateProfile(request: UpdateProfileRequest): Observable<UserProfile> {
    return this.http.put<UserProfile>(this.apiUrl, request);
  }

  uploadAvatar(file: File): Observable<{ url: string }> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<{ url: string }>(`${this.apiUrl}/avatar`, formData);
  }

  getEmergencyContacts(): Observable<EmergencyContact[]> {
    return this.http.get<EmergencyContact[]>(`${this.apiUrl}/emergency-contacts`);
  }

  addEmergencyContact(contact: Partial<EmergencyContact>): Observable<EmergencyContact> {
    return this.http.post<EmergencyContact>(`${this.apiUrl}/emergency-contacts`, contact);
  }

  updateEmergencyContact(id: string, contact: Partial<EmergencyContact>): Observable<EmergencyContact> {
    return this.http.put<EmergencyContact>(`${this.apiUrl}/emergency-contacts/${id}`, contact);
  }

  deleteEmergencyContact(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/emergency-contacts/${id}`);
  }

  getSkills(): Observable<EmployeeSkill[]> {
    return this.http.get<EmployeeSkill[]>(`${this.apiUrl}/skills`);
  }

  addSkill(skill: Partial<EmployeeSkill>): Observable<EmployeeSkill> {
    return this.http.post<EmployeeSkill>(`${this.apiUrl}/skills`, skill);
  }

  deleteSkill(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/skills/${id}`);
  }

  getDocuments(): Observable<EmployeeDocument[]> {
    return this.http.get<EmployeeDocument[]>(`${this.apiUrl}/documents`);
  }

  getLeaveSummary(): Observable<LeaveSummary[]> {
    return this.http.get<LeaveSummary[]>(`${this.apiUrl}/leave-summary`);
  }

  getAttendanceSummary(): Observable<AttendanceSummary> {
    return this.http.get<AttendanceSummary>(`${this.apiUrl}/attendance-summary`);
  }

  getSettings(): Observable<ProfileSettings> {
    return this.http.get<ProfileSettings>(`${this.apiUrl}/settings`);
  }

  updateSettings(settings: Partial<ProfileSettings>): Observable<ProfileSettings> {
    return this.http.put<ProfileSettings>(`${this.apiUrl}/settings`, settings);
  }

  changePassword(request: ChangePasswordRequest): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/change-password`, request);
  }

  getSessions(): Observable<SessionInfo[]> {
    return this.http.get<SessionInfo[]>(`${this.apiUrl}/sessions`);
  }

  revokeSession(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/sessions/${id}`);
  }

  revokeAllSessions(): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/sessions`);
  }
}
