import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { NgClass, DatePipe, TitleCasePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTabsModule } from '@angular/material/tabs';
import { TravelService } from '../travel.service';
import { TravelRequest, TravelStatus } from '../travel.models';

@Component({
  selector: 'app-my-travel-requests',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatChipsModule, MatMenuModule, MatProgressSpinnerModule, MatTabsModule, NgClass, DatePipe, TitleCasePipe],
  templateUrl: './my-travel-requests.component.html',
  styleUrl: './my-travel-requests.component.scss',
})
export class MyTravelRequestsComponent implements OnInit {
  private travelService = inject(TravelService);
  protected router = inject(Router);
  requests = signal<TravelRequest[]>([]);
  loading = signal(true);
  activeTab = signal<TravelStatus | ''>('');

  ngOnInit(): void { this.loadRequests(); }

  loadRequests(): void {
    this.loading.set(true);
    this.travelService.getMyRequests(this.activeTab() || undefined).subscribe({
      next: (reqs) => { this.requests.set(reqs); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  onTabChange(index: number): void {
    const statuses: (TravelStatus | '')[] = ['', 'draft', 'submitted', 'approved', 'rejected', 'completed'];
    this.activeTab.set(statuses[index] ?? '');
    this.loadRequests();
  }

  cancelRequest(id: string): void { this.travelService.cancelRequest(id).subscribe({ next: () => this.loadRequests() }); }
  formatCurrency(amount: number): string { return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(amount); }
  getStatusColor(status: string): string {
    const colors: Record<string, string> = { draft: 'bg-gray-100 text-gray-700', submitted: 'bg-blue-100 text-blue-700', approved: 'bg-green-100 text-green-700', rejected: 'bg-red-100 text-red-700', completed: 'bg-purple-100 text-purple-700', cancelled: 'bg-gray-100 text-gray-500' };
    return colors[status] || 'bg-gray-100 text-gray-700';
  }
}
