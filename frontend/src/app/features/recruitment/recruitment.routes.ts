import { Routes } from '@angular/router';

export const RECRUITMENT_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./recruitment-dashboard/recruitment-dashboard.component').then(
        (m) => m.RecruitmentDashboardComponent,
      ),
  },
  {
    path: 'jobs',
    loadComponent: () =>
      import('./job-listings/job-listings.component').then(
        (m) => m.JobListingsComponent,
      ),
  },
  {
    path: 'jobs/create',
    loadComponent: () =>
      import('./job-create/job-create.component').then(
        (m) => m.JobCreateComponent,
      ),
  },
  {
    path: 'jobs/:id',
    loadComponent: () =>
      import('./job-detail/job-detail.component').then(
        (m) => m.JobDetailComponent,
      ),
  },
  {
    path: 'jobs/:id/applications',
    loadComponent: () =>
      import('./job-applications/job-applications.component').then(
        (m) => m.JobApplicationsComponent,
      ),
  },
  {
    path: 'candidates',
    loadComponent: () =>
      import('./candidate-list/candidate-list.component').then(
        (m) => m.CandidateListComponent,
      ),
  },
  {
    path: 'candidates/:id',
    loadComponent: () =>
      import('./candidate-detail/candidate-detail.component').then(
        (m) => m.CandidateDetailComponent,
      ),
  },
  {
    path: 'interviews',
    loadComponent: () =>
      import('./interview-schedule/interview-schedule.component').then(
        (m) => m.InterviewScheduleComponent,
      ),
  },
  {
    path: 'offers',
    loadComponent: () =>
      import('./offer-management/offer-management.component').then(
        (m) => m.OfferManagementComponent,
      ),
  },
  {
    path: 'pipeline',
    loadComponent: () =>
      import('./recruitment-pipeline/recruitment-pipeline.component').then(
        (m) => m.RecruitmentPipelineComponent,
      ),
  },
  {
    path: 'onboarding',
    loadComponent: () =>
      import('./onboarding/onboarding.component').then(
        (m) => m.OnboardingComponent,
      ),
  },
];
