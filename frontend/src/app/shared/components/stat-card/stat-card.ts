import { Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-stat-card',
  standalone: true,
  imports: [MatCardModule, MatIconModule, NgClass],
  templateUrl: './stat-card.html',
  styleUrl: './stat-card.scss',
})
export class StatCardComponent {
  @Input() title: string = '';
  @Input() value: string | number = 0;
  @Input() icon: string = 'bar_chart';
  @Input() color: string = '#4F46E5';
  @Input() trend: 'up' | 'down' | 'neutral' = 'neutral';
  @Input() trendValue: string = '';
}
