import { TestBed } from '@angular/core/testing';

import { RemoveEmployeesFromBatchService } from './remove-employees-from-batch.service';

describe('RemoveEmployeesFromBatchService', () => {
  let service: RemoveEmployeesFromBatchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RemoveEmployeesFromBatchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
