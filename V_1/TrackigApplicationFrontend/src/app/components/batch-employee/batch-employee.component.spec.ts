import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BatchEmployeeComponent } from './batch-employee.component';

describe('BatchEmployeeComponent', () => {
  let component: BatchEmployeeComponent;
  let fixture: ComponentFixture<BatchEmployeeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BatchEmployeeComponent]
    });
    fixture = TestBed.createComponent(BatchEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
