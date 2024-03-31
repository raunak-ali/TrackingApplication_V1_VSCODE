export class Batch {
  MentorId!: number;
  Domain!: string;
  Description!: string;
  Employee_info_Excel!: Uint8Array|null;
  Employee_info_Excel_File!:File|null;
}
