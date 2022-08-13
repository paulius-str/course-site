import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewCourseCreationViewComponent } from './new-course-creation-view.component';

describe('NewCourseCreationViewComponent', () => {
  let component: NewCourseCreationViewComponent;
  let fixture: ComponentFixture<NewCourseCreationViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewCourseCreationViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewCourseCreationViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
