import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewSectionViewComponent } from './new-section-view.component';

describe('NewSectionViewComponent', () => {
  let component: NewSectionViewComponent;
  let fixture: ComponentFixture<NewSectionViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewSectionViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewSectionViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
