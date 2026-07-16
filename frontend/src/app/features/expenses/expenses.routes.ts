import { Routes } from '@angular/router';

export const EXPENSES_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./expense-dashboard/expense-dashboard.component').then(
        (m) => m.ExpenseDashboardComponent,
      ),
  },
  {
    path: 'my-expenses',
    loadComponent: () =>
      import('./my-expenses/my-expenses.component').then((m) => m.MyExpensesComponent),
  },
  {
    path: 'submit',
    loadComponent: () =>
      import('./submit-expense/submit-expense.component').then((m) => m.SubmitExpenseComponent),
  },
  {
    path: 'approvals',
    loadComponent: () =>
      import('./expense-approvals/expense-approvals.component').then(
        (m) => m.ExpenseApprovalsComponent,
      ),
  },
  {
    path: 'policies',
    loadComponent: () =>
      import('./expense-policies/expense-policies.component').then(
        (m) => m.ExpensePoliciesComponent,
      ),
  },
];
