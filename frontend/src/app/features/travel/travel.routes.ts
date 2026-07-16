import { Routes } from '@angular/router';

export const TRAVEL_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./travel-dashboard/travel-dashboard.component').then(
        (m) => m.TravelDashboardComponent,
      ),
  },
  {
    path: 'my-requests',
    loadComponent: () =>
      import('./my-travel-requests/my-travel-requests.component').then(
        (m) => m.MyTravelRequestsComponent,
      ),
  },
  {
    path: 'submit',
    loadComponent: () =>
      import('./submit-travel-request/submit-travel-request.component').then(
        (m) => m.SubmitTravelRequestComponent,
      ),
  },
  {
    path: 'approvals',
    loadComponent: () =>
      import('./travel-approvals/travel-approvals.component').then(
        (m) => m.TravelApprovalsComponent,
      ),
  },
];
