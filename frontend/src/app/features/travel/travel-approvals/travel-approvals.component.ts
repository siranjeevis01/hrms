import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FormsModule } from '@angular/forms';
import { TravelService } from '../travel.service';
import { TravelRequest } from '../travel.models';

@Component({
  selector: 'app-travel-approvals',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatProgressSpinnerModule, FormsModule, DatePipe, TitleCasePipe],
  templateUrl: './travel-approvals.component.html',
  styleUrl: './travel-approvals.component.scss',
})
export class TravelApprovalsComponent implements OnInit {
  private travelService = inject(TravelService);
  requests = signal<TravelRequest[]>([]);
  loading = signal(true);
  rejectComment = signal('');
  selectedId = signal<string | null>(null);

  ngOnInit(): void { this.loadRequests(); }

  loadRequests(): void {
    this.loading.set(true);
    this.travelService.getPendingApprovals().subscribe({
      next: (reqs) => { this.requests.set(reqs); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  approve(id: string): void { this.travelService.approveRequest(id, true, '').subscribe({ next: () => this.loadRequests() }); }
  reject(id: string): void {
    this.travelService.approveRequest(id, false, this.rejectComment()).subscribe({
      next: () => { this.rejectComment.set(''); this.selectedId.set(null); this.loadRequests(); },
    });
  }

  formatCurrency(amount: number): string { return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD', maximumFractionDigits: 0 }).format(amount); }
}
