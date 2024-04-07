import { TestBed } from '@angular/core/testing';

import { GetUserByBatchService } from './get-user-by-batch.service';

describe('GetUserByBatchService', () => {
  let service: GetUserByBatchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetUserByBatchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
