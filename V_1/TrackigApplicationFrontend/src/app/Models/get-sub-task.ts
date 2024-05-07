export class GetSubTask {
  subTaskId!: number;
    title!: string;
    description!: string;
    taskId!: number;
    fileUploadTaskPdf?: Uint8Array;
    creationDate!: Date;
    fileUploadSubmission?: Uint8Array;
    subTaskCompletedOn?: Date;
    TestCases!:string;
    isCodingProblem!:boolean;

}
