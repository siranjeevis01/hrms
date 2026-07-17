import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  TravelRequest,
  TravelDashboardStats,
  SubmitTravelRequest,
} from './travel.models';

@Injectable({ providedIn: 'root' })
export class TravelService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/travel/TravelRequests`;

  getDashboardStats(): Observable<TravelDashboardStats> {
    return this.http.get<TravelDashboardStats>(`${this.apiUrl}/dashboard`);
  }

  getMyRequests(status?: string): Observable<TravelRequest[]> {
    let params = new HttpParams();
    if (status) params = params.set('status', status);
    return this.http.get<TravelRequest[]>(`${this.apiUrl}/my`, { params });
  }

  getRequest(id: string): Observable<TravelRequest> {
    return this.http.get<TravelRequest>(`${this.apiUrl}/${id}`);
  }

  submitRequest(request: SubmitTravelRequest): Observable<TravelRequest> {
    return this.http.post<TravelRequest>(this.apiUrl, request);
  }

  updateRequest(id: string, request: Partial<SubmitTravelRequest>): Observable<TravelRequest> {
    return this.http.put<TravelRequest>(`${this.apiUrl}/${id}`, request);
  }

  cancelRequest(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/cancel`, {});
  }

  getPendingApprovals(): Observable<TravelRequest[]> {
    return this.http.get<TravelRequest[]>(`${this.apiUrl}/approvals`);
  }

  approveRequest(id: string, approved: boolean, comments: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/approve`, { approved, comments });
  }
}
