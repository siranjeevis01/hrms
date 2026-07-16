import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ReportsService } from '../reports.service';
import { ReportTemplate, ReportInstance, ReportFormat } from '../reports.models';

@Component({
  selector: 'app-report-generate',
  standalone: true,
  imports: [
    ReactiveFormsModule, FormsModule, MatCardModule, MatButtonModule, MatIconModule,
    MatInputModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, MatProgressSpinnerModule, DatePipe,
  ],
  templateUrl: './report-generate.component.html',
  styleUrl: './report-generate.component.scss',
})
export class ReportGenerateComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private reportsService = inject(ReportsService);

  template = signal<ReportTemplate | null>(null);
  generatedReport = signal<ReportInstance | null>(null);
  loading = signal(true);
  generating = signal(false);
  format: ReportFormat = 'pdf';

  paramForm: FormGroup = this.fb.group({});

  ngOnInit(): void {
    const templateId = this.route.snapshot.paramMap.get('templateId') || '';
    this.reportsService.getTemplate(templateId).subscribe({
      next: (template) => {
        this.template.set(template);
        template.parameters.forEach((param) => {
          this.paramForm.addControl(param.key, this.fb.control(param.defaultValue || '', param.required ? Validators.required : []));
        });
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  generate(): void {
    if (this.paramForm.invalid) { this.paramForm.markAllAsTouched(); return; }
    this.generating.set(true);
    this.reportsService.generateReport({
      templateId: this.template()!.id,
      format: this.format,
      parameters: this.paramForm.value,
    }).subscribe({
      next: (report) => { this.generatedReport.set(report); this.generating.set(false); },
      error: () => this.generating.set(false),
    });
  }

  download(): void {
    if (!this.generatedReport()) return;
    this.reportsService.downloadReport(this.generatedReport()!.id).subscribe({
      next: (blob) => {
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `${this.template()!.name}.${this.format}`;
        a.click();
        URL.revokeObjectURL(url);
      },
    });
  }

  goBack(): void { this.router.navigate(['/reports']); }
}
