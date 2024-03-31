import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMentorComponent } from './add-mentor.component';

describe('AddMentorComponent', () => {
  let component: AddMentorComponent;
  let fixture: ComponentFixture<AddMentorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddMentorComponent]
    });
    fixture = TestBed.createComponent(AddMentorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
