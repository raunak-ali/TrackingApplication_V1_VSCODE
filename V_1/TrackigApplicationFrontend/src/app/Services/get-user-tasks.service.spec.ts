import { TestBed } from '@angular/core/testing';

import { GetUserTasksService } from './get-user-tasks.service';

describe('GetUserTasksService', () => {
  let service: GetUserTasksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetUserTasksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
