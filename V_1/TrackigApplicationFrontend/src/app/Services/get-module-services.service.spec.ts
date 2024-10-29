import { TestBed } from '@angular/core/testing';

import { GetModuleServicesService } from './get-module-services.service';

describe('GetModuleServicesService', () => {
  let service: GetModuleServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetModuleServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
