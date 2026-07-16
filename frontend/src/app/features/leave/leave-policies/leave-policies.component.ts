import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LeaveService } from '../leave.service';
import { LeaveType } from '../leave.models';

@Component({
  selector: 'app-leave-policies',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatProgressSpinnerModule],
  templateUrl: './leave-policies.component.html',
  styleUrl: './leave-policies.component.scss',
})
export class LeavePoliciesComponent implements OnInit {
  policies = signal<LeaveType[]>([]);
  loading = signal(true);

  constructor(private leaveService: LeaveService) {}

  ngOnInit(): void {
    this.loadPolicies();
  }

  loadPolicies(): void {
    this.leaveService.getLeaveTypes().subscribe({
      next: (types) => {
        this.policies.set(types);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  getColorClass(color: string): string {
    const colorMap: Record<string, string> = {
      '#ef4444': 'border-l-red-500',
      '#3b82f6': 'border-l-blue-500',
      '#10b981': 'border-l-emerald-500',
      '#8b5cf6': 'border-l-purple-500',
      '#f59e0b': 'border-l-amber-500',
      '#ec4899': 'border-l-pink-500',
    };
    return colorMap[color] || 'border-l-gray-500';
  }
}
