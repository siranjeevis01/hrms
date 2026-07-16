import { Component, OnInit, inject, signal } from '@angular/core';
import { NgClass, TitleCasePipe, DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HelpdeskService } from '../helpdesk.service';
import { HelpdeskStats, Ticket, KnowledgeArticle } from '../helpdesk.models';

@Component({
  selector: 'app-helpdesk-dashboard',
  standalone: true,
  imports: [NgClass, TitleCasePipe, DatePipe, MatCardModule, MatButtonModule, MatIconModule, MatListModule, MatProgressSpinnerModule],
  templateUrl: './helpdesk-dashboard.component.html',
  styleUrl: './helpdesk-dashboard.component.scss',
})
export class HelpdeskDashboardComponent implements OnInit {
  private helpdeskService = inject(HelpdeskService);
  private router = inject(Router);

  stats = signal<HelpdeskStats | null>(null);
  recentTickets = signal<Ticket[]>([]);
  popularArticles = signal<KnowledgeArticle[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    this.helpdeskService.getStats().subscribe({
      next: (stats) => { this.stats.set(stats); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
    this.helpdeskService.getMyTickets().subscribe({ next: (tickets) => this.recentTickets.set(tickets.slice(0, 5)) });
    this.helpdeskService.getKnowledgeArticles().subscribe({ next: (articles) => this.popularArticles.set(articles.slice(0, 4)) });
  }

  navigateTo(path: string): void { this.router.navigate(['/helpdesk', path]); }
  getStatusColor(status: string): string {
    const c: Record<string, string> = { open: 'bg-blue-100 text-blue-700', in_progress: 'bg-yellow-100 text-yellow-700', resolved: 'bg-green-100 text-green-700', closed: 'bg-gray-100 text-gray-600' };
    return c[status] || 'bg-gray-100 text-gray-600';
  }
  getPriorityColor(priority: string): string {
    const c: Record<string, string> = { low: 'bg-gray-100 text-gray-600', medium: 'bg-blue-100 text-blue-700', high: 'bg-orange-100 text-orange-700', urgent: 'bg-red-100 text-red-700' };
    return c[priority] || 'bg-gray-100 text-gray-600';
  }
}
