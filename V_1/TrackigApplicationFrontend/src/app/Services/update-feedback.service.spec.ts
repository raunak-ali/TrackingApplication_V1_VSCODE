import { TestBed } from '@angular/core/testing';

import { UpdateFeedbackService } from './update-feedback.service';

describe('UpdateFeedbackService', () => {
  let service: UpdateFeedbackService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UpdateFeedbackService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
