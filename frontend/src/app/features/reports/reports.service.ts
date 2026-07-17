import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ReportTemplate,
  ReportInstance,
  ScheduledReport,
  GenerateReportRequest,
  ScheduleReportRequest,
} from './reports.models';

@Injectable({ providedIn: 'root' })
export class ReportsService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/reports/ReportTemplates`;

  getTemplates(): Observable<ReportTemplate[]> {
    return this.http.get<ReportTemplate[]>(`${this.apiUrl}/templates`);
  }

  getTemplate(id: string): Observable<ReportTemplate> {
    return this.http.get<ReportTemplate>(`${this.apiUrl}/templates/${id}`);
  }

  generateReport(request: GenerateReportRequest): Observable<ReportInstance> {
    return this.http.post<ReportInstance>(`${this.apiUrl}/generate`, request);
  }

  getInstances(): Observable<ReportInstance[]> {
    return this.http.get<ReportInstance[]>(`${this.apiUrl}/instances`);
  }

  getInstance(id: string): Observable<ReportInstance> {
    return this.http.get<ReportInstance>(`${this.apiUrl}/instances/${id}`);
  }

  downloadReport(id: string): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/instances/${id}/download`, { responseType: 'blob' });
  }

  getScheduledReports(): Observable<ScheduledReport[]> {
    return this.http.get<ScheduledReport[]>(`${this.apiUrl}/scheduled`);
  }

  scheduleReport(request: ScheduleReportRequest): Observable<ScheduledReport> {
    return this.http.post<ScheduledReport>(`${this.apiUrl}/scheduled`, request);
  }

  updateScheduledReport(id: string, request: Partial<ScheduleReportRequest>): Observable<ScheduledReport> {
    return this.http.put<ScheduledReport>(`${this.apiUrl}/scheduled/${id}`, request);
  }

  deleteScheduledReport(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/scheduled/${id}`);
  }

  toggleScheduledReport(id: string, isActive: boolean): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/scheduled/${id}/toggle`, { isActive });
  }
}
