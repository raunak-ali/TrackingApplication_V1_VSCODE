import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskSubmissionsComponent } from './task-submissions.component';

describe('TaskSubmissionsComponent', () => {
  let component: TaskSubmissionsComponent;
  let fixture: ComponentFixture<TaskSubmissionsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TaskSubmissionsComponent]
    });
    fixture = TestBed.createComponent(TaskSubmissionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
