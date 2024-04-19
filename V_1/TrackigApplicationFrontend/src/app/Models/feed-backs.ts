import { GetTask } from "./get-task";
import { GetUser } from "./get-user";
import { User } from "./user";

export class FeedBacks {
  feedbackId!: number;
  taskId!: number;
  totalAverageRating!: number;
  comments!: number;
  description!: string;
  userId!: number;
  userTask!: GetTask;
  user!: GetUser;
  ratings!: any[];
}
