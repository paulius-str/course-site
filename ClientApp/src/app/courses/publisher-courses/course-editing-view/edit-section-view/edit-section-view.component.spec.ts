import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSectionViewComponent } from './edit-section-view.component';

describe('NewSectionViewComponent', () => {
  let component: EditSectionViewComponent;
  let fixture: ComponentFixture<EditSectionViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditSectionViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditSectionViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
