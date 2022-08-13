import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherCoursesComponent } from './publisher-courses.component';

describe('PublisherCoursesComponent', () => {
  let component: PublisherCoursesComponent;
  let fixture: ComponentFixture<PublisherCoursesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublisherCoursesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublisherCoursesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
