export class GetSubTask {
  subTaskId!: number;
    title!: string;
    description!: string;
    status!: number;
    taskId!: number;
    fileUploadTaskPdf?: Uint8Array;
    creationDate!: Date;
    fileUploadSubmission?: Uint8Array;
    subTaskCompletedOn?: Date;
}
