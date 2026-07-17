import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  Course, Enrollment, Assessment, Certificate, LearningPath,
  TrainingSchedule, TrainingDashboardStats,
} from './training.models';

@Injectable({ providedIn: 'root' })
export class TrainingService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/training`;
  private coursesUrl = `${this.apiUrl}/Courses`;
  private enrollmentsUrl = `${this.apiUrl}/Enrollments`;
  private assessmentsUrl = `${this.apiUrl}/Assessments`;
  private certificatesUrl = `${this.apiUrl}/Certificates`;
  private pathsUrl = `${this.apiUrl}/LearningPaths`;
  private schedulesUrl = `${this.apiUrl}/TrainingSchedules`;

  getDashboardStats(): Observable<TrainingDashboardStats> {
    return this.http.get<TrainingDashboardStats>(`${this.coursesUrl}`);
  }

  // Courses
  getCourses(filters?: any): Observable<Course[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Course[]>(`${this.coursesUrl}`, { params });
  }

  getCourse(id: string): Observable<Course> {
    return this.http.get<Course>(`${this.coursesUrl}/${id}`);
  }

  createCourse(course: Partial<Course>): Observable<Course> {
    return this.http.post<Course>(`${this.coursesUrl}`, course);
  }

  updateCourse(id: string, course: Partial<Course>): Observable<Course> {
    return this.http.put<Course>(`${this.coursesUrl}/${id}`, course);
  }

  deleteCourse(id: string): Observable<void> {
    return this.http.delete<void>(`${this.coursesUrl}/${id}`);
  }

  // Enrollments
  enrollInCourse(courseId: string): Observable<Enrollment> {
    return this.http.post<Enrollment>(`${this.enrollmentsUrl}`, { courseId });
  }

  getMyEnrollments(filters?: any): Observable<Enrollment[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Enrollment[]>(`${this.enrollmentsUrl}`, { params });
  }

  updateLessonProgress(enrollmentId: string, lessonId: string): Observable<Enrollment> {
    return this.http.patch<Enrollment>(`${this.enrollmentsUrl}/${enrollmentId}/progress`, { lessonId });
  }

  bookmarkCourse(courseId: string): Observable<Enrollment> {
    return this.http.post<Enrollment>(`${this.enrollmentsUrl}/bookmark`, { courseId });
  }

  // Assessments
  getAssessments(filters?: any): Observable<Assessment[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Assessment[]>(`${this.assessmentsUrl}`, { params });
  }

  startAssessment(id: string): Observable<Assessment> {
    return this.http.post<Assessment>(`${this.assessmentsUrl}/${id}/start`, {});
  }

  submitAssessment(id: string, answers: { questionId: string; answer: number }[]): Observable<{ score: number; passed: boolean }> {
    return this.http.post<{ score: number; passed: boolean }>(`${this.assessmentsUrl}/${id}/submit`, { answers });
  }

  // Certificates
  getCertificates(): Observable<Certificate[]> {
    return this.http.get<Certificate[]>(`${this.certificatesUrl}`);
  }

  // Learning Paths
  getLearningPaths(): Observable<LearningPath[]> {
    return this.http.get<LearningPath[]>(`${this.pathsUrl}`);
  }

  getLearningPath(id: string): Observable<LearningPath> {
    return this.http.get<LearningPath>(`${this.pathsUrl}/${id}`);
  }

  enrollInPath(pathId: string): Observable<LearningPath> {
    return this.http.post<LearningPath>(`${this.pathsUrl}/${pathId}/enroll`, {});
  }

  // Training Schedule
  getTrainingSchedules(filters?: any): Observable<TrainingSchedule[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<TrainingSchedule[]>(`${this.schedulesUrl}`, { params });
  }

  createSchedule(schedule: Partial<TrainingSchedule>): Observable<TrainingSchedule> {
    return this.http.post<TrainingSchedule>(`${this.schedulesUrl}`, schedule);
  }
}
