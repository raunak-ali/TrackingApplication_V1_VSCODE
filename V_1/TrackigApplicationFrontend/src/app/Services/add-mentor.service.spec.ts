import { TestBed } from '@angular/core/testing';

import { AddMentorService } from './add-mentor.service';

describe('AddMentorService', () => {
  let service: AddMentorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddMentorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
