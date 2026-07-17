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
  private goalsUrl = `${this.apiUrl}/Goals`;
  private kpisUrl = `${this.apiUrl}/KPIs`;
  private reviewsUrl = `${this.apiUrl}/PerformanceReviews`;
  private okrsUrl = `${this.apiUrl}/OKRs`;
  private feedbackUrl = `${this.apiUrl}/Feedback360`;
  private appraisalsUrl = `${this.apiUrl}/Appraisals`;
  private calibrationUrl = `${this.apiUrl}/CalibrationSessions`;

  getDashboardStats(): Observable<PerformanceDashboardStats> {
    return this.http.get<PerformanceDashboardStats>(`${this.goalsUrl}`);
  }

  // Goals
  getGoals(filters?: any): Observable<Goal[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Goal[]>(`${this.goalsUrl}`, { params });
  }

  getGoal(id: string): Observable<Goal> {
    return this.http.get<Goal>(`${this.goalsUrl}/${id}`);
  }

  createGoal(goal: Partial<Goal>): Observable<Goal> {
    return this.http.post<Goal>(`${this.goalsUrl}`, goal);
  }

  updateGoal(id: string, goal: Partial<Goal>): Observable<Goal> {
    return this.http.put<Goal>(`${this.goalsUrl}/${id}`, goal);
  }

  deleteGoal(id: string): Observable<void> {
    return this.http.delete<void>(`${this.goalsUrl}/${id}`);
  }

  updateKeyResult(goalId: string, keyResultId: string, current: number): Observable<Goal> {
    return this.http.patch<Goal>(`${this.goalsUrl}/${goalId}/key-results/${keyResultId}`, { current });
  }

  // OKRs
  getOKRCycles(): Observable<OKRCycle[]> {
    return this.http.get<OKRCycle[]>(`${this.okrsUrl}`);
  }

  getOKRCycle(id: string): Observable<OKRCycle> {
    return this.http.get<OKRCycle>(`${this.okrsUrl}/${id}`);
  }

  createOKRCycle(cycle: Partial<OKRCycle>): Observable<OKRCycle> {
    return this.http.post<OKRCycle>(`${this.okrsUrl}`, cycle);
  }

  // KPIs
  getKPIs(filters?: any): Observable<KPI[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<KPI[]>(`${this.kpisUrl}`, { params });
  }

  createKPI(kpi: Partial<KPI>): Observable<KPI> {
    return this.http.post<KPI>(`${this.kpisUrl}`, kpi);
  }

  updateKPI(id: string, kpi: Partial<KPI>): Observable<KPI> {
    return this.http.put<KPI>(`${this.kpisUrl}/${id}`, kpi);
  }

  // Reviews
  getReviewCycles(): Observable<ReviewCycle[]> {
    return this.http.get<ReviewCycle[]>(`${this.reviewsUrl}`);
  }

  getReviews(filters?: any): Observable<Review[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Review[]>(`${this.reviewsUrl}`, { params });
  }

  getReview(id: string): Observable<Review> {
    return this.http.get<Review>(`${this.reviewsUrl}/${id}`);
  }

  createReview(review: Partial<Review>): Observable<Review> {
    return this.http.post<Review>(`${this.reviewsUrl}`, review);
  }

  updateReview(id: string, review: Partial<Review>): Observable<Review> {
    return this.http.put<Review>(`${this.reviewsUrl}/${id}`, review);
  }

  submitReview(id: string): Observable<Review> {
    return this.http.patch<Review>(`${this.reviewsUrl}/${id}/submit`, {});
  }

  // Feedback
  getFeedbackRequests(filters?: any): Observable<FeedbackRequest[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<FeedbackRequest[]>(`${this.feedbackUrl}`, { params });
  }

  requestFeedback(request: Partial<FeedbackRequest>): Observable<FeedbackRequest> {
    return this.http.post<FeedbackRequest>(`${this.feedbackUrl}`, request);
  }

  submitFeedback(id: string, answers: { question: string; answer: string }[]): Observable<FeedbackRequest> {
    return this.http.patch<FeedbackRequest>(`${this.feedbackUrl}/${id}`, { questions: answers });
  }

  // Appraisals
  getAppraisals(filters?: any): Observable<Appraisal[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Appraisal[]>(`${this.appraisalsUrl}`, { params });
  }

  updateAppraisal(id: string, appraisal: Partial<Appraisal>): Observable<Appraisal> {
    return this.http.put<Appraisal>(`${this.appraisalsUrl}/${id}`, appraisal);
  }

  // Calibration
  getCalibrationSessions(): Observable<CalibrationSession[]> {
    return this.http.get<CalibrationSession[]>(`${this.calibrationUrl}`);
  }

  getCalibrationSession(id: string): Observable<CalibrationSession> {
    return this.http.get<CalibrationSession>(`${this.calibrationUrl}/${id}`);
  }

  updateCalibrationRating(sessionId: string, employeeId: string, calibratedRating: number, discussion: string): Observable<CalibrationSession> {
    return this.http.patch<CalibrationSession>(`${this.calibrationUrl}/${sessionId}/ratings`, { employeeId, calibratedRating, discussion });
  }
}
