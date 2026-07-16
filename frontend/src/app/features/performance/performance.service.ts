import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  Goal, OKRCycle, KPI, Review, ReviewCycle, FeedbackRequest, Appraisal,
  CalibrationSession, PerformanceDashboardStats,
} from './performance.models';

@Injectable({ providedIn: 'root' })
export class PerformanceService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/performance`;

  getDashboardStats(): Observable<PerformanceDashboardStats> {
    return this.http.get<PerformanceDashboardStats>(`${this.apiUrl}/dashboard`);
  }

  // Goals
  getGoals(filters?: any): Observable<Goal[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Goal[]>(`${this.apiUrl}/goals`, { params });
  }

  getGoal(id: string): Observable<Goal> {
    return this.http.get<Goal>(`${this.apiUrl}/goals/${id}`);
  }

  createGoal(goal: Partial<Goal>): Observable<Goal> {
    return this.http.post<Goal>(`${this.apiUrl}/goals`, goal);
  }

  updateGoal(id: string, goal: Partial<Goal>): Observable<Goal> {
    return this.http.put<Goal>(`${this.apiUrl}/goals/${id}`, goal);
  }

  deleteGoal(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/goals/${id}`);
  }

  updateKeyResult(goalId: string, keyResultId: string, current: number): Observable<Goal> {
    return this.http.patch<Goal>(`${this.apiUrl}/goals/${goalId}/key-results/${keyResultId}`, { current });
  }

  // OKRs
  getOKRCycles(): Observable<OKRCycle[]> {
    return this.http.get<OKRCycle[]>(`${this.apiUrl}/okrs`);
  }

  getOKRCycle(id: string): Observable<OKRCycle> {
    return this.http.get<OKRCycle>(`${this.apiUrl}/okrs/${id}`);
  }

  createOKRCycle(cycle: Partial<OKRCycle>): Observable<OKRCycle> {
    return this.http.post<OKRCycle>(`${this.apiUrl}/okrs`, cycle);
  }

  // KPIs
  getKPIs(filters?: any): Observable<KPI[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<KPI[]>(`${this.apiUrl}/kpis`, { params });
  }

  createKPI(kpi: Partial<KPI>): Observable<KPI> {
    return this.http.post<KPI>(`${this.apiUrl}/kpis`, kpi);
  }

  updateKPI(id: string, kpi: Partial<KPI>): Observable<KPI> {
    return this.http.put<KPI>(`${this.apiUrl}/kpis/${id}`, kpi);
  }

  // Reviews
  getReviewCycles(): Observable<ReviewCycle[]> {
    return this.http.get<ReviewCycle[]>(`${this.apiUrl}/review-cycles`);
  }

  getReviews(filters?: any): Observable<Review[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Review[]>(`${this.apiUrl}/reviews`, { params });
  }

  getReview(id: string): Observable<Review> {
    return this.http.get<Review>(`${this.apiUrl}/reviews/${id}`);
  }

  createReview(review: Partial<Review>): Observable<Review> {
    return this.http.post<Review>(`${this.apiUrl}/reviews`, review);
  }

  updateReview(id: string, review: Partial<Review>): Observable<Review> {
    return this.http.put<Review>(`${this.apiUrl}/reviews/${id}`, review);
  }

  submitReview(id: string): Observable<Review> {
    return this.http.patch<Review>(`${this.apiUrl}/reviews/${id}/submit`, {});
  }

  // Feedback
  getFeedbackRequests(filters?: any): Observable<FeedbackRequest[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<FeedbackRequest[]>(`${this.apiUrl}/feedback`, { params });
  }

  requestFeedback(request: Partial<FeedbackRequest>): Observable<FeedbackRequest> {
    return this.http.post<FeedbackRequest>(`${this.apiUrl}/feedback`, request);
  }

  submitFeedback(id: string, answers: { question: string; answer: string }[]): Observable<FeedbackRequest> {
    return this.http.patch<FeedbackRequest>(`${this.apiUrl}/feedback/${id}`, { questions: answers });
  }

  // Appraisals
  getAppraisals(filters?: any): Observable<Appraisal[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Appraisal[]>(`${this.apiUrl}/appraisals`, { params });
  }

  updateAppraisal(id: string, appraisal: Partial<Appraisal>): Observable<Appraisal> {
    return this.http.put<Appraisal>(`${this.apiUrl}/appraisals/${id}`, appraisal);
  }

  // Calibration
  getCalibrationSessions(): Observable<CalibrationSession[]> {
    return this.http.get<CalibrationSession[]>(`${this.apiUrl}/calibration`);
  }

  getCalibrationSession(id: string): Observable<CalibrationSession> {
    return this.http.get<CalibrationSession>(`${this.apiUrl}/calibration/${id}`);
  }

  updateCalibrationRating(sessionId: string, employeeId: string, calibratedRating: number, discussion: string): Observable<CalibrationSession> {
    return this.http.patch<CalibrationSession>(`${this.apiUrl}/calibration/${sessionId}/ratings`, { employeeId, calibratedRating, discussion });
  }
}
