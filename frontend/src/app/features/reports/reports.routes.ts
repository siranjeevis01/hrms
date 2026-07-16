import { Routes } from '@angular/router';

export const REPORTS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./report-list/report-list.component').then((m) => m.ReportListComponent),
  },
  {
    path: 'generate/:templateId',
    loadComponent: () =>
      import('./report-generate/report-generate.component').then(
        (m) => m.ReportGenerateComponent,
      ),
  },
  {
    path: 'scheduled',
    loadComponent: () =>
      import('./scheduled-reports/scheduled-reports.component').then(
        (m) => m.ScheduledReportsComponent,
      ),
  },
];
