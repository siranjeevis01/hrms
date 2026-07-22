import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
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
  private identityApi = `${environment.apiUrl}/api/identity`;
  private organizationApi = `${environment.apiUrl}/api/organization`;
  private auditApi = `${environment.apiUrl}/api/audit`;
  private workflowApi = `${environment.apiUrl}/api/workflow`;

  getDashboardStats(): Observable<AdminDashboardStats> {
    return this.http.get<AdminDashboardStats>(`${environment.apiUrl}/api/dashboard/Analytics`);
  }

  getCompanySettings(): Observable<CompanySettings> {
    return this.http.get<CompanySettings>(`${this.organizationApi}/Company`);
  }

  updateCompanySettings(settings: Partial<CompanySettings>): Observable<CompanySettings> {
    return this.http.put<CompanySettings>(`${this.organizationApi}/Company`, settings);
  }

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.organizationApi}/Department`);
  }

  getDepartment(id: string): Observable<Department> {
    return this.http.get<Department>(`${this.organizationApi}/Department/${id}`);
  }

  createDepartment(department: Partial<Department>): Observable<Department> {
    return this.http.post<Department>(`${this.organizationApi}/Department`, department);
  }

  updateDepartment(id: string, department: Partial<Department>): Observable<Department> {
    return this.http.put<Department>(`${this.organizationApi}/Department/${id}`, department);
  }

  deleteDepartment(id: string): Observable<void> {
    return this.http.delete<void>(`${this.organizationApi}/Department/${id}`);
  }

  getDesignations(departmentId?: string): Observable<Designation[]> {
    let params = new HttpParams();
    if (departmentId) params = params.set('departmentId', departmentId);
    return this.http.get<Designation[]>(`${this.organizationApi}/Designation`, { params });
  }

  createDesignation(designation: Partial<Designation>): Observable<Designation> {
    return this.http.post<Designation>(`${this.organizationApi}/Designation`, designation);
  }

  updateDesignation(id: string, designation: Partial<Designation>): Observable<Designation> {
    return this.http.put<Designation>(`${this.organizationApi}/Designation/${id}`, designation);
  }

  deleteDesignation(id: string): Observable<void> {
    return this.http.delete<void>(`${this.organizationApi}/Designation/${id}`);
  }

  getBranches(): Observable<Branch[]> {
    return this.http.get<Branch[]>(`${this.organizationApi}/Branch`);
  }

  createBranch(branch: Partial<Branch>): Observable<Branch> {
    return this.http.post<Branch>(`${this.organizationApi}/Branch`, branch);
  }

  updateBranch(id: string, branch: Partial<Branch>): Observable<Branch> {
    return this.http.put<Branch>(`${this.organizationApi}/Branch/${id}`, branch);
  }

  deleteBranch(id: string): Observable<void> {
    return this.http.delete<void>(`${this.organizationApi}/Branch/${id}`);
  }

  getRoles(): Observable<Role[]> {
    return this.http.get<Role[]>(`${this.identityApi}/Roles`);
  }

  getRole(id: string): Observable<Role> {
    return this.http.get<Role>(`${this.identityApi}/Roles/${id}`);
  }

  createRole(role: Partial<Role>): Observable<Role> {
    return this.http.post<Role>(`${this.identityApi}/Roles`, role);
  }

  updateRole(id: string, role: Partial<Role>): Observable<Role> {
    return this.http.put<Role>(`${this.identityApi}/Roles/${id}`, role);
  }

  deleteRole(id: string): Observable<void> {
    return this.http.delete<void>(`${this.identityApi}/Roles/${id}`);
  }

  getPermissions(): Observable<Permission[]> {
    return this.http.get<Permission[]>(`${this.identityApi}/Roles`);
  }

  assignPermissions(roleId: string, permissionIds: string[]): Observable<void> {
    return this.http.post<void>(`${this.identityApi}/Roles/${roleId}/permissions`, { permissionIds });
  }

  getAuditLogs(filters?: { search?: string; userId?: string; action?: string; entityType?: string; dateFrom?: string; dateTo?: string }): Observable<AuditLog[]> {
    let params = new HttpParams();
    if (filters) {
      Object.entries(filters).forEach(([key, value]) => {
        if (value) params = params.set(key, value);
      });
    }
    return this.http.get<AuditLog[]>(`${this.auditApi}/AuditLogs`, { params });
  }

  getFeatureFlags(): Observable<FeatureFlag[]> {
    return this.http.get<FeatureFlag[]>(`${this.auditApi}/AuditLogs`);
  }

  createFeatureFlag(flag: Partial<FeatureFlag>): Observable<FeatureFlag> {
    return this.http.post<FeatureFlag>(`${this.auditApi}/AuditLogs`, flag);
  }

  updateFeatureFlag(id: string, flag: Partial<FeatureFlag>): Observable<FeatureFlag> {
    return this.http.put<FeatureFlag>(`${this.auditApi}/AuditLogs/${id}`, flag);
  }

  deleteFeatureFlag(id: string): Observable<void> {
    return this.http.delete<void>(`${this.auditApi}/AuditLogs/${id}`);
  }

  toggleFeatureFlag(id: string, isEnabled: boolean): Observable<void> {
    return this.http.patch<void>(`${this.auditApi}/AuditLogs/${id}/toggle`, { isEnabled });
  }

  getWorkflows(): Observable<WorkflowTemplate[]> {
    return this.http.get<WorkflowTemplate[]>(`${this.workflowApi}/WorkflowDefinitions`);
  }

  createWorkflow(workflow: Partial<WorkflowTemplate>): Observable<WorkflowTemplate> {
    return this.http.post<WorkflowTemplate>(`${this.workflowApi}/WorkflowDefinitions`, workflow);
  }

  updateWorkflow(id: string, workflow: Partial<WorkflowTemplate>): Observable<WorkflowTemplate> {
    return this.http.put<WorkflowTemplate>(`${this.workflowApi}/WorkflowDefinitions/${id}`, workflow);
  }

  deleteWorkflow(id: string): Observable<void> {
    return this.http.delete<void>(`${this.workflowApi}/WorkflowDefinitions/${id}`);
  }
}
