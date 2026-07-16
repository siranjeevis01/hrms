export interface EmployeeListDto {
  id: string;
  employeeCode: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  department: string;
  designation: string;
  branch: string;
  status: EmployeeStatus;
  joinDate: Date;
  photoUrl: string;
}

export type EmployeeStatus = 'Active' | 'Inactive' | 'OnNotice' | 'Terminated';

export interface EmployeeDto extends EmployeeListDto {
  dateOfBirth: Date;
  gender: string;
  maritalStatus: string;
  bloodGroup: string;
  nationality: string;
  address: Address;
  emergencyContacts: EmergencyContact[];
  bankDetails: BankDetails;
  education: Education[];
  experience: Experience[];
  skills: string[];
  certifications: Certification[];
  salaryStructure: SalaryStructure;
}

export interface Address {
  street: string;
  city: string;
  state: string;
  zipCode: string;
  country: string;
}

export interface EmergencyContact {
  name: string;
  relationship: string;
  phoneNumber: string;
  email: string;
}

export interface BankDetails {
  bankName: string;
  accountNumber: string;
  routingNumber: string;
  accountType: string;
}

export interface Education {
  institution: string;
  degree: string;
  field: string;
  startDate: Date;
  endDate: Date;
  grade: string;
}

export interface Experience {
  company: string;
  designation: string;
  startDate: Date;
  endDate: Date;
  description: string;
}

export interface Certification {
  name: string;
  issuer: string;
  issueDate: Date;
  expiryDate: Date;
  credentialId: string;
}

export interface SalaryStructure {
  basicSalary: number;
  hra: number;
  transportAllowance: number;
  medicalAllowance: number;
  specialAllowance: number;
  pf: number;
  esi: number;
  professionalTax: number;
  incomeTax: number;
  totalDeductions: number;
  netSalary: number;
}

export interface CreateEmployeeCommand {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: Date;
  gender: string;
  maritalStatus: string;
  bloodGroup: string;
  nationality: string;
  department: string;
  designation: string;
  branch: string;
  employmentType: string;
  joinDate: Date;
  reportingManager: string;
}

export interface UpdateEmployeeCommand extends CreateEmployeeCommand {}

export interface PromoteEmployeeCommand {
  newDesignation: string;
  newSalary: number;
  effectiveDate: Date;
  reason: string;
}

export interface TransferEmployeeCommand {
  newDepartment: string;
  newBranch: string;
  effectiveDate: Date;
  reason: string;
}

export interface TerminateEmployeeCommand {
  terminationDate: Date;
  reason: string;
  noticePeriod: number;
  finalSettlement: number;
}

export interface EmployeeFilter {
  search: string;
  department: string;
  designation: string;
  branch: string;
  status: string;
  joinDateFrom: Date | null;
  joinDateTo: Date | null;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

export interface DocumentDto {
  id: string;
  name: string;
  type: string;
  uploadDate: Date;
  fileSize: number;
  url: string;
}

export interface HistoryDto {
  id: string;
  action: string;
  description: string;
  performedBy: string;
  performedOn: Date;
  details: string;
}
