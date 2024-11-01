import { Batch } from "./batch";
import { GetModules } from "./get-modules";
import { SubTask } from "./sub-task";
import { User } from "./user";

export class GetTask {
    userTaskID!: number;
    taskName!: string;
    description!: string;
    priority!: number;
    deadLine!: Date;
    status!: number;
    assignedBy!: number;
    assignedTo!: number;
    batchId!: number;
    comments!: string;
    createdAt!: Date | null;
    assignedByUser!: User | null; // Nav Property
   // batches!: Batch | null; // Nav Property
    assignedToUser!: User[] | null; // Nav Property
    subTasks!: SubTask[]; // Nav Property
    feedBack!: null; // Nav Property
    ModuleId!:number;
    module!:GetModules;


}
