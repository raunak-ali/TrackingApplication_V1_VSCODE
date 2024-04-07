import { GetTask } from "./get-task";

export class SubTask {
  subTaskId!: number;
  title!: string;
  description!: string;
  status!: number;
  taskId!: number;
  fileUploadTaskPdf?: Uint8Array;
  creationDate!: Date;
  fileUploadSubmission?: Uint8Array;
  subTaskCompletedOn?: Date;
  userTask?: GetTask|null; // Assuming UserTask is also an Angular model
  fileName!:string;
}

