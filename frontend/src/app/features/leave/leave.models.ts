export type LeaveStatus = 'Pending' | 'Approved' | 'Rejected' | 'Cancelled';

export interface LeaveRequest {
  id: string;
  employeeId: string;
  employeeName: string;
  leaveType: string;
  startDate: string;
  endDate: string;
  totalDays: number;
  reason: string;
  status: LeaveStatus;
  appliedAt: string;
  approvedBy: string | null;
  approvedAt: string | null;
  comments: string | null;
}

export interface LeaveBalance {
  leaveType: string;
  entitled: number;
  taken: number;
  pending: number;
  carried: number;
  balance: number;
}

export interface LeaveType {
  id: string;
  name: string;
  code: string;
  daysAllowed: number;
  isCarryForward: boolean;
  maxCarryDays: number;
  isEncashable: boolean;
  color: string;
}

export interface Holiday {
  id: string;
  name: string;
  date: string;
  type: string;
}

export interface LeaveCalendarEntry {
  employeeName: string;
  leaveType: string;
  startDate: string;
  endDate: string;
  status: LeaveStatus;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

export interface LeaveFilters {
  page: number;
  pageSize: number;
  status?: LeaveStatus;
  leaveType?: string;
  startDate?: string;
  endDate?: string;
  departmentId?: string;
}

export interface ApplyLeaveCommand {
  leaveType: string;
  startDate: string;
  endDate: string;
  reason: string;
}
