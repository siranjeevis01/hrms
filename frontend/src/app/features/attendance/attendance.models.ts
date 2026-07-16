export type AttendanceStatus = 'Present' | 'Absent' | 'Late' | 'EarlyExit' | 'HalfDay' | 'WFH' | 'Holiday' | 'WeekOff';
export type WorkMode = 'Office' | 'WFH' | 'Hybrid';

export interface AttendanceRecord {
  id: string;
  employeeId: string;
  employeeName: string;
  department: string;
  date: string;
  checkIn: string | null;
  checkOut: string | null;
  totalHours: number;
  status: AttendanceStatus;
  workMode: WorkMode;
  location: string | null;
  remarks: string | null;
}

export interface AttendanceSummary {
  totalWorkingDays: number;
  present: number;
  absent: number;
  late: number;
  earlyExit: number;
  overtime: number;
  wfh: number;
  halfDay: number;
}

export interface AttendanceReport {
  employeeName: string;
  department: string;
  present: number;
  absent: number;
  late: number;
  hours: number;
}

export interface MarkAttendanceRequest {
  checkInTime: string;
  location?: string;
  latitude?: number;
  longitude?: number;
  workMode: WorkMode;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

export interface AttendanceFilters {
  page: number;
  pageSize: number;
  startDate?: string;
  endDate?: string;
  departmentId?: string;
  status?: AttendanceStatus;
  employeeId?: string;
}

export interface ReportFilters {
  startDate: string;
  endDate: string;
  departmentId?: string;
  employeeId?: string;
  reportType: 'Daily' | 'Weekly' | 'Monthly';
}
