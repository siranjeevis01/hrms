export type TravelStatus = 'draft' | 'submitted' | 'approved' | 'rejected' | 'completed' | 'cancelled';
export type TransportType = 'flight' | 'train' | 'bus' | 'car_rental' | 'personal_vehicle' | 'taxi' | 'other';
export type AccommodationType = 'hotel' | 'guest_house' | 'airbnb' | 'none';

export interface TravelRequest {
  id: string;
  employeeId: string;
  employeeName: string;
  purpose: string;
  destination: string;
  startDate: string;
  endDate: string;
  status: TravelStatus;
  transportType: TransportType;
  accommodationType: AccommodationType;
  estimatedCost: number;
  actualCost: number | null;
  itinerary: ItineraryDay[];
  expenses: TravelExpense[];
  approvedBy: string | null;
  approvedDate: string | null;
  rejectionReason: string | null;
  notes: string;
  createdAt: string;
  updatedAt: string;
}

export interface ItineraryDay {
  date: string;
  activities: string;
  accommodation: string;
  transport: string;
  estimatedCost: number;
}

export interface TravelExpense {
  id: string;
  description: string;
  amount: number;
  category: string;
  date: string;
  receiptUrl: string | null;
}

export interface TravelDashboardStats {
  activeTrips: number;
  pendingApprovals: number;
  monthlyCost: number;
  upcomingTrips: TravelRequest[];
  recentTrips: TravelRequest[];
}

export interface SubmitTravelRequest {
  purpose: string;
  destination: string;
  startDate: string;
  endDate: string;
  transportType: TransportType;
  accommodationType: AccommodationType;
  estimatedCost: number;
  itinerary: ItineraryDay[];
  expenses: Omit<TravelExpense, 'id'>[];
  notes: string;
}
