import { bootstrapApplication } from '@angular/platform-browser';
import { routes } from './app/app.routes'; // Zdefiniowane trasy
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { AppComponent } from './app/app.component';

import { provideHttpClient } from '@angular/common/http';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { provideAnimations } from '@angular/platform-browser/animations';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes, withComponentInputBinding()),
    provideHttpClient(),
    provideAnimations(),
    provideHttpClient(),
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
  ],
});
