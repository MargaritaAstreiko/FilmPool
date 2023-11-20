import { Routes, RouterModule } from '@angular/router';
import { AdminGuard } from './helpers/adminguard';
import { AuthGuard } from './helpers/authguard';
import { LoginComponent } from './pages/authentication/login.component';
import { FilmComponent } from './pages/film-page/film-page.component';
import { FilmsListComponent } from './pages/films-main/films-main.component';
import { ForgotPasswordComponent } from './pages/forgotPassword/forgot-password.component';
import { RegisterUserComponent } from './pages/registration/registration.component';
import { ResetPasswordComponent } from './pages/resetPassword/resetPassword.component';
import { UserlistComponent } from './pages/userlist/userlist.component';
import { CollectionComponent } from './pages/collection/collection.component';
import { UserComponent } from './pages/user/user.component';
import { FilmCreateComponent } from './pages/create-film/create-film.component';



const routes: Routes = [
    { path: '', component: FilmsListComponent, canActivate: [AuthGuard]},//canActivate: [AuthGuard]},
   // { path: 'register', component: RegisterUserComponent },
    { path: 'films', component: FilmsListComponent, canActivate: [AuthGuard]},
    { path: 'film/:id', component: FilmComponent, canActivate: [AuthGuard]},
    { path: 'users', component: UserlistComponent, canActivate: [AuthGuard,AdminGuard]},
    { path: 'user/:id', component: UserComponent, canActivate: [AuthGuard]},
    { path: 'collections', component: CollectionComponent, canActivate: [AuthGuard]},
    { path: 'forgotPassword', component: ForgotPasswordComponent },
    { path: 'resetPassword', component: ResetPasswordComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterUserComponent },
    { path: 'new-film', component: FilmCreateComponent, canActivate: [AuthGuard,AdminGuard]},
    { path: '', redirectTo: '/films', pathMatch: 'full' },
    { path: '**', redirectTo: '/404', pathMatch: 'full'},
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const appRoutingModule = RouterModule.forRoot(routes);