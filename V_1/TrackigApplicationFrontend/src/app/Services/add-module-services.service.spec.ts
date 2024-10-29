import { TestBed } from '@angular/core/testing';

import { AddModuleServicesService } from './add-module-services.service';

describe('AddModuleServicesService', () => {
  let service: AddModuleServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddModuleServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
