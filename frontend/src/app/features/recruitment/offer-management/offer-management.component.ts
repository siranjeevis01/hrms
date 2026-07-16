import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTabsModule } from '@angular/material/tabs';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatMenuModule } from '@angular/material/menu';
import { DatePipe, CurrencyPipe, TitleCasePipe, DecimalPipe } from '@angular/common';
import { RecruitmentService } from '../recruitment.service';
import { Offer, Candidate, Job, OfferStatus } from '../recruitment.models';

@Component({
  selector: 'app-offer-management',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule,
    MatSelectModule, MatDatepickerModule, MatNativeDateModule, MatTabsModule,
    MatProgressSpinnerModule, MatSnackBarModule, MatMenuModule, DatePipe, CurrencyPipe, TitleCasePipe, DecimalPipe,
  ],
  templateUrl: './offer-management.component.html',
  styleUrl: './offer-management.component.scss',
})
export class OfferManagementComponent implements OnInit {
  private recruitmentService = inject(RecruitmentService);
  private snackBar = inject(MatSnackBar);

  offers = signal<Offer[]>([]);
  candidates = signal<Candidate[]>([]);
  jobs = signal<Job[]>([]);
  loading = signal(true);
  showCreateForm = signal(false);
  minDate = new Date();

  newOffer = {
    candidateId: '', jobId: '', position: '', department: '',
    ctc: 0, basicSalary: 0, joiningDate: new Date(), expiryDate: new Date(),
  };

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.loading.set(true);
    this.recruitmentService.getOffers().subscribe({
      next: (offers) => { this.offers.set(offers); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
    this.recruitmentService.getCandidates().subscribe({
      next: (c) => this.candidates.set(c), error: () => {},
    });
    this.recruitmentService.getJobs().subscribe({
      next: (j) => this.jobs.set(j), error: () => {},
    });
  }

  createOffer(): void {
    if (!this.newOffer.candidateId || !this.newOffer.jobId) {
      this.snackBar.open('Please fill required fields', 'Close', { duration: 3000 });
      return;
    }
    this.recruitmentService.createOffer(this.newOffer).subscribe({
      next: () => {
        this.snackBar.open('Offer created', 'Close', { duration: 3000 });
        this.showCreateForm.set(false);
        this.loadData();
      },
      error: () => this.snackBar.open('Failed to create offer', 'Close', { duration: 3000 }),
    });
  }

  updateStatus(id: string, status: string): void {
    this.recruitmentService.updateOfferStatus(id, status).subscribe({
      next: () => { this.snackBar.open('Offer updated', 'Close', { duration: 2000 }); this.loadData(); },
      error: () => this.snackBar.open('Failed to update', 'Close', { duration: 2000 }),
    });
  }

  getStatusColor(status: string): string {
    const colors: Record<string, string> = {
      draft: 'bg-gray-100 text-gray-800', sent: 'bg-blue-100 text-blue-800',
      accepted: 'bg-green-100 text-green-800', rejected: 'bg-red-100 text-red-800',
      expired: 'bg-orange-100 text-orange-800',
    };
    return colors[status] || 'bg-gray-100 text-gray-800';
  }

  get pendingOffers(): Offer[] { return this.offers().filter(o => o.status === 'sent' || o.status === 'draft'); }
  get historyOffers(): Offer[] { return this.offers().filter(o => o.status === 'accepted' || o.status === 'rejected' || o.status === 'expired'); }
}
