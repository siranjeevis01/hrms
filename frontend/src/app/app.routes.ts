import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'projects',
    loadChildren: () =>
      import('./features/projects/projects.routes').then((m) => m.PROJECTS_ROUTES),
  },
  {
    path: 'expenses',
    loadChildren: () =>
      import('./features/expenses/expenses.routes').then((m) => m.EXPENSES_ROUTES),
  },
  {
    path: 'travel',
    loadChildren: () =>
      import('./features/travel/travel.routes').then((m) => m.TRAVEL_ROUTES),
  },
  {
    path: 'helpdesk',
    loadChildren: () =>
      import('./features/helpdesk/helpdesk.routes').then((m) => m.HELPDESK_ROUTES),
  },
  {
    path: 'chat',
    loadChildren: () =>
      import('./features/chat/chat.routes').then((m) => m.CHAT_ROUTES),
  },
  {
    path: 'documents',
    loadChildren: () =>
      import('./features/documents/documents.routes').then((m) => m.DOCUMENTS_ROUTES),
  },
  {
    path: 'reports',
    loadChildren: () =>
      import('./features/reports/reports.routes').then((m) => m.REPORTS_ROUTES),
  },
  {
    path: 'admin',
    loadChildren: () =>
      import('./features/admin/admin.routes').then((m) => m.ADMIN_ROUTES),
  },
  {
    path: 'profile',
    loadChildren: () =>
      import('./features/profile/profile.routes').then((m) => m.PROFILE_ROUTES),
  },
];
