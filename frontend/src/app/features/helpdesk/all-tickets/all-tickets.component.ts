import { Component, OnInit, inject, signal } from '@angular/core';
import { NgClass, TitleCasePipe, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatMenuModule } from '@angular/material/menu';
import { HelpdeskService } from '../helpdesk.service';
import { Ticket, TicketFilters } from '../helpdesk.models';

@Component({
  selector: 'app-all-tickets',
  standalone: true,
  imports: [NgClass, TitleCasePipe, DatePipe, FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatSelectModule, MatTableModule, MatProgressSpinnerModule, MatMenuModule],
  templateUrl: './all-tickets.component.html',
  styleUrl: './all-tickets.component.scss',
})
export class AllTicketsComponent implements OnInit {
  private helpdeskService = inject(HelpdeskService);
  tickets = signal<Ticket[]>([]);
  loading = signal(true);
  searchQuery = signal('');
  statusFilter = signal('');
  priorityFilter = signal('');
  displayedColumns = ['id', 'subject', 'employee', 'priority', 'status', 'assignee', 'createdAt', 'actions'];

  ngOnInit(): void { this.loadTickets(); }

  loadTickets(): void {
    this.loading.set(true);
    const filters: TicketFilters = {};
    if (this.searchQuery()) filters.search = this.searchQuery();
    if (this.statusFilter()) filters.status = this.statusFilter() as any;
    if (this.priorityFilter()) filters.priority = this.priorityFilter() as any;
    this.helpdeskService.getAllTickets(filters).subscribe({
      next: (tickets) => { this.tickets.set(tickets); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
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
