export type JobStatus = 'draft' | 'published' | 'on_hold' | 'closed';
export type EmploymentType = 'full_time' | 'part_time' | 'contract' | 'internship' | 'freelance';
export type ApplicationStatus = 'applied' | 'screening' | 'shortlisted' | 'interview' | 'offered' | 'hired' | 'rejected';
export type InterviewType = 'phone' | 'video' | 'in_person' | 'technical' | 'panel';
export type InterviewStatus = 'scheduled' | 'completed' | 'cancelled' | 'no_show';
export type OfferStatus = 'draft' | 'sent' | 'accepted' | 'rejected' | 'expired';
export type CandidateSource = 'job_board' | 'referral' | 'company_website' | 'linkedin' | 'agency' | 'campus' | 'other';
export type OnboardingStatus = 'not_started' | 'in_progress' | 'completed';

export interface Job {
  id: string;
  title: string;
  description: string;
  department: string;
  designation: string;
  employmentType: EmploymentType;
  minExperience: number;
  maxExperience: number;
  minSalary: number;
  maxSalary: number;
  skills: string[];
  requirements: string[];
  responsibilities: string[];
  benefits: string[];
  status: JobStatus;
  applicationCount: number;
  postedDate: Date;
  closingDate?: Date;
  createdAt: Date;
  updatedAt: Date;
}

export interface Candidate {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  source: CandidateSource;
  resumeUrl?: string;
  coverLetter?: string;
  status: ApplicationStatus;
  appliedDate: Date;
  currentJobTitle?: string;
  experience?: number;
  skills: string[];
  notes?: string;
  createdAt: Date;
  updatedAt: Date;
}

export interface Application {
  id: string;
  candidateId: string;
  candidate?: Candidate;
  jobId: string;
  job?: Job;
  status: ApplicationStatus;
  appliedDate: Date;
  screeningScore?: number;
  notes?: string;
  createdAt: Date;
  updatedAt: Date;
}

export interface Interview {
  id: string;
  candidateId: string;
  candidate?: Candidate;
  jobId: string;
  job?: Job;
  interviewers: string[];
  scheduledDate: Date;
  duration: number;
  type: InterviewType;
  status: InterviewStatus;
  location?: string;
  meetingUrl?: string;
  feedback?: string;
  rating?: number;
  notes?: string;
  createdAt: Date;
  updatedAt: Date;
}

export interface Offer {
  id: string;
  candidateId: string;
  candidate?: Candidate;
  jobId: string;
  job?: Job;
  position: string;
  department: string;
  ctc: number;
  basicSalary: number;
  joiningDate: Date;
  expiryDate: Date;
  status: OfferStatus;
  benefits?: string[];
  notes?: string;
  createdAt: Date;
  updatedAt: Date;
}

export interface OnboardingTask {
  id: string;
  title: string;
  description?: string;
  completed: boolean;
  assignedTo?: string;
  dueDate?: Date;
}

export interface OnboardingChecklist {
  id: string;
  employeeId: string;
  employeeName: string;
  position: string;
  joiningDate: Date;
  status: OnboardingStatus;
  tasks: OnboardingTask[];
  progress: number;
  createdAt: Date;
  updatedAt: Date;
}

export interface RecruitmentDashboardStats {
  openPositions: number;
  applicationsThisMonth: number;
  interviewsScheduled: number;
  offersPending: number;
  hiredThisMonth: number;
  pipelineFunnel: { stage: string; count: number }[];
  recentApplications: Application[];
  upcomingInterviews: Interview[];
}

export interface JobFilters {
  search?: string;
  department?: string;
  status?: JobStatus;
  employmentType?: EmploymentType;
}

export interface CandidateFilters {
  search?: string;
  status?: ApplicationStatus;
  source?: CandidateSource;
}
