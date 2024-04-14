export class GetBatches {
  batchId!: number; // PK
  batchName!: string; // System generated using the Date_of_creation+MentorName+Domain
  //mentor!: User; // Navigation property for the mentor
  mentorId!: number; // FK to UserID
  //employees!: User[] | null; // Navigation property for the employees
  domain!: string;
  description!: string;
showDetails: any=false;
employee_info_Excel!:Uint8Array;
  //attendanceExcel?: ArrayBuffer | null; // Used for File upload
  //employeeInfoExcel?: ArrayBuffer | null;
  //userTasks!: UserTask[];
}
