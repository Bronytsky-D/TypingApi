import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { KeyupDirective } from './keyup.directive';
import { TypingGameComponent } from './domain/typing-game/typing-game.component';
import { LoginComponent } from './domain/login/login.component';
import { RegistrationComponent } from './domain/registration/registration.component';
import { LayoutComponent } from './layout/layout.component';
import { HttpClientModule } from '@angular/common/http';
import { ProfileComponent } from './domain/profile/profile.component';
import { NgApexchartsModule } from 'ng-apexcharts';

@NgModule({
  declarations: [
    AppComponent,
    KeyupDirective,
    TypingGameComponent,
    LoginComponent,
    RegistrationComponent,
    LayoutComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    NgApexchartsModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
