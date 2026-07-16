import { Routes } from '@angular/router';

export const ATTENDANCE_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./attendance-dashboard/attendance-dashboard.component').then(
        (m) => m.AttendanceDashboardComponent,
      ),
  },
  {
    path: 'my-attendance',
    loadComponent: () =>
      import('./my-attendance/my-attendance.component').then(
        (m) => m.MyAttendanceComponent,
      ),
  },
  {
    path: 'team-attendance',
    loadComponent: () =>
      import('./team-attendance/team-attendance.component').then(
        (m) => m.TeamAttendanceComponent,
      ),
  },
  {
    path: 'mark',
    loadComponent: () =>
      import('./mark-attendance/mark-attendance.component').then(
        (m) => m.MarkAttendanceComponent,
      ),
  },
  {
    path: 'reports',
    loadComponent: () =>
      import('./attendance-reports/attendance-reports.component').then(
        (m) => m.AttendanceReportsComponent,
      ),
  },
];
