import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuleDashboardComponent } from './module-dashboard.component';

describe('ModuleDashboardComponent', () => {
  let component: ModuleDashboardComponent;
  let fixture: ComponentFixture<ModuleDashboardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModuleDashboardComponent]
    });
    fixture = TestBed.createComponent(ModuleDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
