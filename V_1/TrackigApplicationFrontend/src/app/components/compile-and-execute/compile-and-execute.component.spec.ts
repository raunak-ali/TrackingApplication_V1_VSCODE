import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompileAndExecuteComponent } from './compile-and-execute.component';

describe('CompileAndExecuteComponent', () => {
  let component: CompileAndExecuteComponent;
  let fixture: ComponentFixture<CompileAndExecuteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CompileAndExecuteComponent]
    });
    fixture = TestBed.createComponent(CompileAndExecuteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
