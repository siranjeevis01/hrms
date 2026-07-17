import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { LayoutComponent } from './layout/layout';

export const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.routes').then((m) => m.AUTH_ROUTES),
  },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full',
      },
      {
        path: 'dashboard',
        loadChildren: () =>
          import('./features/dashboard/dashboard.routes').then(
            (m) => m.DASHBOARD_ROUTES,
          ),
      },
      {
        path: 'projects',
        loadChildren: () =>
          import('./features/projects/projects.routes').then(
            (m) => m.PROJECTS_ROUTES,
          ),
      },
      {
        path: 'leave',
        loadChildren: () =>
          import('./features/leave/leave.routes').then(
            (m) => m.LEAVE_ROUTES,
          ),
      },
      {
        path: 'expenses',
        loadChildren: () =>
          import('./features/expenses/expenses.routes').then(
            (m) => m.EXPENSES_ROUTES,
          ),
      },
      {
        path: 'travel',
        loadChildren: () =>
          import('./features/travel/travel.routes').then(
            (m) => m.TRAVEL_ROUTES,
          ),
      },
      {
        path: 'helpdesk',
        loadChildren: () =>
          import('./features/helpdesk/helpdesk.routes').then(
            (m) => m.HELPDESK_ROUTES,
          ),
      },
      {
        path: 'chat',
        loadChildren: () =>
          import('./features/chat/chat.routes').then((m) => m.CHAT_ROUTES),
      },
      {
        path: 'documents',
        loadChildren: () =>
          import('./features/documents/documents.routes').then(
            (m) => m.DOCUMENTS_ROUTES,
          ),
      },
      {
        path: 'reports',
        loadChildren: () =>
          import('./features/reports/reports.routes').then(
            (m) => m.REPORTS_ROUTES,
          ),
      },
      {
        path: 'employees',
        loadChildren: () =>
          import('./features/employees/employees.routes').then(
            (m) => m.EMPLOYEES_ROUTES,
          ),
      },
      {
        path: 'attendance',
        loadChildren: () =>
          import('./features/attendance/attendance.routes').then(
            (m) => m.ATTENDANCE_ROUTES,
          ),
      },
      {
        path: 'payroll',
        loadChildren: () =>
          import('./features/payroll/payroll.routes').then(
            (m) => m.PAYROLL_ROUTES,
          ),
      },
      {
        path: 'recruitment',
        loadChildren: () =>
          import('./features/recruitment/recruitment.routes').then(
            (m) => m.RECRUITMENT_ROUTES,
          ),
      },
      {
        path: 'training',
        loadChildren: () =>
          import('./features/training/training.routes').then(
            (m) => m.TRAINING_ROUTES,
          ),
      },
      {
        path: 'performance',
        loadChildren: () =>
          import('./features/performance/performance.routes').then(
            (m) => m.PERFORMANCE_ROUTES,
          ),
      },
      {
        path: 'admin',
        loadChildren: () =>
          import('./features/admin/admin.routes').then(
            (m) => m.ADMIN_ROUTES,
          ),
      },
      {
        path: 'profile',
        loadChildren: () =>
          import('./features/profile/profile.routes').then(
            (m) => m.PROFILE_ROUTES,
          ),
      },
    ],
  },
  {
    path: '**',
    redirectTo: '',
  },
];
