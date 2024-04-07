export class AddSubTask {

  Title!: string;
  Description!: string;
  Status!: number;
  TaskId!: number[];
  FileUploadTaskFileUpload?: File|null; // Assuming this is for file uploads in Angular
  FileUploadTaskPdf?: Uint8Array|string|null;
}
