import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  Project,
  ProjectTask,
  Sprint,
  BoardColumn,
  Epic,
  ProjectStats,
  CreateProjectRequest,
  CreateTaskRequest,
  UpdateTaskStatusRequest,
} from './projects.models';

@Injectable({ providedIn: 'root' })
export class ProjectsService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/projects`;

  getProjects(search?: string, status?: string): Observable<Project[]> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    if (status) params = params.set('status', status);
    return this.http.get<Project[]>(`${environment.apiUrl}/api/projects/Projects`, { params });
  }

  getProject(id: string): Observable<Project> {
    return this.http.get<Project>(`${environment.apiUrl}/api/projects/Projects/${id}`);
  }

  createProject(request: CreateProjectRequest): Observable<Project> {
    return this.http.post<Project>(`${environment.apiUrl}/api/projects/Projects`, request);
  }

  updateProject(id: string, request: Partial<CreateProjectRequest>): Observable<Project> {
    return this.http.put<Project>(`${environment.apiUrl}/api/projects/Projects/${id}`, request);
  }

  deleteProject(id: string): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}/api/projects/Projects/${id}`);
  }

  getProjectStats(id: string): Observable<ProjectStats> {
    return this.http.get<ProjectStats>(`${environment.apiUrl}/api/projects/Projects/${id}/stats`);
  }

  getTasks(projectId: string, sprintId?: string, status?: string): Observable<ProjectTask[]> {
    let params = new HttpParams();
    if (sprintId) params = params.set('sprintId', sprintId);
    if (status) params = params.set('status', status);
    return this.http.get<ProjectTask[]>(`${this.apiUrl}/${projectId}/tasks`, { params });
  }

  getTask(taskId: string): Observable<ProjectTask> {
    return this.http.get<ProjectTask>(`${environment.apiUrl}/api/projects/projects/${taskId}`);
  }

  createTask(request: CreateTaskRequest): Observable<ProjectTask> {
    return this.http.post<ProjectTask>(`${environment.apiUrl}/api/projects/projects`, request);
  }

  updateTask(taskId: string, request: Partial<CreateTaskRequest>): Observable<ProjectTask> {
    return this.http.put<ProjectTask>(`${environment.apiUrl}/api/projects/projects/${taskId}`, request);
  }

  updateTaskStatus(request: UpdateTaskStatusRequest): Observable<void> {
    return this.http.patch<void>(`${environment.apiUrl}/api/projects/projects/${request.taskId}/status`, {
      status: request.status,
      position: request.position,
    });
  }

  deleteTask(taskId: string): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}/api/projects/projects/${taskId}`);
  }

  getBoard(projectId: string): Observable<BoardColumn[]> {
    return this.http.get<BoardColumn[]>(`${this.apiUrl}/${projectId}/board`);
  }

  getBacklog(projectId: string): Observable<ProjectTask[]> {
    return this.http.get<ProjectTask[]>(`${this.apiUrl}/${projectId}/backlog`);
  }

  getSprints(projectId: string): Observable<Sprint[]> {
    return this.http.get<Sprint[]>(`${this.apiUrl}/${projectId}/sprints`);
  }

  getSprint(projectId: string, sprintId: string): Observable<Sprint> {
    return this.http.get<Sprint>(`${this.apiUrl}/${projectId}/sprints/${sprintId}`);
  }

  createSprint(projectId: string, sprint: Partial<Sprint>): Observable<Sprint> {
    return this.http.post<Sprint>(`${this.apiUrl}/${projectId}/sprints`, sprint);
  }

  updateSprint(projectId: string, sprintId: string, sprint: Partial<Sprint>): Observable<Sprint> {
    return this.http.put<Sprint>(`${this.apiUrl}/${projectId}/sprints/${sprintId}`, sprint);
  }

  startSprint(projectId: string, sprintId: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${projectId}/sprints/${sprintId}/start`, {});
  }

  completeSprint(projectId: string, sprintId: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${projectId}/sprints/${sprintId}/complete`, {});
  }

  getEpics(projectId: string): Observable<Epic[]> {
    return this.http.get<Epic[]>(`${this.apiUrl}/${projectId}/epics`);
  }

  createEpic(projectId: string, epic: Partial<Epic>): Observable<Epic> {
    return this.http.post<Epic>(`${this.apiUrl}/${projectId}/epics`, epic);
  }

  addTeamMember(projectId: string, userId: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${projectId}/members`, { userId });
  }

  removeTeamMember(projectId: string, userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${projectId}/members/${userId}`);
  }

  logTime(taskId: string, hours: number, description: string): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/api/projects/projects/${taskId}/time-log`, { hours, description });
  }
}
