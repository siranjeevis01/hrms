import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DatePipe, KeyValuePipe, TitleCasePipe } from '@angular/common';
import { TrainingService } from '../training.service';
import { TrainingSchedule } from '../training.models';

@Component({
  selector: 'app-training-schedule',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatTabsModule, MatChipsModule, MatProgressSpinnerModule, DatePipe, KeyValuePipe, TitleCasePipe],
  templateUrl: './training-schedule.component.html',
  styleUrl: './training-schedule.component.scss',
})
export class TrainingScheduleComponent implements OnInit {
  private trainingService = inject(TrainingService);

  schedules = signal<TrainingSchedule[]>([]);
  loading = signal(true);
  viewMode = 'calendar';

  ngOnInit(): void { this.loadSchedules(); }

  loadSchedules(): void {
    this.loading.set(true);
    this.trainingService.getTrainingSchedules().subscribe({
      next: (schedules) => { this.schedules.set(schedules); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  getTypeIcon(type: string): string {
    const icons: Record<string, string> = { online: 'videocam', in_person: 'person', hybrid: 'hybrid' };
    return icons[type] || 'event';
  }

  getTypeColor(type: string): string {
    const c: Record<string, string> = { online: 'bg-blue-100 text-blue-800', in_person: 'bg-green-100 text-green-800', hybrid: 'bg-purple-100 text-purple-800' };
    return c[type] || 'bg-gray-100 text-gray-800';
  }

  get groupedByDate(): Map<string, TrainingSchedule[]> {
    const map = new Map<string, TrainingSchedule[]>();
    for (const s of this.schedules()) {
      const key = new Date(s.startDate).toDateString();
      if (!map.has(key)) map.set(key, []);
      map.get(key)!.push(s);
    }
    return map;
  }
}
