import { Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HelpdeskService } from '../helpdesk.service';
import { Faq } from '../helpdesk.models';

@Component({
  selector: 'app-faqs',
  standalone: true,
  imports: [FormsModule, MatExpansionModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatChipsModule, MatProgressSpinnerModule],
  templateUrl: './faqs.component.html',
  styleUrl: './faqs.component.scss',
})
export class FaqsComponent implements OnInit {
  private helpdeskService = inject(HelpdeskService);
  faqs = signal<Faq[]>([]);
  loading = signal(true);
  searchQuery = signal('');
  selectedCategory = signal('');
  categories = signal<string[]>([]);

  ngOnInit(): void { this.loadFaqs(); }

  loadFaqs(): void {
    this.loading.set(true);
    this.helpdeskService.getFaqs(this.searchQuery() || undefined, this.selectedCategory() || undefined).subscribe({
      next: (faqs) => { this.faqs.set(faqs); this.loading.set(false); this.categories.set([...new Set(faqs.map(f => f.category))]); },
      error: () => this.loading.set(false),
    });
  }

  markHelpful(id: string, event: Event): void {
    event.stopPropagation();
    this.helpdeskService.markFaqHelpful(id).subscribe({ next: () => this.loadFaqs() });
  }
}
