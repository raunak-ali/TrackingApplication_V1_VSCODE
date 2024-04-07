import { TestBed } from '@angular/core/testing';

import { AddNewRatingService } from './add-new-rating.service';

describe('AddNewRatingService', () => {
  let service: AddNewRatingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddNewRatingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
