export class AddTaskSubmissions {
  UserId!: number; // FK to user table
  subtaskid!: number;
  status!:StatusEnum;
  submittedFileName?: string | null;
  FileUpload?: File | null; // Will always be null
  FileUploadSubmission?: Uint8Array | null;
  SubTaskSubmitteddOn?: Date | null; // Date of when the Submission file was submitted
  Result!:string;

}
export enum StatusEnum {
  Completed = 'Complted',
  Pending = 'Pending',
  Failed_to_submit_within_deadline = 'Failed_to_submit_within_deadline'
}
