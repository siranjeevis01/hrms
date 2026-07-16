import { Component } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-page-loader',
  standalone: true,
  imports: [MatProgressSpinnerModule],
  templateUrl: './page-loader.html',
  styleUrl: './page-loader.scss',
})
export class PageLoaderComponent {}
