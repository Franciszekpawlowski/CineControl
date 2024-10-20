import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { MovieDetailsComponent } from './pages/movie-details/movie-details.component';
import { RepertoireComponent } from './pages/repertoire/repertoire.component';
import { LoginComponent } from './shared/login/login.component';
import { RegistrationComponent } from './shared/registration/registration.component';


export const routes: Routes = [
    { path: '', component: HomeComponent },
  { path: 'movie/:id', component: MovieDetailsComponent },
  {path:'repertoire', component: RepertoireComponent},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },
];
