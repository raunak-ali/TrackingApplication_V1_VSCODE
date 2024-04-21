import { TestBed } from '@angular/core/testing';

import { CompileAndExecuteService } from './compile-and-execute.service';

describe('CompileAndExecuteService', () => {
  let service: CompileAndExecuteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CompileAndExecuteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
