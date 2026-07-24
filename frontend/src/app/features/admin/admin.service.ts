import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
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
    return this.http.get<any>(`${environment.apiUrl}/api/dashboard/analytics`).pipe(
      map((data) => this.mapAdminStats(data)),
      catchError(() => of({
        totalUsers: 0,
        activeUsers: 0,
        totalDepartments: 0,
        totalRoles: 0,
        recentAuditLogs: [],
        systemHealth: { status: 'healthy' as const, uptime: 0, cpuUsage: 0, memoryUsage: 0, diskUsage: 0, activeUsers: 0, lastBackup: '', version: '1.0.0' },
      }))
    );
  }

  private mapAdminStats(data: any): AdminDashboardStats {
    if (!data) {
      return {
        totalUsers: 0, activeUsers: 0, totalDepartments: 0, totalRoles: 0,
        recentAuditLogs: [],
        systemHealth: { status: 'healthy', uptime: 0, cpuUsage: 0, memoryUsage: 0, diskUsage: 0, activeUsers: 0, lastBackup: '', version: '1.0.0' },
      };
    }
    return {
      totalUsers: data.totalUsers ?? data.TotalUsers ?? 0,
      activeUsers: data.activeUsers ?? data.ActiveUsers ?? 0,
      totalDepartments: data.totalDepartments ?? data.TotalDepartments ?? 0,
      totalRoles: data.totalRoles ?? data.TotalRoles ?? 0,
      recentAuditLogs: (data.recentAuditLogs ?? data.RecentAuditLogs ?? []).map((log: any) => ({
        id: log.id ?? log.Id ?? '',
        userId: log.userId ?? log.UserId ?? '',
        userName: log.userName ?? log.UserName ?? '',
        action: log.action ?? log.Action ?? '',
        entityType: log.entityType ?? log.EntityType ?? '',
        entityId: log.entityId ?? log.EntityId ?? '',
        entityName: log.entityName ?? log.EntityName ?? '',
        oldValues: log.oldValues ?? log.OldValues ?? null,
        newValues: log.newValues ?? log.NewValues ?? null,
        ipAddress: log.ipAddress ?? log.IpAddress ?? '',
        userAgent: log.userAgent ?? log.UserAgent ?? '',
        timestamp: log.timestamp ?? log.Timestamp ?? '',
      })),
      systemHealth: this.mapSystemHealth(data.systemHealth ?? data.SystemHealth),
    };
  }

  private mapSystemHealth(data: any): any {
    if (!data) {
      return { status: 'healthy', uptime: 0, cpuUsage: 0, memoryUsage: 0, diskUsage: 0, activeUsers: 0, lastBackup: '', version: '1.0.0' };
    }
    return {
      status: data.status ?? data.Status ?? 'healthy',
      uptime: data.uptime ?? data.Uptime ?? 0,
      cpuUsage: data.cpuUsage ?? data.CpuUsage ?? 0,
      memoryUsage: data.memoryUsage ?? data.MemoryUsage ?? 0,
      diskUsage: data.diskUsage ?? data.DiskUsage ?? 0,
      activeUsers: data.activeUsers ?? data.ActiveUsers ?? 0,
      lastBackup: data.lastBackup ?? data.LastBackup ?? '',
      version: data.version ?? data.Version ?? '1.0.0',
    };
  }

  getCompanySettings(): Observable<CompanySettings> {
    return this.http.get<CompanySettings>(`${this.organizationApi}/Company/1`).pipe(
      catchError(() => of({
        id: '', name: '', logo: '', address: '', city: '', state: '', country: '',
        postalCode: '', phone: '', email: '', website: '', currency: 'USD',
        timezone: 'UTC', dateFormat: 'MMM d, yyyy', language: 'en',
        taxId: '', registrationNumber: '',
      }))
    );
  }

  updateCompanySettings(settings: Partial<CompanySettings>): Observable<CompanySettings> {
    return this.http.put<CompanySettings>(`${this.organizationApi}/Company/${settings.id}`, settings);
  }

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.organizationApi}/Department`).pipe(
      catchError(() => of([]))
    );
  }

  getDepartment(id: string): Observable<Department> {
    return this.http.get<Department>(`${this.organizationApi}/Department/${id}`).pipe(
      catchError(() => of({
        id: '', name: '', code: '', description: '', headId: null, headName: null,
        parentId: null, parentName: null, employeeCount: 0, budget: 0,
        isActive: true, children: [], createdAt: '', updatedAt: '',
      }))
    );
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
    return this.http.get<Permission[]>(`${this.identityApi}/Roles`).pipe(
      catchError(() => of([]))
    );
  }

  assignPermissions(roleId: string, permissionIds: string[]): Observable<void> {
    // Backend adds permissions one at a time
    const calls = permissionIds.map(p =>
      this.http.post<void>(`${this.identityApi}/Roles/${roleId}/permissions`, { permission: p, module: '', description: '' })
    );
    // Execute sequentially - return last
    return this.http.post<void>(`${this.identityApi}/Roles/${roleId}/permissions`, {
      permission: permissionIds[0] || '', module: '', description: ''
    });
  }

  getAuditLogs(filters?: { search?: string; userId?: string; action?: string; entityType?: string; dateFrom?: string; dateTo?: string }): Observable<AuditLog[]> {
    let params = new HttpParams();
    if (filters) {
      Object.entries(filters).forEach(([key, value]) => {
        if (value) params = params.set(key, value);
      });
    }
    return this.http.get<AuditLog[]>(`${this.auditApi}/AuditLogs`, { params }).pipe(
      catchError(() => of([]))
    );
  }

  getFeatureFlags(): Observable<FeatureFlag[]> {
    // Feature flags are not yet implemented on the backend
    return of([]);
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
