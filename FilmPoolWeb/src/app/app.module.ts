import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { appRoutingModule } from './app-routing';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FilmsListComponent } from './pages/films-main/films-main.component';
import { UserlistComponent } from './pages/userlist/userlist.component';
import { FilmFilterComponent } from './pages/film/filmFilter.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { UsersService } from './services/users.service';
import { FilmsService } from './services/films.service';
import { NgxPaginationModule } from 'ngx-pagination';
import { FilterPipe } from './helpers/filter';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthenticationService } from './services/authentication.service';
import { LoginComponent } from './pages/authentication/login.component';
import { RegisterUserComponent } from './pages/registration/registration.component';
//import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtModule } from "@auth0/angular-jwt";
import { ErrorHandlerService } from './services/error.service';
import { ForgotPasswordComponent } from './pages/forgotPassword/forgot-password.component';
import { ResetPasswordComponent } from './pages/resetPassword/resetPassword.component';
import { FilmComponent } from './pages/film-page/film-page.component';
import { FilmCommentsComponent } from './pages/comments/comment.component';
import { HeaderComponent } from './shared/header/header.component';
import { CollectionComponent } from './pages/collection/collection.component';
import { NgxBootstrapIconsModule, allIcons } from 'ngx-bootstrap-icons';
import { VideoPlayerComponent } from './shared/video-player/video-player.component';
import { UserComponent } from './pages/user/user.component';


export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    UserlistComponent,
    FilmsListComponent,
    FilmFilterComponent,
    FilmCommentsComponent,
    FilterPipe,
    LoginComponent,
    RegisterUserComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    HeaderComponent,
    FilmComponent,
    CollectionComponent,
    VideoPlayerComponent,
    UserComponent,
    
  ],
  imports: [
    BrowserModule,
    appRoutingModule,
    HttpClientModule,
    NgxPaginationModule,
    FormsModule,
    ReactiveFormsModule,
    NgxBootstrapIconsModule.pick(allIcons),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001"],
        disallowedRoutes: []
      }
    })

  ],
  providers: [UsersService, FilmsService, AuthenticationService,  { provide: HTTP_INTERCEPTORS,useClass: ErrorHandlerService, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
