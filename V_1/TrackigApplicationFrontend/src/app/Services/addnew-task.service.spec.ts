import { TestBed } from '@angular/core/testing';

import { AddnewTaskService } from './addnew-task.service';

describe('AddnewTaskService', () => {
  let service: AddnewTaskService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddnewTaskService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
