import { Routes } from '@angular/router';

export const PROJECTS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./project-list/project-list.component').then((m) => m.ProjectListComponent),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./project-create/project-create.component').then((m) => m.ProjectCreateComponent),
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./project-detail/project-detail.component').then((m) => m.ProjectDetailComponent),
  },
  {
    path: ':id/board',
    loadComponent: () =>
      import('./board/board.component').then((m) => m.BoardComponent),
  },
  {
    path: ':id/backlog',
    loadComponent: () =>
      import('./backlog/backlog.component').then((m) => m.BacklogComponent),
  },
  {
    path: ':id/sprints',
    loadComponent: () =>
      import('./sprint-list/sprint-list.component').then((m) => m.SprintListComponent),
  },
  {
    path: ':id/sprints/:sprintId',
    loadComponent: () =>
      import('./sprint-detail/sprint-detail.component').then((m) => m.SprintDetailComponent),
  },
];
