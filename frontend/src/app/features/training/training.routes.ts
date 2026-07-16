import { Routes } from '@angular/router';

export const TRAINING_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./training-dashboard/training-dashboard.component').then(
        (m) => m.TrainingDashboardComponent,
      ),
  },
  {
    path: 'courses',
    loadComponent: () =>
      import('./course-list/course-list.component').then(
        (m) => m.CourseListComponent,
      ),
  },
  {
    path: 'courses/create',
    loadComponent: () =>
      import('./course-create/course-create.component').then(
        (m) => m.CourseCreateComponent,
      ),
  },
  {
    path: 'courses/:id',
    loadComponent: () =>
      import('./course-detail/course-detail.component').then(
        (m) => m.CourseDetailComponent,
      ),
  },
  {
    path: 'my-learning',
    loadComponent: () =>
      import('./my-learning/my-learning.component').then(
        (m) => m.MyLearningComponent,
      ),
  },
  {
    path: 'assessments',
    loadComponent: () =>
      import('./assessment-list/assessment-list.component').then(
        (m) => m.AssessmentListComponent,
      ),
  },
  {
    path: 'certificates',
    loadComponent: () =>
      import('./certificate-list/certificate-list.component').then(
        (m) => m.CertificateListComponent,
      ),
  },
  {
    path: 'learning-paths',
    loadComponent: () =>
      import('./learning-paths/learning-paths.component').then(
        (m) => m.LearningPathsComponent,
      ),
  },
  {
    path: 'schedule',
    loadComponent: () =>
      import('./training-schedule/training-schedule.component').then(
        (m) => m.TrainingScheduleComponent,
      ),
  },
];
