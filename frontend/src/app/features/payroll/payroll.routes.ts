import { Routes } from '@angular/router';

export const PAYROLL_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./payroll-dashboard/payroll-dashboard.component').then(
        (m) => m.PayrollDashboardComponent,
      ),
  },
  {
    path: 'run',
    loadComponent: () =>
      import('./payroll-run/payroll-run.component').then(
        (m) => m.PayrollRunComponent,
      ),
  },
  {
    path: 'payslips',
    loadComponent: () =>
      import('./payslips/payslips.component').then(
        (m) => m.PayslipsComponent,
      ),
  },
  {
    path: 'salary-structures',
    loadComponent: () =>
      import('./salary-structures/salary-structures.component').then(
        (m) => m.SalaryStructuresComponent,
      ),
  },
  {
    path: 'components',
    loadComponent: () =>
      import('./salary-components/salary-components.component').then(
        (m) => m.SalaryComponentsComponent,
      ),
  },
  {
    path: 'tax',
    loadComponent: () =>
      import('./tax-calculator/tax-calculator.component').then(
        (m) => m.TaxCalculatorComponent,
      ),
  },
  {
    path: 'reports',
    loadComponent: () =>
      import('./payroll-reports/payroll-reports.component').then(
        (m) => m.PayrollReportsComponent,
      ),
  },
];
