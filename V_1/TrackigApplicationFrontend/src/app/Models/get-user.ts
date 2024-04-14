import { Role } from "./user";

export class GetUser {
  userId!:number;

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
    total_Average_RatingStatus: number = 0;
    personalEmailId?: string;
    earlierMentorName?: string;
    finalMentorName?: string;
    attendanceCount: number = 0;
    batches:null = null;//Later ocnvert it into a Batch Array
selected: any;

}
