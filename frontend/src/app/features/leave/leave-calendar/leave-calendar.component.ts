import { Component, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LeaveService } from '../leave.service';
import { LeaveCalendarEntry } from '../leave.models';

interface CalendarDay {
  date: Date;
  entries: LeaveCalendarEntry[];
  isCurrentMonth: boolean;
  isToday: boolean;
}

@Component({
  selector: 'app-leave-calendar',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './leave-calendar.component.html',
  styleUrl: './leave-calendar.component.scss',
})
export class LeaveCalendarComponent implements OnInit {
  currentMonth = signal(new Date());
  calendarDays = signal<CalendarDay[]>([]);
  allEntries = signal<LeaveCalendarEntry[]>([]);
  selectedDayEntries = signal<LeaveCalendarEntry[]>([]);
  selectedDayDate = signal<string>('');
  filterDepartment = signal('');
  loading = signal(true);

  departments = ['Engineering', 'Marketing', 'Sales', 'HR', 'Finance'];
  dayLabels = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];

  monthYear = computed(() => {
    return this.currentMonth().toLocaleDateString('en-US', { month: 'long', year: 'numeric' });
  });

  constructor(private leaveService: LeaveService) {}

  ngOnInit(): void {
    this.loadCalendar();
  }

  loadCalendar(): void {
    this.loading.set(true);
    const month = this.currentMonth();
    const startDate = new Date(month.getFullYear(), month.getMonth(), 1);
    const endDate = new Date(month.getFullYear(), month.getMonth() + 1, 0);

    this.leaveService
      .getLeaveCalendar(
        startDate.toISOString().split('T')[0],
        endDate.toISOString().split('T')[0],
      )
      .subscribe({
        next: (entries) => {
          this.allEntries.set(entries);
          this.buildCalendar(entries);
          this.loading.set(false);
        },
        error: () => {
          this.buildCalendar([]);
          this.loading.set(false);
        },
      });
  }

  buildCalendar(entries: LeaveCalendarEntry[]): void {
    const month = this.currentMonth();
    const year = month.getFullYear();
    const m = month.getMonth();
    const firstDay = new Date(year, m, 1);
    const lastDay = new Date(year, m + 1, 0);
    const startOffset = (firstDay.getDay() + 6) % 7;
    const today = new Date();
    const days: CalendarDay[] = [];

    for (let i = startOffset - 1; i >= 0; i--) {
      const d = new Date(year, m, -i);
      days.push({ date: d, entries: [], isCurrentMonth: false, isToday: false });
    }

    for (let d = 1; d <= lastDay.getDate(); d++) {
      const date = new Date(year, m, d);
      const dateStr = date.toISOString().split('T')[0];
      const dayEntries = entries.filter((e) => {
        const start = e.startDate;
        const end = e.endDate;
        return dateStr >= start && dateStr <= end;
      });
      days.push({
        date,
        entries: dayEntries,
        isCurrentMonth: true,
        isToday: date.toDateString() === today.toDateString(),
      });
    }

    const remaining = 42 - days.length;
    for (let i = 1; i <= remaining; i++) {
      const d = new Date(year, m + 1, i);
      days.push({ date: d, entries: [], isCurrentMonth: false, isToday: false });
    }

    this.calendarDays.set(days);
  }

  prevMonth(): void {
    const curr = this.currentMonth();
    this.currentMonth.set(new Date(curr.getFullYear(), curr.getMonth() - 1, 1));
    this.loadCalendar();
  }

  nextMonth(): void {
    const curr = this.currentMonth();
    this.currentMonth.set(new Date(curr.getFullYear(), curr.getMonth() + 1, 1));
    this.loadCalendar();
  }

  selectDay(day: CalendarDay): void {
    if (day.entries.length > 0) {
      this.selectedDayEntries.set(day.entries);
      this.selectedDayDate.set(
        day.date.toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' }),
      );
    }
  }

  getLeaveTypeColor(type: string): string {
    const colors: Record<string, string> = {
      'Sick Leave': 'bg-red-100 text-red-700',
      'Casual Leave': 'bg-blue-100 text-blue-700',
      'Annual Leave': 'bg-emerald-100 text-emerald-700',
      'Maternity Leave': 'bg-purple-100 text-purple-700',
      'Paternity Leave': 'bg-indigo-100 text-indigo-700',
    };
    return colors[type] || 'bg-gray-100 text-gray-700';
  }

  getInitials(name: string): string {
    if (!name) return '';
    return name.split(' ').map(n => n[0]).join('');
  }
}
