import { Component, OnInit, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DatePipe } from '@angular/common';
import { TrainingService } from '../training.service';
import { Certificate } from '../training.models';

@Component({
  selector: 'app-certificate-list',
  standalone: true,
  imports: [MatCardModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule, DatePipe],
  templateUrl: './certificate-list.component.html',
  styleUrl: './certificate-list.component.scss',
})
export class CertificateListComponent implements OnInit {
  private trainingService = inject(TrainingService);

  certificates = signal<Certificate[]>([]);
  loading = signal(true);

  ngOnInit(): void {
    this.trainingService.getCertificates().subscribe({
      next: (certs) => { this.certificates.set(certs); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  downloadCertificate(url: string): void {
    window.open(url, '_blank');
  }
}
