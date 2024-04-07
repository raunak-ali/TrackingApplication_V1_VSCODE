import { TestBed } from '@angular/core/testing';

import { GetSubTasksByTaskService } from './get-sub-tasks-by-task.service';

describe('GetSubTasksByTaskService', () => {
  let service: GetSubTasksByTaskService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetSubTasksByTaskService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
