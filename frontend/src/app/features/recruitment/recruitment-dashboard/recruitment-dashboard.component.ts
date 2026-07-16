import { Component, OnInit, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { MatRippleModule } from '@angular/material/core';
import { DatePipe, TitleCasePipe } from '@angular/common';
import { RecruitmentService } from '../recruitment.service';
import { RecruitmentDashboardStats } from '../recruitment.models';

@Component({
  selector: 'app-recruitment-dashboard',
  standalone: true,
  imports: [
    MatCardModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule,
    MatListModule, MatRippleModule, DatePipe, TitleCasePipe,
  ],
  templateUrl: './recruitment-dashboard.component.html',
  styleUrl: './recruitment-dashboard.component.scss',
})
export class RecruitmentDashboardComponent implements OnInit {
  private recruitmentService = inject(RecruitmentService);
  private router = inject(Router);

  stats = signal<RecruitmentDashboardStats | null>(null);
  loading = signal(true);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard(): void {
    this.loading.set(true);
    this.error.set(null);
    this.recruitmentService.getDashboardStats().subscribe({
      next: (data) => {
        this.stats.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Failed to load recruitment dashboard');
        this.loading.set(false);
      },
    });
  }

  navigateTo(path: string): void {
    this.router.navigate(['/recruitment', path]);
  }

  getFunnelPercentage(count: number, maxCount: number): number {
    return maxCount > 0 ? (count / maxCount) * 100 : 0;
  }

  getFunnelColor(index: number): string {
    const colors = ['#3b82f6', '#8b5cf6', '#f59e0b', '#10b981', '#059669'];
    return colors[index % colors.length];
  }

  getInterviewTypeIcon(type: string): string {
    const icons: Record<string, string> = {
      phone: 'phone', video: 'videocam', in_person: 'person',
      technical: 'code', panel: 'groups',
    };
    return icons[type] || 'event';
  }
}
