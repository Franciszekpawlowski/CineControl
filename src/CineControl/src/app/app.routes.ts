import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { MovieDetailsComponent } from './pages/movie-details/movie-details.component';
import { RepertoireComponent } from './pages/repertoire/repertoire.component';
import { LoginComponent } from './shared/login/login.component';
import { RegistrationComponent } from './shared/registration/registration.component';
import { BookingComponent } from './pages/booking/booking.component';
import { ContactComponent } from './pages/contact/contact.component';
import { TermsAndConditionsComponent } from './pages/terms-and-conditions/terms-and-conditions.component';

import { PrivacyPolicyComponent } from './pages/privacy-policy/privacy-policy.component';
import { BookingConditionsComponent } from './pages/booking-conditions/booking-conditions.component';
import { HelpComponent } from './pages/help/help.component';
import { UserPanelComponent } from './pages/user-panel/user-panel.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'movie/:id', component: MovieDetailsComponent },
  { path: 'repertoire', component: RepertoireComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'booking/:id', component: BookingComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'terms', component: TermsAndConditionsComponent },
  { path: 'privacy', component: PrivacyPolicyComponent },
  { path: 'booking-conditions', component: BookingConditionsComponent },
  { path: 'help', component: HelpComponent },
  { path: 'user-panel', component: UserPanelComponent },
];

