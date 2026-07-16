import { Component, OnInit, inject, signal } from '@angular/core';
import { NgClass, TitleCasePipe, DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HelpdeskService } from '../helpdesk.service';
import { Ticket } from '../helpdesk.models';

@Component({
  selector: 'app-my-tickets',
  standalone: true,
  imports: [NgClass, TitleCasePipe, DatePipe, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule, MatTabsModule, MatProgressSpinnerModule],
  templateUrl: './my-tickets.component.html',
  styleUrl: './my-tickets.component.scss',
})
export class MyTicketsComponent implements OnInit {
  private helpdeskService = inject(HelpdeskService);
  protected router = inject(Router);
  tickets = signal<Ticket[]>([]);
  loading = signal(true);
  statusFilter = signal<string>('');

  ngOnInit(): void { this.loadTickets(); }

  loadTickets(): void {
    this.loading.set(true);
    this.helpdeskService.getMyTickets({ status: this.statusFilter() as any }).subscribe({
      next: (tickets) => { this.tickets.set(tickets); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  onTabChange(index: number): void {
    const statuses = ['', 'open', 'in_progress', 'resolved', 'closed'];
    this.statusFilter.set(statuses[index]);
    this.loadTickets();
  }

  getStatusColor(status: string): string {
    const c: Record<string, string> = { open: 'bg-blue-100 text-blue-700', in_progress: 'bg-yellow-100 text-yellow-700', resolved: 'bg-green-100 text-green-700', closed: 'bg-gray-100 text-gray-600' };
    return c[status] || 'bg-gray-100 text-gray-600';
  }
  getPriorityColor(priority: string): string {
    const c: Record<string, string> = { low: 'bg-gray-100 text-gray-600', medium: 'bg-blue-100 text-blue-700', high: 'bg-orange-100 text-orange-700', urgent: 'bg-red-100 text-red-700' };
    return c[priority] || 'bg-gray-100 text-gray-600';
  }
}
