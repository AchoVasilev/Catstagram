import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthenticationService } from './services/authentication.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CreatepostComponent } from './createpost/createpost.component';
import { CatService } from './services/cat.service';
import { AuthGuardService } from './services/auth-guard.service';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { ListCatsComponent } from './list-cats/list-cats.component';
import { DetailsCatComponent } from './details-cat/details-cat.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    CreatepostComponent,
    ListCatsComponent,
    DetailsCatComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    AuthenticationService,
    CatService,
    AuthGuardService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    },

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
