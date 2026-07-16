import { Routes } from '@angular/router';

export const LEAVE_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./leave-dashboard/leave-dashboard.component').then(
        (m) => m.LeaveDashboardComponent,
      ),
  },
  {
    path: 'apply',
    loadComponent: () =>
      import('./apply-leave/apply-leave.component').then(
        (m) => m.ApplyLeaveComponent,
      ),
  },
  {
    path: 'my-leaves',
    loadComponent: () =>
      import('./my-leaves/my-leaves.component').then(
        (m) => m.MyLeavesComponent,
      ),
  },
  {
    path: 'team-leaves',
    loadComponent: () =>
      import('./team-leaves/team-leaves.component').then(
        (m) => m.TeamLeavesComponent,
      ),
  },
  {
    path: 'balances',
    loadComponent: () =>
      import('./leave-balances/leave-balances.component').then(
        (m) => m.LeaveBalancesComponent,
      ),
  },
  {
    path: 'calendar',
    loadComponent: () =>
      import('./leave-calendar/leave-calendar.component').then(
        (m) => m.LeaveCalendarComponent,
      ),
  },
  {
    path: 'policies',
    loadComponent: () =>
      import('./leave-policies/leave-policies.component').then(
        (m) => m.LeavePoliciesComponent,
      ),
  },
];
