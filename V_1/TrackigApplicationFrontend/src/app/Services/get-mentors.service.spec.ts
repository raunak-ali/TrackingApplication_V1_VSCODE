import { TestBed } from '@angular/core/testing';

import { GetMentorsService } from './get-mentors.service';

describe('GetMentorsService', () => {
  let service: GetMentorsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetMentorsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
