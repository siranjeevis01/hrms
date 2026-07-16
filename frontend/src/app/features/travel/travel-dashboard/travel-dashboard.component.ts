import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { NgClass, DatePipe, TitleCasePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { TravelService } from '../travel.service';
import { TravelDashboardStats, TravelRequest } from '../travel.models';

@Component({
  selector: 'app-travel-dashboard',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatListModule, MatProgressSpinnerModule, NgClass, DatePipe, TitleCasePipe],
  templateUrl: './travel-dashboard.component.html',
  styleUrl: './travel-dashboard.component.scss',
})
export class TravelDashboardComponent implements OnInit {
  private travelService = inject(TravelService);
  private router = inject(Router);

  stats = signal<TravelDashboardStats | null>(null);
  loading = signal(true);

  ngOnInit(): void {
    this.travelService.getDashboardStats().subscribe({
      next: (stats) => { this.stats.set(stats); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  navigateTo(path: string): void { this.router.navigate(['/travel', path]); }
  formatCurrency(amount: number): string { return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(amount); }
  getStatusColor(status: string): string {
    const colors: Record<string, string> = { draft: 'bg-gray-100 text-gray-700', submitted: 'bg-blue-100 text-blue-700', approved: 'bg-green-100 text-green-700', rejected: 'bg-red-100 text-red-700', completed: 'bg-purple-100 text-purple-700', cancelled: 'bg-gray-100 text-gray-500' };
    return colors[status] || 'bg-gray-100 text-gray-700';
  }
}
