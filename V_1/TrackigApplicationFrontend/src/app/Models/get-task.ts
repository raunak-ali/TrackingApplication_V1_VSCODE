import { Batch } from "./batch";
import { User } from "./user";

export class GetTask {
    userTaskID!: number;
    taskName!: string;
    Description!: string;
    Priority!: number;
    deadLine!: Date;
    status!: number;
    assignedBy!: number;
    assignedTo!: number;
    batchId!: number;
    comments!: string;
    createdAt!: Date | null;
    assignedByUser!: User | null; // Nav Property
   // batches!: Batch | null; // Nav Property
    assignedToUser!: User | null; // Nav Property
    subTasks!:  null; // Nav Property
    feedBack!: null; // Nav Property


}
