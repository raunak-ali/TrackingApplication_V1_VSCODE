import { TestBed } from '@angular/core/testing';

import { AddBatchesService } from './add-batches.service';

describe('AddBatchesService', () => {
  let service: AddBatchesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddBatchesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
