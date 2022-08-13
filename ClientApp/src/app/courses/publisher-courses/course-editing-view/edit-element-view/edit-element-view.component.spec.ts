import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditElementViewComponent } from './edit-element-view.component';

describe('EditElementViewComponent', () => {
  let component: EditElementViewComponent;
  let fixture: ComponentFixture<EditElementViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditElementViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditElementViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
