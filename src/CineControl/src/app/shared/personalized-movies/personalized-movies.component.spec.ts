import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalizedMoviesComponent } from './personalized-movies.component';

describe('PersonalizedMoviesComponent', () => {
  let component: PersonalizedMoviesComponent;
  let fixture: ComponentFixture<PersonalizedMoviesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PersonalizedMoviesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonalizedMoviesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
