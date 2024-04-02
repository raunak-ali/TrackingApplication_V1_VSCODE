import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BatchDashboardComponent } from './batch-dashboard.component';

describe('BatchDashboardComponent', () => {
  let component: BatchDashboardComponent;
  let fixture: ComponentFixture<BatchDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BatchDashboardComponent]
    });
    fixture = TestBed.createComponent(BatchDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
