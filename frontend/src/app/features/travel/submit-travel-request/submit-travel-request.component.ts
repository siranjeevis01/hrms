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
import { MatStepperModule } from '@angular/material/stepper';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { TravelService } from '../travel.service';

@Component({
  selector: 'app-submit-travel-request',
  standalone: true,
  imports: [
    ReactiveFormsModule, MatCardModule, MatButtonModule, MatIconModule,
    MatInputModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule,
    MatDividerModule, MatStepperModule, MatProgressSpinnerModule,
  ],
  templateUrl: './submit-travel-request.component.html',
  styleUrl: './submit-travel-request.component.scss',
})
export class SubmitTravelRequestComponent {
  private fb = inject(FormBuilder);
  private travelService = inject(TravelService);
  private router = inject(Router);
  saving = signal(false);

  travelForm: FormGroup = this.fb.group({
    purpose: ['', Validators.required],
    destination: ['', Validators.required],
    startDate: ['', Validators.required],
    endDate: ['', Validators.required],
    transportType: ['', Validators.required],
    accommodationType: ['', Validators.required],
    estimatedCost: [0, [Validators.required, Validators.min(0)]],
    notes: [''],
  });

  itineraryForm: FormGroup = this.fb.group({
    days: this.fb.array([]),
  });

  transportTypes = [
    { value: 'flight', label: 'Flight' }, { value: 'train', label: 'Train' },
    { value: 'bus', label: 'Bus' }, { value: 'car_rental', label: 'Car Rental' },
    { value: 'personal_vehicle', label: 'Personal Vehicle' }, { value: 'taxi', label: 'Taxi' },
  ];

  accommodationTypes = [
    { value: 'hotel', label: 'Hotel' }, { value: 'guest_house', label: 'Guest House' },
    { value: 'airbnb', label: 'Airbnb' }, { value: 'none', label: 'None' },
  ];

  get itineraryDays(): FormArray { return this.itineraryForm.get('days') as FormArray; }

  addDay(): void {
    this.itineraryDays.push(this.fb.group({
      date: [''], activities: [''], accommodation: [''], transport: [''], estimatedCost: [0],
    }));
  }

  removeDay(index: number): void { this.itineraryDays.removeAt(index); }

  onSubmit(): void {
    if (this.travelForm.invalid) { this.travelForm.markAllAsTouched(); return; }
    this.saving.set(true);
    this.travelService.submitRequest({
      ...this.travelForm.value,
      itinerary: this.itineraryDays.value,
      expenses: [],
    }).subscribe({
      next: () => { this.saving.set(false); this.router.navigate(['/travel/my-requests']); },
      error: () => this.saving.set(false),
    });
  }

  cancel(): void { this.router.navigate(['/travel']); }
}
