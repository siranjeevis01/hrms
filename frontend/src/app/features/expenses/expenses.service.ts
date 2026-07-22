import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ExpenseClaim,
  ExpensePolicy,
  ExpenseDashboardStats,
  SubmitExpenseRequest,
  ApproveExpenseRequest,
} from './expenses.models';

@Injectable({ providedIn: 'root' })
export class ExpensesService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/expenses/ExpenseClaims`;
  private policiesApi = `${environment.apiUrl}/api/expenses/ExpensePolicies`;

  getDashboardStats(): Observable<ExpenseDashboardStats> {
    return this.http.get<ExpenseDashboardStats>(`${this.apiUrl}/dashboard`);
  }

  getMyExpenses(status?: string): Observable<ExpenseClaim[]> {
    let params = new HttpParams();
    if (status) params = params.set('status', status);
    return this.http.get<ExpenseClaim[]>(`${this.apiUrl}/my`, { params });
  }

  getExpense(id: string): Observable<ExpenseClaim> {
    return this.http.get<ExpenseClaim>(`${this.apiUrl}/${id}`);
  }

  submitExpense(request: SubmitExpenseRequest): Observable<ExpenseClaim> {
    return this.http.post<ExpenseClaim>(this.apiUrl, request);
  }

  updateExpense(id: string, request: Partial<SubmitExpenseRequest>): Observable<ExpenseClaim> {
    return this.http.put<ExpenseClaim>(`${this.apiUrl}/${id}`, request);
  }

  deleteExpense(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  cancelExpense(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/cancel`, {});
  }

  getPendingApprovals(): Observable<ExpenseClaim[]> {
    return this.http.get<ExpenseClaim[]>(`${this.apiUrl}/approvals`);
  }

  approveExpense(request: ApproveExpenseRequest): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${request.expenseId}/approve`, {
      approved: request.approved,
      comments: request.comments,
    });
  }

  getPolicies(): Observable<ExpensePolicy[]> {
    return this.http.get<ExpensePolicy[]>(`${this.policiesApi}`);
  }

  createPolicy(policy: Partial<ExpensePolicy>): Observable<ExpensePolicy> {
    return this.http.post<ExpensePolicy>(`${this.policiesApi}`, policy);
  }

  updatePolicy(id: string, policy: Partial<ExpensePolicy>): Observable<ExpensePolicy> {
    return this.http.put<ExpensePolicy>(`${this.policiesApi}/${id}`, policy);
  }

  deletePolicy(id: string): Observable<void> {
    return this.http.delete<void>(`${this.policiesApi}/${id}`);
  }
}
