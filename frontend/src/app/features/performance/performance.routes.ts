import { Routes } from '@angular/router';

export const PERFORMANCE_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./performance-dashboard/performance-dashboard.component').then(
        (m) => m.PerformanceDashboardComponent,
      ),
  },
  {
    path: 'goals',
    loadComponent: () =>
      import('./goals/goals.component').then((m) => m.GoalsComponent),
  },
  {
    path: 'goals/create',
    loadComponent: () =>
      import('./goal-create/goal-create.component').then(
        (m) => m.GoalCreateComponent,
      ),
  },
  {
    path: 'goals/:id',
    loadComponent: () =>
      import('./goal-detail/goal-detail.component').then(
        (m) => m.GoalDetailComponent,
      ),
  },
  {
    path: 'okrs',
    loadComponent: () =>
      import('./okrs/okrs.component').then((m) => m.OKRComponent),
  },
  {
    path: 'kpis',
    loadComponent: () =>
      import('./kpis/kpis.component').then((m) => m.KPIComponent),
  },
  {
    path: 'reviews',
    loadComponent: () =>
      import('./reviews/reviews.component').then((m) => m.ReviewsComponent),
  },
  {
    path: 'reviews/:id',
    loadComponent: () =>
      import('./review-detail/review-detail.component').then(
        (m) => m.ReviewDetailComponent,
      ),
  },
  {
    path: 'feedback',
    loadComponent: () =>
      import('./feedback360/feedback360.component').then(
        (m) => m.Feedback360Component,
      ),
  },
  {
    path: 'appraisals',
    loadComponent: () =>
      import('./appraisals/appraisals.component').then(
        (m) => m.AppraisalsComponent,
      ),
  },
  {
    path: 'calibration',
    loadComponent: () =>
      import('./calibration/calibration.component').then(
        (m) => m.CalibrationComponent,
      ),
  },
];
