import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HelpdeskService } from '../helpdesk.service';
import { TicketCategory, TicketPriority } from '../helpdesk.models';

@Component({
  selector: 'app-create-ticket',
  standalone: true,
  imports: [ReactiveFormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatSelectModule, MatChipsModule, MatProgressSpinnerModule],
  templateUrl: './create-ticket.component.html',
  styleUrl: './create-ticket.component.scss',
})
export class CreateTicketComponent {
  private fb = inject(FormBuilder);
  private helpdeskService = inject(HelpdeskService);
  private router = inject(Router);
  saving = signal(false);
  categories = signal<TicketCategory[]>([]);

  ticketForm: FormGroup = this.fb.group({
    subject: ['', [Validators.required, Validators.minLength(5)]],
    description: ['', [Validators.required, Validators.minLength(20)]],
    categoryId: ['', Validators.required],
    priority: ['medium', Validators.required],
  });

  priorities: { value: TicketPriority; label: string; color: string }[] = [
    { value: 'low', label: 'Low', color: 'border-gray-300' },
    { value: 'medium', label: 'Medium', color: 'border-blue-400' },
    { value: 'high', label: 'High', color: 'border-orange-400' },
    { value: 'urgent', label: 'Urgent', color: 'border-red-400' },
  ];

  constructor() {
    this.helpdeskService.getCategories().subscribe({ next: (cats) => this.categories.set(cats) });
  }

  onSubmit(): void {
    if (this.ticketForm.invalid) { this.ticketForm.markAllAsTouched(); return; }
    this.saving.set(true);
    this.helpdeskService.createTicket({ ...this.ticketForm.value, attachments: [] }).subscribe({
      next: () => { this.saving.set(false); this.router.navigate(['/helpdesk/my-tickets']); },
      error: () => this.saving.set(false),
    });
  }

  cancel(): void { this.router.navigate(['/helpdesk']); }
}
