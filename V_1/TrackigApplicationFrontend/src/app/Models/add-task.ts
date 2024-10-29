export class AddTask {

    userTaskID!: number;
    TaskName!: string;
    Description!: string;
    Priority!: priority;
    DeadLine!: Date;
    Status!: number;
    AssignedBy!: number;
    AssignedTo!: number;
    BatchId!: number;
    Comments!: Comment;
    ModuleId!:number;


}
export enum priority{

  low
  ,medium,
  high
}


