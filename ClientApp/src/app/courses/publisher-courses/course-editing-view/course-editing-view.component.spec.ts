import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseEditingViewComponent } from './course-editing-view.component';

describe('CourseEditingViewComponent', () => {
  let component: CourseEditingViewComponent;
  let fixture: ComponentFixture<CourseEditingViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CourseEditingViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseEditingViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
