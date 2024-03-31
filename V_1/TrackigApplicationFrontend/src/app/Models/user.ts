export enum Role {
  // Define your role enums here
  Employee,
  Mentor,
  Admin
}
export class User {
  name!: string;
  role!: Role;
  domain!: string;
  jobTitle!: string;
  location!: string;
  phone!: string;
  isCr!: boolean;
  gender!: string;
  doj!: Date;
  capgeminiEmailId!: string;
  grade!: string;
  totalAverageRatingStatus: number = 0;
  personalEmailId?: string;
  earlierMentorName?: string;
  finalMentorName?: string;
  attendanceCount: number = 0;
  batches:null = null;//Later ocnvert it into a Batch Array
}
