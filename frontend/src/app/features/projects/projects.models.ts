export type ProjectStatus = 'planning' | 'active' | 'on_hold' | 'completed' | 'cancelled';
export type TaskStatus = 'todo' | 'in_progress' | 'in_review' | 'done';
export type TaskPriority = 'low' | 'medium' | 'high' | 'critical';
export type SprintStatus = 'planned' | 'active' | 'completed';
export type StoryType = 'story' | 'task' | 'bug' | 'epic';

export interface Project {
  id: string;
  name: string;
  code: string;
  description: string;
  client: string;
  startDate: string;
  endDate: string;
  budget: number;
  spent: number;
  status: ProjectStatus;
  progress: number;
  manager: TeamMember;
  department: string;
  team: TeamMember[];
  taskCount: number;
  completedTaskCount: number;
  bugCount: number;
  createdAt: string;
  updatedAt: string;
}

export interface TeamMember {
  id: string;
  name: string;
  email: string;
  avatar: string;
  role: string;
}

export interface ProjectTask {
  id: string;
  projectId: string;
  title: string;
  description: string;
  type: StoryType;
  status: TaskStatus;
  priority: TaskPriority;
  assignee: TeamMember | null;
  reporter: TeamMember;
  storyPoints: number;
  sprintId: string | null;
  epicId: string | null;
  parentId: string | null;
  tags: string[];
  dueDate: string | null;
  estimatedHours: number;
  loggedHours: number;
  subtasks: ProjectTask[];
  comments: TaskComment[];
  attachments: TaskAttachment[];
  createdAt: string;
  updatedAt: string;
}

export interface TaskComment {
  id: string;
  author: TeamMember;
  content: string;
  createdAt: string;
}

export interface TaskAttachment {
  id: string;
  name: string;
  url: string;
  size: number;
  uploadedBy: TeamMember;
  uploadedAt: string;
}

export interface Sprint {
  id: string;
  projectId: string;
  name: string;
  goal: string;
  startDate: string;
  endDate: string;
  status: SprintStatus;
  totalStoryPoints: number;
  completedStoryPoints: number;
  taskCount: number;
  completedTaskCount: number;
  tasks: ProjectTask[];
  createdAt: string;
}

export interface BoardColumn {
  id: string;
  title: string;
  status: TaskStatus;
  tasks: ProjectTask[];
  color: string;
}

export interface Epic {
  id: string;
  projectId: string;
  title: string;
  description: string;
  status: TaskStatus;
  progress: number;
  storyPoints: number;
  completedStoryPoints: number;
  color: string;
  createdAt: string;
}

export interface BacklogItem {
  id: string;
  type: StoryType;
  title: string;
  priority: TaskPriority;
  storyPoints: number;
  assignee: TeamMember | null;
  sprintId: string | null;
  epicId: string | null;
}

export interface ProjectStats {
  totalTasks: number;
  completedTasks: number;
  totalBugs: number;
  openBugs: number;
  totalStoryPoints: number;
  completedStoryPoints: number;
  teamSize: number;
  daysRemaining: number;
}

export interface CreateProjectRequest {
  name: string;
  code: string;
  description: string;
  client: string;
  startDate: string;
  endDate: string;
  budget: number;
  managerId: string;
  departmentId: string;
  teamMemberIds: string[];
}

export interface CreateTaskRequest {
  projectId: string;
  title: string;
  description: string;
  type: StoryType;
  priority: TaskPriority;
  assigneeId?: string;
  storyPoints: number;
  sprintId?: string;
  epicId?: string;
  dueDate?: string;
  tags: string[];
}

export interface UpdateTaskStatusRequest {
  taskId: string;
  status: TaskStatus;
  position: number;
}
