export type PayslipStatus = 'Generated' | 'Paid' | 'Pending' | 'OnHold';
export type ComponentType = 'Earning' | 'Deduction';
export type CalculationType = 'Fixed' | 'Percentage';

export interface PayrollSummary {
  totalEmployees: number;
  processedAmount: number;
  pendingAmount: number;
  avgSalary: number;
  totalDeductions: number;
  totalAllowances: number;
}

export interface Payslip {
  id: string;
  employeeId: string;
  employeeCode: string;
  employeeName: string;
  department: string;
  month: number;
  year: number;
  grossSalary: number;
  totalDeductions: number;
  netPay: number;
  status: PayslipStatus;
}

export interface PayslipDetail extends Payslip {
  earnings: PayslipLineItem[];
  deductions: PayslipLineItem[];
  taxBreakdown: TaxBreakdown;
}

export interface PayslipLineItem {
  name: string;
  amount: number;
}

export interface TaxBreakdown {
  taxableIncome: number;
  totalTax: number;
  cess: number;
  tds: number;
  sections: TaxSection[];
}

export interface TaxSection {
  section: string;
  amount: number;
}

export interface PayrollRunResult {
  processedCount: number;
  totalAmount: number;
  errors: PayrollError[];
}

export interface PayrollError {
  employeeId: string;
  employeeName: string;
  error: string;
}

export interface SalaryStructure {
  id: string;
  name: string;
  components: SalaryComponent[];
}

export interface SalaryComponent {
  id: string;
  name: string;
  type: ComponentType;
  calculationType: CalculationType;
  value: number;
}

export interface TaxCalculation {
  annualIncome: number;
  taxableIncome: number;
  oldRegimeTax: number;
  newRegimeTax: number;
  oldRegimeBreakdown: TaxSection[];
  newRegimeBreakdown: TaxSection[];
}

export interface PayrollReport {
  department: string;
  employeeCount: number;
  totalGross: number;
  totalDeductions: number;
  totalNet: number;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

export interface PayslipFilters {
  page: number;
  pageSize: number;
  month: number;
  year: number;
  departmentId?: string;
  employeeSearch?: string;
}

export interface ReportFilters {
  startDate: string;
  endDate: string;
  departmentId?: string;
  reportType: string;
}
