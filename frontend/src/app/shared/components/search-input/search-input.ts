import { Component, Input, Output, EventEmitter, OnInit, OnDestroy, inject } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { debounceTime, distinctUntilChanged } from 'rxjs';

@Component({
  selector: 'app-search-input',
  standalone: true,
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatIconModule],
  templateUrl: './search-input.html',
  styleUrl: './search-input.scss',
})
export class SearchInputComponent implements OnInit, OnDestroy {
  @Input() placeholder: string = 'Search...';
  @Input() debounceTime: number = 300;
  @Output() searchChange = new EventEmitter<string>();

  searchControl = new FormControl('');
  private destroyRef = inject(DestroyRef);

  ngOnInit(): void {
    this.searchControl.valueChanges
      .pipe(debounceTime(this.debounceTime), distinctUntilChanged(), takeUntilDestroyed(this.destroyRef))
      .subscribe((value) => {
        this.searchChange.emit(value ?? '');
      });
  }

  ngOnDestroy(): void {
    this.searchControl.reset();
  }
}
