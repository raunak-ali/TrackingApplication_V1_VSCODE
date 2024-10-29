import { TestBed } from '@angular/core/testing';

import { GetModuleFeedbackService } from './get-module-feedback.service';

describe('GetModuleFeedbackService', () => {
  let service: GetModuleFeedbackService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetModuleFeedbackService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
