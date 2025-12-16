import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingConditionsComponent } from './booking-conditions.component';

describe('BookingConditionsComponent', () => {
  let component: BookingConditionsComponent;
  let fixture: ComponentFixture<BookingConditionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookingConditionsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookingConditionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
