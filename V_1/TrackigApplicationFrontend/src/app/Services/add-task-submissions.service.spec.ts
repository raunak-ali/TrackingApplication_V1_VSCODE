import { TestBed } from '@angular/core/testing';

import { AddTaskSubmissionsService } from './add-task-submissions.service';

describe('AddTaskSubmissionsService', () => {
  let service: AddTaskSubmissionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddTaskSubmissionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
