import { TestBed } from '@angular/core/testing';

import { ProcteredService } from './proctered.service';

describe('ProcteredService', () => {
  let service: ProcteredService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProcteredService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
