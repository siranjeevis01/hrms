import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  Ticket,
  TicketCategory,
  KnowledgeArticle,
  Faq,
  HelpdeskStats,
  CreateTicketRequest,
  TicketFilters,
} from './helpdesk.models';

@Injectable({ providedIn: 'root' })
export class HelpdeskService {
  private http = inject(HttpClient);
  private apiUrl = '/api/helpdesk';

  getStats(): Observable<HelpdeskStats> {
    return this.http.get<HelpdeskStats>(`${this.apiUrl}/stats`);
  }

  getMyTickets(filters?: TicketFilters): Observable<Ticket[]> {
    let params = new HttpParams();
    if (filters) {
      Object.entries(filters).forEach(([key, value]) => {
        if (value) params = params.set(key, value);
      });
    }
    return this.http.get<Ticket[]>(`${this.apiUrl}/my-tickets`, { params });
  }

  getAllTickets(filters?: TicketFilters): Observable<Ticket[]> {
    let params = new HttpParams();
    if (filters) {
      Object.entries(filters).forEach(([key, value]) => {
        if (value) params = params.set(key, value);
      });
    }
    return this.http.get<Ticket[]>(`${this.apiUrl}/tickets`, { params });
  }

  getTicket(id: string): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.apiUrl}/tickets/${id}`);
  }

  createTicket(request: CreateTicketRequest): Observable<Ticket> {
    return this.http.post<Ticket>(`${this.apiUrl}/tickets`, request);
  }

  updateTicket(id: string, request: Partial<CreateTicketRequest>): Observable<Ticket> {
    return this.http.put<Ticket>(`${this.apiUrl}/tickets/${id}`, request);
  }

  assignTicket(ticketId: string, userId: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/tickets/${ticketId}/assign`, { userId });
  }

  updateTicketStatus(ticketId: string, status: string): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/tickets/${ticketId}/status`, { status });
  }

  addComment(ticketId: string, content: string, isInternal: boolean): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/tickets/${ticketId}/comments`, {
      content,
      isInternal,
    });
  }

  getCategories(): Observable<TicketCategory[]> {
    return this.http.get<TicketCategory[]>(`${this.apiUrl}/categories`);
  }

  getKnowledgeArticles(search?: string, category?: string): Observable<KnowledgeArticle[]> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    if (category) params = params.set('category', category);
    return this.http.get<KnowledgeArticle[]>(`${this.apiUrl}/knowledge-base`, { params });
  }

  getKnowledgeArticle(id: string): Observable<KnowledgeArticle> {
    return this.http.get<KnowledgeArticle>(`${this.apiUrl}/knowledge-base/${id}`);
  }

  getFaqs(search?: string, category?: string): Observable<Faq[]> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    if (category) params = params.set('category', category);
    return this.http.get<Faq[]>(`${this.apiUrl}/faqs`, { params });
  }

  markFaqHelpful(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/faqs/${id}/helpful`, {});
  }
}
