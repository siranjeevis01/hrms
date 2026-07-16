import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HelpdeskService } from '../helpdesk.service';
import { KnowledgeArticle } from '../helpdesk.models';

@Component({
  selector: 'app-knowledge-base',
  standalone: true,
  imports: [DatePipe, FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatInputModule, MatChipsModule, MatProgressSpinnerModule],
  templateUrl: './knowledge-base.component.html',
  styleUrl: './knowledge-base.component.scss',
})
export class KnowledgeBaseComponent implements OnInit {
  private helpdeskService = inject(HelpdeskService);
  articles = signal<KnowledgeArticle[]>([]);
  loading = signal(true);
  searchQuery = signal('');
  selectedCategory = signal('');
  selectedArticle = signal<KnowledgeArticle | null>(null);
  categories = signal<string[]>([]);

  ngOnInit(): void { this.loadArticles(); }

  loadArticles(): void {
    this.loading.set(true);
    this.helpdeskService.getKnowledgeArticles(this.searchQuery() || undefined, this.selectedCategory() || undefined).subscribe({
      next: (articles) => { this.articles.set(articles); this.loading.set(false); this.categories.set([...new Set(articles.map(a => a.category))]); },
      error: () => this.loading.set(false),
    });
  }

  selectArticle(article: KnowledgeArticle): void { this.selectedArticle.set(article); }
  closeArticle(): void { this.selectedArticle.set(null); }
}
