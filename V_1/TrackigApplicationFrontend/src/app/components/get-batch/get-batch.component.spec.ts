import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetBatchComponent } from './get-batch.component';

describe('GetBatchComponent', () => {
  let component: GetBatchComponent;
  let fixture: ComponentFixture<GetBatchComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GetBatchComponent]
    });
    fixture = TestBed.createComponent(GetBatchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
