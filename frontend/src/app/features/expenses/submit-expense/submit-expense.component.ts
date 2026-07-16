import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ExpensesService } from '../expenses.service';

@Component({
  selector: 'app-submit-expense',
  standalone: true,
  imports: [
    ReactiveFormsModule, MatCardModule, MatButtonModule, MatIconModule,
    MatInputModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, MatDividerModule, MatProgressSpinnerModule,
  ],
  templateUrl: './submit-expense.component.html',
  styleUrl: './submit-expense.component.scss',
})
export class SubmitExpenseComponent {
  private fb = inject(FormBuilder);
  private expensesService = inject(ExpensesService);
  private router = inject(Router);

  saving = signal(false);

  expenseForm: FormGroup = this.fb.group({
    title: ['', Validators.required],
    items: this.fb.array([this.createItem()]),
  });

  categories = [
    { value: 'travel', label: 'Travel' }, { value: 'meals', label: 'Meals' },
    { value: 'office_supplies', label: 'Office Supplies' }, { value: 'software', label: 'Software' },
    { value: 'training', label: 'Training' }, { value: 'transport', label: 'Transport' },
    { value: 'accommodation', label: 'Accommodation' }, { value: 'communication', label: 'Communication' },
    { value: 'other', label: 'Other' },
  ];

  get items(): FormArray { return this.expenseForm.get('items') as FormArray; }

  createItem(): FormGroup {
    return this.fb.group({
      category: ['', Validators.required], description: ['', Validators.required],
      amount: [0, [Validators.required, Validators.min(0.01)]],
      date: ['', Validators.required], taxAmount: [0], notes: [''],
    });
  }

  addItem(): void { this.items.push(this.createItem()); }
  removeItem(index: number): void { if (this.items.length > 1) this.items.removeAt(index); }

  get totalAmount(): number {
    return this.items.controls.reduce((sum, item) => sum + (item.get('amount')?.value || 0), 0);
  }

  onSubmit(): void {
    if (this.expenseForm.invalid) { this.expenseForm.markAllAsTouched(); return; }
    this.saving.set(true);
    this.expensesService.submitExpense({
      title: this.expenseForm.value.title,
      items: this.expenseForm.value.items.map((item: any) => ({
        ...item, receiptUrl: null, receiptName: null,
      })),
    }).subscribe({
      next: () => { this.saving.set(false); this.router.navigate(['/expenses/my-expenses']); },
      error: () => this.saving.set(false),
    });
  }

  cancel(): void { this.router.navigate(['/expenses']); }
}
