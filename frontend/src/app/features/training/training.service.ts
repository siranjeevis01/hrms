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

  getDashboardStats(): Observable<TrainingDashboardStats> {
    return this.http.get<TrainingDashboardStats>(`${this.apiUrl}/dashboard`);
  }

  // Courses
  getCourses(filters?: any): Observable<Course[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Course[]>(`${this.apiUrl}/courses`, { params });
  }

  getCourse(id: string): Observable<Course> {
    return this.http.get<Course>(`${this.apiUrl}/courses/${id}`);
  }

  createCourse(course: Partial<Course>): Observable<Course> {
    return this.http.post<Course>(`${this.apiUrl}/courses`, course);
  }

  updateCourse(id: string, course: Partial<Course>): Observable<Course> {
    return this.http.put<Course>(`${this.apiUrl}/courses/${id}`, course);
  }

  deleteCourse(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/courses/${id}`);
  }

  // Enrollments
  enrollInCourse(courseId: string): Observable<Enrollment> {
    return this.http.post<Enrollment>(`${this.apiUrl}/enrollments`, { courseId });
  }

  getMyEnrollments(filters?: any): Observable<Enrollment[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Enrollment[]>(`${this.apiUrl}/enrollments`, { params });
  }

  updateLessonProgress(enrollmentId: string, lessonId: string): Observable<Enrollment> {
    return this.http.patch<Enrollment>(`${this.apiUrl}/enrollments/${enrollmentId}/progress`, { lessonId });
  }

  bookmarkCourse(courseId: string): Observable<Enrollment> {
    return this.http.post<Enrollment>(`${this.apiUrl}/enrollments/bookmark`, { courseId });
  }

  // Assessments
  getAssessments(filters?: any): Observable<Assessment[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<Assessment[]>(`${this.apiUrl}/assessments`, { params });
  }

  startAssessment(id: string): Observable<Assessment> {
    return this.http.post<Assessment>(`${this.apiUrl}/assessments/${id}/start`, {});
  }

  submitAssessment(id: string, answers: { questionId: string; answer: number }[]): Observable<{ score: number; passed: boolean }> {
    return this.http.post<{ score: number; passed: boolean }>(`${this.apiUrl}/assessments/${id}/submit`, { answers });
  }

  // Certificates
  getCertificates(): Observable<Certificate[]> {
    return this.http.get<Certificate[]>(`${this.apiUrl}/certificates`);
  }

  // Learning Paths
  getLearningPaths(): Observable<LearningPath[]> {
    return this.http.get<LearningPath[]>(`${this.apiUrl}/learning-paths`);
  }

  getLearningPath(id: string): Observable<LearningPath> {
    return this.http.get<LearningPath>(`${this.apiUrl}/learning-paths/${id}`);
  }

  enrollInPath(pathId: string): Observable<LearningPath> {
    return this.http.post<LearningPath>(`${this.apiUrl}/learning-paths/${pathId}/enroll`, {});
  }

  // Training Schedule
  getTrainingSchedules(filters?: any): Observable<TrainingSchedule[]> {
    let params = new HttpParams();
    if (filters) {
      Object.keys(filters).forEach(key => {
        if (filters[key]) params = params.set(key, filters[key]);
      });
    }
    return this.http.get<TrainingSchedule[]>(`${this.apiUrl}/schedules`, { params });
  }

  createSchedule(schedule: Partial<TrainingSchedule>): Observable<TrainingSchedule> {
    return this.http.post<TrainingSchedule>(`${this.apiUrl}/schedules`, schedule);
  }
}
