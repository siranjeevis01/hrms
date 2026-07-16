import { Routes } from '@angular/router';

export const HELPDESK_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./helpdesk-dashboard/helpdesk-dashboard.component').then(
        (m) => m.HelpdeskDashboardComponent,
      ),
  },
  {
    path: 'my-tickets',
    loadComponent: () =>
      import('./my-tickets/my-tickets.component').then((m) => m.MyTicketsComponent),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./create-ticket/create-ticket.component').then((m) => m.CreateTicketComponent),
  },
  {
    path: 'all-tickets',
    loadComponent: () =>
      import('./all-tickets/all-tickets.component').then((m) => m.AllTicketsComponent),
  },
  {
    path: 'knowledge-base',
    loadComponent: () =>
      import('./knowledge-base/knowledge-base.component').then((m) => m.KnowledgeBaseComponent),
  },
  {
    path: 'faqs',
    loadComponent: () => import('./faqs/faqs.component').then((m) => m.FaqsComponent),
  },
];
