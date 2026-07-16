export type CourseDifficulty = 'beginner' | 'intermediate' | 'advanced';
export type CourseStatus = 'draft' | 'published' | 'archived';
export type EnrollmentStatus = 'enrolled' | 'in_progress' | 'completed' | 'bookmarked';
export type LessonType = 'video' | 'text' | 'quiz' | 'assignment';
export type AssessmentStatus = 'available' | 'in_progress' | 'completed';

export interface Course {
  id: string;
  title: string;
  description: string;
  category: string;
  difficulty: CourseDifficulty;
  duration: number;
  thumbnailUrl?: string;
  instructor: string;
  rating: number;
  enrollmentCount: number;
  status: CourseStatus;
  modules: CourseModule[];
  createdAt: Date;
  updatedAt: Date;
}

export interface CourseModule {
  id: string;
  title: string;
  order: number;
  lessons: Lesson[];
}

export interface Lesson {
  id: string;
  title: string;
  type: LessonType;
  content?: string;
  videoUrl?: string;
  duration: number;
  order: number;
}

export interface Enrollment {
  id: string;
  courseId: string;
  course?: Course;
  userId: string;
  status: EnrollmentStatus;
  progress: number;
  completedLessons: string[];
  enrolledAt: Date;
  completedAt?: Date;
  lastAccessedAt?: Date;
}

export interface Assessment {
  id: string;
  courseId: string;
  course?: Course;
  title: string;
  questions: AssessmentQuestion[];
  passingScore: number;
  timeLimit: number;
  status: AssessmentStatus;
  lastScore?: number;
  attempts: number;
}

export interface AssessmentQuestion {
  id: string;
  question: string;
  options: string[];
  correctAnswer: number;
  explanation?: string;
}

export interface Certificate {
  id: string;
  courseId: string;
  course?: Course;
  userId: string;
  userName: string;
  issuedDate: Date;
  certificateUrl: string;
}

export interface LearningPath {
  id: string;
  title: string;
  description: string;
  courses: { courseId: string; course?: Course; order: number }[];
  totalDuration: number;
  enrolledCount: number;
  enrolled?: boolean;
  progress?: number;
}

export interface TrainingSchedule {
  id: string;
  title: string;
  courseId?: string;
  instructor: string;
  startDate: Date;
  endDate: Date;
  location?: string;
  meetingUrl?: string;
  maxParticipants: number;
  enrolledCount: number;
  type: 'online' | 'in_person' | 'hybrid';
}

export interface TrainingDashboardStats {
  enrolledCourses: number;
  completedCourses: number;
  inProgressCourses: number;
  certificatesEarned: number;
  courseProgress: Enrollment[];
  upcomingSchedules: TrainingSchedule[];
  recommendedCourses: Course[];
}
