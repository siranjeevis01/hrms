export interface DashboardStats {
  totalEmployees: number;
  activeEmployees: number;
  newHires: number;
  attritionRate: number;
  avgAttendance: number;
  pendingLeaves: number;
  openPositions: number;
  totalDepartments: number;
}

export interface Activity {
  id: string;
  type: string;
  description: string;
  user: string;
  timestamp: Date;
  icon: string;
}

export interface Event {
  id: string;
  title: string;
  date: Date;
  type: string;
  description: string;
}

export interface ChartData {
  labels: string[];
  datasets: ChartDataset[];
}

export interface ChartDataset {
  label: string;
  data: number[];
  backgroundColor?: string | string[];
  borderColor?: string | string[];
  borderWidth?: number;
  fill?: boolean;
  tension?: number;
}

export interface PayrollSummary {
  totalPayroll: number;
  averageSalary: number;
  highestSalary: number;
  departmentWise: DepartmentPayroll[];
  month: string;
}

export interface DepartmentPayroll {
  department: string;
  totalSalary: number;
  employeeCount: number;
}
