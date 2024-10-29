import { Batch } from "./batch";
import { GetBatches } from "./get-batches";

export class GetModules {
  moduleId!:number;
  moduleName!: string;
  description!: string;
  batchId!: number;
  batchs!:GetBatches;
}
