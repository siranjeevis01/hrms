export type ExpenseStatus = 'draft' | 'submitted' | 'approved' | 'rejected' | 'reimbursed';
export type ExpenseCategory = 'travel' | 'meals' | 'office_supplies' | 'software' | 'training' | 'utilities' | 'transport' | 'accommodation' | 'communication' | 'other';

export interface ExpenseClaim {
  id: string;
  title: string;
  employeeId: string;
  employeeName: string;
  status: ExpenseStatus;
  totalAmount: number;
  currency: string;
  items: ExpenseItem[];
  submittedDate: string | null;
  approvedDate: string | null;
  approvedBy: string | null;
  rejectionReason: string | null;
  reimbursedDate: string | null;
  createdAt: string;
  updatedAt: string;
}

export interface ExpenseItem {
  id: string;
  category: ExpenseCategory;
  description: string;
  amount: number;
  date: string;
  receiptUrl: string | null;
  receiptName: string | null;
  taxAmount: number;
  notes: string;
}

export interface ExpensePolicy {
  id: string;
  name: string;
  category: ExpenseCategory;
  maxAmount: number;
  requiresReceipt: boolean;
  description: string;
  isActive: boolean;
}

export interface ExpenseDashboardStats {
  totalThisMonth: number;
  totalPending: number;
  totalApproved: number;
  totalReimbursed: number;
  pendingCount: number;
  approvedCount: number;
  categoryBreakdown: CategoryBreakdown[];
}

export interface CategoryBreakdown {
  category: ExpenseCategory;
  amount: number;
  count: number;
  percentage: number;
}

export interface SubmitExpenseRequest {
  title: string;
  items: Omit<ExpenseItem, 'id'>[];
}

export interface ApproveExpenseRequest {
  expenseId: string;
  approved: boolean;
  comments: string;
}
