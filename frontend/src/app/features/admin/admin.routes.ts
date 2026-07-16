import { Routes } from '@angular/router';

export const ADMIN_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./admin-dashboard/admin-dashboard.component').then(
        (m) => m.AdminDashboardComponent,
      ),
  },
  {
    path: 'company',
    loadComponent: () =>
      import('./company-settings/company-settings.component').then(
        (m) => m.CompanySettingsComponent,
      ),
  },
  {
    path: 'departments',
    loadComponent: () =>
      import('./department-management/department-management.component').then(
        (m) => m.DepartmentManagementComponent,
      ),
  },
  {
    path: 'designations',
    loadComponent: () =>
      import('./designation-management/designation-management.component').then(
        (m) => m.DesignationManagementComponent,
      ),
  },
  {
    path: 'branches',
    loadComponent: () =>
      import('./branch-management/branch-management.component').then(
        (m) => m.BranchManagementComponent,
      ),
  },
  {
    path: 'roles',
    loadComponent: () =>
      import('./role-management/role-management.component').then(
        (m) => m.RoleManagementComponent,
      ),
  },
  {
    path: 'permissions',
    loadComponent: () =>
      import('./permission-management/permission-management.component').then(
        (m) => m.PermissionManagementComponent,
      ),
  },
  {
    path: 'audit-logs',
    loadComponent: () =>
      import('./audit-logs/audit-logs.component').then((m) => m.AuditLogsComponent),
  },
  {
    path: 'feature-flags',
    loadComponent: () =>
      import('./feature-flags/feature-flags.component').then((m) => m.FeatureFlagsComponent),
  },
  {
    path: 'workflow',
    loadComponent: () =>
      import('./workflow-management/workflow-management.component').then(
        (m) => m.WorkflowManagementComponent,
      ),
  },
];
