import { TestBed } from '@angular/core/testing';

import { AddEmployeesToBatchService } from './add-employees-to-batch.service';

describe('AddEmployeesToBatchService', () => {
  let service: AddEmployeesToBatchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddEmployeesToBatchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
