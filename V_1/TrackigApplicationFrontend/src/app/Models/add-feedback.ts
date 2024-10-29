import { GetModules } from "./get-modules";

export class AddFeedback {
  feedbackId!: number;
  totalAverageRating?: number | null;
  comments?: any;
  description?: string | null;
  user?: UserModel | null;
  userTask?: TaskModel | null;
  module?: GetModules | null;
  ratings?: any[]|null;
  Submission_Count?: string | null;
}

interface UserModel {
  userId: number;
  name: string;
  total_Average_RatingStatus: number;
}

interface TaskModel {
  userTaskID: number;
  taskName: string;
}

interface Module {
  ModuleId: number;
  ModuleName: string;
}
