export type ReportStatus = 'pending' | 'processing' | 'completed' | 'failed';
export type ReportFormat = 'pdf' | 'excel' | 'csv';
export type ReportCategory = 'hr' | 'payroll' | 'attendance' | 'leave' | 'recruitment' | 'expense' | 'travel' | 'project';

export interface ReportTemplate {
  id: string;
  name: string;
  description: string;
  category: ReportCategory;
  icon: string;
  parameters: ReportParameter[];
  createdAt: string;
}

export interface ReportParameter {
  key: string;
  label: string;
  type: 'text' | 'date' | 'dateRange' | 'select' | 'multiSelect' | 'checkbox';
  options?: { label: string; value: string }[];
  required: boolean;
  defaultValue?: string | string[];
}

export interface ReportInstance {
  id: string;
  templateId: string;
  templateName: string;
  category: ReportCategory;
  status: ReportStatus;
  format: ReportFormat;
  parameters: Record<string, any>;
  fileUrl: string | null;
  fileSize: number | null;
  generatedAt: string | null;
  generatedBy: string;
  generatedByName: string;
  error?: string;
}

export interface ScheduledReport {
  id: string;
  templateId: string;
  templateName: string;
  category: ReportCategory;
  frequency: 'daily' | 'weekly' | 'monthly' | 'quarterly';
  format: ReportFormat;
  parameters: Record<string, any>;
  recipients: string[];
  nextRunAt: string;
  lastRunAt: string | null;
  isActive: boolean;
  createdAt: string;
}

export interface GenerateReportRequest {
  templateId: string;
  format: ReportFormat;
  parameters: Record<string, any>;
}

export interface ScheduleReportRequest {
  templateId: string;
  frequency: string;
  format: ReportFormat;
  parameters: Record<string, any>;
  recipients: string[];
}
