import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewElementViewComponent } from './new-element-view.component';

describe('NewElementViewComponent', () => {
  let component: NewElementViewComponent;
  let fixture: ComponentFixture<NewElementViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewElementViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewElementViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
