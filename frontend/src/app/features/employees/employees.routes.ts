import { Routes } from '@angular/router';

export const EMPLOYEES_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./employee-list/employee-list.component').then(
        (m) => m.EmployeeListComponent,
      ),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./employee-create/employee-create.component').then(
        (m) => m.EmployeeCreateComponent,
      ),
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./employee-detail/employee-detail.component').then(
        (m) => m.EmployeeDetailComponent,
      ),
  },
  {
    path: ':id/edit',
    loadComponent: () =>
      import('./employee-edit/employee-edit.component').then(
        (m) => m.EmployeeEditComponent,
      ),
  },
  {
    path: ':id/personal',
    loadComponent: () =>
      import('./employee-personal/employee-personal.component').then(
        (m) => m.EmployeePersonalComponent,
      ),
  },
  {
    path: ':id/documents',
    loadComponent: () =>
      import('./employee-documents/employee-documents.component').then(
        (m) => m.EmployeeDocumentsComponent,
      ),
  },
  {
    path: ':id/salary',
    loadComponent: () =>
      import('./employee-salary/employee-salary.component').then(
        (m) => m.EmployeeSalaryComponent,
      ),
  },
  {
    path: ':id/history',
    loadComponent: () =>
      import('./employee-history/employee-history.component').then(
        (m) => m.EmployeeHistoryComponent,
      ),
  },
];
