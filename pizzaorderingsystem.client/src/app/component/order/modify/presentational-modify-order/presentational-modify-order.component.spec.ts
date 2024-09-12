import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PresentationalModifyOrderComponent } from './presentational-modify-order.component';

describe('PresentationalModifyOrderComponent', () => {
  let component: PresentationalModifyOrderComponent;
  let fixture: ComponentFixture<PresentationalModifyOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PresentationalModifyOrderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PresentationalModifyOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
