import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  CompanySettings,
  Department,
  Designation,
  Branch,
  Role,
  Permission,
  AuditLog,
  FeatureFlag,
  WorkflowTemplate,
  AdminDashboardStats,
} from './admin.models';

@Injectable({ providedIn: 'root' })
export class AdminService {
  private http = inject(HttpClient);
  private apiUrl = '/api/admin';

  getDashboardStats(): Observable<AdminDashboardStats> {
    return this.http.get<AdminDashboardStats>(`${this.apiUrl}/dashboard`);
  }

  getCompanySettings(): Observable<CompanySettings> {
    return this.http.get<CompanySettings>(`${this.apiUrl}/company`);
  }

  updateCompanySettings(settings: Partial<CompanySettings>): Observable<CompanySettings> {
    return this.http.put<CompanySettings>(`${this.apiUrl}/company`, settings);
  }

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.apiUrl}/departments`);
  }

  getDepartment(id: string): Observable<Department> {
    return this.http.get<Department>(`${this.apiUrl}/departments/${id}`);
  }

  createDepartment(department: Partial<Department>): Observable<Department> {
    return this.http.post<Department>(`${this.apiUrl}/departments`, department);
  }

  updateDepartment(id: string, department: Partial<Department>): Observable<Department> {
    return this.http.put<Department>(`${this.apiUrl}/departments/${id}`, department);
  }

  deleteDepartment(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/departments/${id}`);
  }

  getDesignations(departmentId?: string): Observable<Designation[]> {
    let params = new HttpParams();
    if (departmentId) params = params.set('departmentId', departmentId);
    return this.http.get<Designation[]>(`${this.apiUrl}/designations`, { params });
  }

  createDesignation(designation: Partial<Designation>): Observable<Designation> {
    return this.http.post<Designation>(`${this.apiUrl}/designations`, designation);
  }

  updateDesignation(id: string, designation: Partial<Designation>): Observable<Designation> {
    return this.http.put<Designation>(`${this.apiUrl}/designations/${id}`, designation);
  }

  deleteDesignation(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/designations/${id}`);
  }

  getBranches(): Observable<Branch[]> {
    return this.http.get<Branch[]>(`${this.apiUrl}/branches`);
  }

  createBranch(branch: Partial<Branch>): Observable<Branch> {
    return this.http.post<Branch>(`${this.apiUrl}/branches`, branch);
  }

  updateBranch(id: string, branch: Partial<Branch>): Observable<Branch> {
    return this.http.put<Branch>(`${this.apiUrl}/branches/${id}`, branch);
  }

  deleteBranch(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/branches/${id}`);
  }

  getRoles(): Observable<Role[]> {
    return this.http.get<Role[]>(`${this.apiUrl}/roles`);
  }

  getRole(id: string): Observable<Role> {
    return this.http.get<Role>(`${this.apiUrl}/roles/${id}`);
  }

  createRole(role: Partial<Role>): Observable<Role> {
    return this.http.post<Role>(`${this.apiUrl}/roles`, role);
  }

  updateRole(id: string, role: Partial<Role>): Observable<Role> {
    return this.http.put<Role>(`${this.apiUrl}/roles/${id}`, role);
  }

  deleteRole(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/roles/${id}`);
  }

  getPermissions(): Observable<Permission[]> {
    return this.http.get<Permission[]>(`${this.apiUrl}/permissions`);
  }

  assignPermissions(roleId: string, permissionIds: string[]): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/roles/${roleId}/permissions`, { permissionIds });
  }

  getAuditLogs(filters?: { search?: string; userId?: string; action?: string; entityType?: string; dateFrom?: string; dateTo?: string }): Observable<AuditLog[]> {
    let params = new HttpParams();
    if (filters) {
      Object.entries(filters).forEach(([key, value]) => {
        if (value) params = params.set(key, value);
      });
    }
    return this.http.get<AuditLog[]>(`${this.apiUrl}/audit-logs`, { params });
  }

  getFeatureFlags(): Observable<FeatureFlag[]> {
    return this.http.get<FeatureFlag[]>(`${this.apiUrl}/feature-flags`);
  }

  createFeatureFlag(flag: Partial<FeatureFlag>): Observable<FeatureFlag> {
    return this.http.post<FeatureFlag>(`${this.apiUrl}/feature-flags`, flag);
  }

  updateFeatureFlag(id: string, flag: Partial<FeatureFlag>): Observable<FeatureFlag> {
    return this.http.put<FeatureFlag>(`${this.apiUrl}/feature-flags/${id}`, flag);
  }

  deleteFeatureFlag(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/feature-flags/${id}`);
  }

  toggleFeatureFlag(id: string, isEnabled: boolean): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/feature-flags/${id}/toggle`, { isEnabled });
  }

  getWorkflows(): Observable<WorkflowTemplate[]> {
    return this.http.get<WorkflowTemplate[]>(`${this.apiUrl}/workflows`);
  }

  createWorkflow(workflow: Partial<WorkflowTemplate>): Observable<WorkflowTemplate> {
    return this.http.post<WorkflowTemplate>(`${this.apiUrl}/workflows`, workflow);
  }

  updateWorkflow(id: string, workflow: Partial<WorkflowTemplate>): Observable<WorkflowTemplate> {
    return this.http.put<WorkflowTemplate>(`${this.apiUrl}/workflows/${id}`, workflow);
  }

  deleteWorkflow(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/workflows/${id}`);
  }
}
