import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { AdminService } from '../admin.service';
import { FeatureFlag } from '../admin.models';

@Component({
  selector: 'app-feature-flags',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatSlideToggleModule, MatChipsModule],
  templateUrl: './feature-flags.component.html',
  styleUrl: './feature-flags.component.scss',
})
export class FeatureFlagsComponent implements OnInit {
  private adminService = inject(AdminService);
  flags = signal<FeatureFlag[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    this.adminService.getFeatureFlags().subscribe({
      next: (f) => { this.flags.set(f); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  toggleFlag(flag: FeatureFlag): void {
    this.adminService.toggleFeatureFlag(flag.id, !flag.isEnabled).subscribe({
      next: () => this.flags.update((fs) => fs.map((f) => f.id === flag.id ? { ...f, isEnabled: !f.isEnabled } : f)),
    });
  }
}
