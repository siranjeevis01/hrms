import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
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
  private ticketsApi = `${environment.apiUrl}/api/helpdesk/Tickets`;
  private categoriesApi = `${environment.apiUrl}/api/helpdesk/TicketCategories`;
  private knowledgeApi = `${environment.apiUrl}/api/helpdesk/KnowledgeArticles`;
  private faqsApi = `${environment.apiUrl}/api/helpdesk/Faqs`;

  getStats(): Observable<HelpdeskStats> {
    return this.http.get<HelpdeskStats>(`${this.ticketsApi}`);
  }

  getMyTickets(filters?: TicketFilters): Observable<Ticket[]> {
    let params = new HttpParams();
    if (filters) {
      Object.entries(filters).forEach(([key, value]) => {
        if (value) params = params.set(key, value);
      });
    }
    return this.http.get<Ticket[]>(`${this.ticketsApi}`, { params });
  }

  getAllTickets(filters?: TicketFilters): Observable<Ticket[]> {
    let params = new HttpParams();
    if (filters) {
      Object.entries(filters).forEach(([key, value]) => {
        if (value) params = params.set(key, value);
      });
    }
    return this.http.get<Ticket[]>(`${this.ticketsApi}`, { params });
  }

  getTicket(id: string): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.ticketsApi}/${id}`);
  }

  createTicket(request: CreateTicketRequest): Observable<Ticket> {
    return this.http.post<Ticket>(`${this.ticketsApi}`, request);
  }

  updateTicket(id: string, request: Partial<CreateTicketRequest>): Observable<Ticket> {
    return this.http.put<Ticket>(`${this.ticketsApi}/${id}`, request);
  }

  assignTicket(ticketId: string, userId: string): Observable<void> {
    return this.http.post<void>(`${this.ticketsApi}/${ticketId}/assign`, { userId });
  }

  updateTicketStatus(ticketId: string, status: string): Observable<void> {
    return this.http.patch<void>(`${this.ticketsApi}/${ticketId}/status`, { status });
  }

  addComment(ticketId: string, content: string, isInternal: boolean): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/api/tickets/${ticketId}/comments`, {
      content,
      isInternal,
    });
  }

  getCategories(): Observable<TicketCategory[]> {
    return this.http.get<TicketCategory[]>(`${this.categoriesApi}`);
  }

  getKnowledgeArticles(search?: string, category?: string): Observable<KnowledgeArticle[]> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    if (category) params = params.set('category', category);
    return this.http.get<KnowledgeArticle[]>(`${this.knowledgeApi}`, { params });
  }

  getKnowledgeArticle(id: string): Observable<KnowledgeArticle> {
    return this.http.get<KnowledgeArticle>(`${this.knowledgeApi}/${id}`);
  }

  getFaqs(search?: string, category?: string): Observable<Faq[]> {
    let params = new HttpParams();
    if (search) params = params.set('search', search);
    if (category) params = params.set('category', category);
    return this.http.get<Faq[]>(`${this.faqsApi}`, { params });
  }

  markFaqHelpful(id: string): Observable<void> {
    return this.http.post<void>(`${this.faqsApi}/${id}/helpful`, {});
  }
}
