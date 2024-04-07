import { TestBed } from '@angular/core/testing';

import { AddsubtaskServiceService } from './addsubtask-service.service';

describe('AddsubtaskServiceService', () => {
  let service: AddsubtaskServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddsubtaskServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
