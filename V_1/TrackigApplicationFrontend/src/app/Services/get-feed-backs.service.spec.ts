import { TestBed } from '@angular/core/testing';

import { GetFeedBacksService } from './get-feed-backs.service';

describe('GetFeedBacksService', () => {
  let service: GetFeedBacksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetFeedBacksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
