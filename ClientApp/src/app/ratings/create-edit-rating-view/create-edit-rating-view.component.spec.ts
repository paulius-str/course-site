import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditRatingViewComponent } from './create-edit-rating-view.component';

describe('CreateEditRatingViewComponent', () => {
  let component: CreateEditRatingViewComponent;
  let fixture: ComponentFixture<CreateEditRatingViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateEditRatingViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEditRatingViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
