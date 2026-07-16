export type GoalStatus = 'not_started' | 'in_progress' | 'completed' | 'cancelled';
export type GoalCategory = 'individual' | 'team' | 'department';
export type GoalPriority = 'low' | 'medium' | 'high' | 'critical';
export type ReviewStatus = 'draft' | 'pending' | 'in_progress' | 'completed';
export type AppraisalStatus = 'pending' | 'in_progress' | 'completed' | 'appealed';
export type FeedbackStatus = 'pending' | 'submitted';
export type KPIStatus = 'active' | 'inactive';

export interface Goal {
  id: string;
  title: string;
  description: string;
  category: GoalCategory;
  startDate: Date;
  endDate: Date;
  priority: GoalPriority;
  weight: number;
  progress: number;
  status: GoalStatus;
  keyResults: KeyResult[];
  assignedTo: string[];
  createdBy: string;
  department?: string;
  createdAt: Date;
  updatedAt: Date;
}

export interface KeyResult {
  id: string;
  title: string;
  target: number;
  current: number;
  unit: string;
  progress: number;
}

export interface OKRCycle {
  id: string;
  name: string;
  startDate: Date;
  endDate: Date;
  status: 'active' | 'completed' | 'upcoming';
  objectives: OKRObjective[];
}

export interface OKRObjective {
  id: string;
  title: string;
  progress: number;
  keyResults: KeyResult[];
}

export interface KPI {
  id: string;
  name: string;
  metric: string;
  target: number;
  current: number;
  score: number;
  status: KPIStatus;
  department?: string;
  employeeId?: string;
  createdAt: Date;
  updatedAt: Date;
}

export interface ReviewCycle {
  id: string;
  name: string;
  startDate: Date;
  endDate: Date;
  status: ReviewStatus;
  type: 'self' | 'manager' | 'peer' | '360';
}

export interface Review {
  id: string;
  reviewCycleId: string;
  reviewCycle?: ReviewCycle;
  employeeId: string;
  reviewerId: string;
  status: ReviewStatus;
  ratings: ReviewRating[];
  selfAssessment?: string;
  managerAssessment?: string;
  overallComments?: string;
  overallRating?: number;
  createdAt: Date;
  updatedAt: Date;
}

export interface ReviewRating {
  criterion: string;
  rating: number;
  comment?: string;
}

export interface FeedbackRequest {
  id: string;
  requesterId: string;
  targetId: string;
  targetName?: string;
  status: FeedbackStatus;
  questions: FeedbackQuestion[];
  submittedAt?: Date;
  createdAt: Date;
}

export interface FeedbackQuestion {
  question: string;
  answer: string;
}

export interface Appraisal {
  id: string;
  employeeId: string;
  employeeName?: string;
  cycleName: string;
  status: AppraisalStatus;
  currentRating?: number;
  recommendedRating?: number;
  hikePercentage?: number;
  promotionRecommended: boolean;
  comments?: string;
  createdAt: Date;
  updatedAt: Date;
}

export interface CalibrationSession {
  id: string;
  name: string;
  date: Date;
  participants: string[];
  status: 'scheduled' | 'in_progress' | 'completed';
  ratings: CalibrationRating[];
}

export interface CalibrationRating {
  employeeId: string;
  employeeName: string;
  originalRating: number;
  calibratedRating: number;
  discussion: string;
}

export interface PerformanceDashboardStats {
  activeGoals: number;
  okrCompletionRate: number;
  pendingReviews: number;
  appraisalPending: number;
  goalProgressByDepartment: { department: string; progress: number }[];
  ratingDistribution: { rating: number; count: number }[];
}
