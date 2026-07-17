import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  Job, Candidate, Application, Interview, Offer, OnboardingChecklist,
  RecruitmentDashboardStats, JobFilters, CandidateFilters,
} from './recruitment.models';

@Injectable({ providedIn: 'root' })
export class RecruitmentService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/recruitment`;
  private jobsUrl = `${environment.apiUrl}/api/recruitment/JobPostings`;
  private candidatesUrl = `${environment.apiUrl}/api/recruitment/Candidates`;
  private applicationsUrl = `${environment.apiUrl}/api/recruitment/JobApplications`;
  private interviewsUrl = `${environment.apiUrl}/api/recruitment/Interviews`;
  private offersUrl = `${environment.apiUrl}/api/recruitment/OfferLetters`;
  private onboardingUrl = `${environment.apiUrl}/api/recruitment/OnboardingChecklists`;

  getDashboardStats(): Observable<RecruitmentDashboardStats> {
    return this.http.get<RecruitmentDashboardStats>(`${this.jobsUrl}`);
  }

  // Jobs
  getJobs(filters?: JobFilters): Observable<Job[]> {
    let params = new HttpParams();
    if (filters) {
      if (filters.search) params = params.set('search', filters.search);
      if (filters.department) params = params.set('department', filters.department);
      if (filters.status) params = params.set('status', filters.status);
      if (filters.employmentType) params = params.set('employmentType', filters.employmentType);
    }
    return this.http.get<Job[]>(`${this.jobsUrl}`, { params });
  }

  getJob(id: string): Observable<Job> {
    return this.http.get<Job>(`${this.jobsUrl}/${id}`);
  }

  createJob(job: Partial<Job>): Observable<Job> {
    return this.http.post<Job>(`${this.jobsUrl}`, job);
  }

  updateJob(id: string, job: Partial<Job>): Observable<Job> {
    return this.http.put<Job>(`${this.jobsUrl}/${id}`, job);
  }

  deleteJob(id: string): Observable<void> {
    return this.http.delete<void>(`${this.jobsUrl}/${id}`);
  }

  updateJobStatus(id: string, status: string): Observable<Job> {
    return this.http.patch<Job>(`${this.jobsUrl}/${id}/status`, { status });
  }

  // Candidates
  getCandidates(filters?: CandidateFilters): Observable<Candidate[]> {
    let params = new HttpParams();
    if (filters) {
      if (filters.search) params = params.set('search', filters.search);
      if (filters.status) params = params.set('status', filters.status);
      if (filters.source) params = params.set('source', filters.source);
    }
    return this.http.get<Candidate[]>(`${this.candidatesUrl}`, { params });
  }

  getCandidate(id: string): Observable<Candidate> {
    return this.http.get<Candidate>(`${this.candidatesUrl}/${id}`);
  }

  createCandidate(candidate: Partial<Candidate>): Observable<Candidate> {
    return this.http.post<Candidate>(`${this.candidatesUrl}`, candidate);
  }

  updateCandidate(id: string, candidate: Partial<Candidate>): Observable<Candidate> {
    return this.http.put<Candidate>(`${this.candidatesUrl}/${id}`, candidate);
  }

  deleteCandidate(id: string): Observable<void> {
    return this.http.delete<void>(`${this.candidatesUrl}/${id}`);
  }

  // Applications
  getApplications(jobId?: string): Observable<Application[]> {
    let params = new HttpParams();
    if (jobId) params = params.set('jobId', jobId);
    return this.http.get<Application[]>(`${this.applicationsUrl}`, { params });
  }

  getApplication(id: string): Observable<Application> {
    return this.http.get<Application>(`${this.applicationsUrl}/${id}`);
  }

  createApplication(application: Partial<Application>): Observable<Application> {
    return this.http.post<Application>(`${this.applicationsUrl}`, application);
  }

  updateApplicationStatus(id: string, status: string): Observable<Application> {
    return this.http.patch<Application>(`${this.applicationsUrl}/${id}/status`, { status });
  }

  // Interviews
  getInterviews(filters?: any): Observable<Interview[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Interview[]>(`${this.interviewsUrl}`, { params });
  }

  getInterview(id: string): Observable<Interview> {
    return this.http.get<Interview>(`${this.interviewsUrl}/${id}`);
  }

  scheduleInterview(interview: Partial<Interview>): Observable<Interview> {
    return this.http.post<Interview>(`${this.interviewsUrl}`, interview);
  }

  updateInterview(id: string, interview: Partial<Interview>): Observable<Interview> {
    return this.http.put<Interview>(`${this.interviewsUrl}/${id}`, interview);
  }

  updateInterviewStatus(id: string, status: string, feedback?: string, rating?: number): Observable<Interview> {
    return this.http.patch<Interview>(`${this.interviewsUrl}/${id}/status`, { status, feedback, rating });
  }

  // Offers
  getOffers(filters?: any): Observable<Offer[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Offer[]>(`${this.offersUrl}`, { params });
  }

  getOffer(id: string): Observable<Offer> {
    return this.http.get<Offer>(`${this.offersUrl}/${id}`);
  }

  createOffer(offer: Partial<Offer>): Observable<Offer> {
    return this.http.post<Offer>(`${this.offersUrl}`, offer);
  }

  updateOfferStatus(id: string, status: string): Observable<Offer> {
    return this.http.patch<Offer>(`${this.offersUrl}/${id}/status`, { status });
  }

  // Onboarding
  getOnboardingChecklists(): Observable<OnboardingChecklist[]> {
    return this.http.get<OnboardingChecklist[]>(`${this.onboardingUrl}`);
  }

  getOnboardingChecklist(id: string): Observable<OnboardingChecklist> {
    return this.http.get<OnboardingChecklist>(`${this.onboardingUrl}/${id}`);
  }

  createOnboardingChecklist(checklist: Partial<OnboardingChecklist>): Observable<OnboardingChecklist> {
    return this.http.post<OnboardingChecklist>(`${this.onboardingUrl}`, checklist);
  }

  updateOnboardingTask(checklistId: string, taskId: string, completed: boolean): Observable<OnboardingChecklist> {
    return this.http.patch<OnboardingChecklist>(`${this.onboardingUrl}/${checklistId}/tasks/${taskId}`, { completed });
  }
}
