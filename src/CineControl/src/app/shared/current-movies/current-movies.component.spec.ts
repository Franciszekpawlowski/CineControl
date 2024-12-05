import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrentMoviesComponent } from './current-movies.component';

describe('CurrentMoviesComponent', () => {
  let component: CurrentMoviesComponent;
  let fixture: ComponentFixture<CurrentMoviesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CurrentMoviesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CurrentMoviesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
