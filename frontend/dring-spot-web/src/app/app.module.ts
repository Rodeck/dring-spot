import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

// Reactive Form
import { ReactiveFormsModule, FormsModule } from "@angular/forms";

// App routing modules
import { AppRoutingModule } from './shared/routing/app-routing.module';

// App components
import { AppComponent } from './app.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { VerifyEmailComponent } from './components/verify-email/verify-email.component';

// Firebase services + enviorment module
import { AngularFireModule } from "@angular/fire";
import { AngularFireAuthModule } from "@angular/fire/auth";
import { AngularFirestoreModule } from '@angular/fire/firestore';
import { environment } from '../environments/environment';

// Auth service
import { AuthService } from "./shared/services/auth.service";
import { MeetingPlaceAddComponent } from './components/meeting-place-add/meeting-place-add.component';
import { AgmCoreModule } from '@agm/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { PlacesComponent } from './components/places/places.component';
import { TokenInterceptor } from './services/token.interceptor';
import { PlaceDetailsComponent } from './components/place-details/place-details.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { StoreModule } from '@ngrx/store';
import { reducer, appReducers } from './store/reducers/app.reducers';
import { EffectsModule } from '@ngrx/effects';
import { AppEffects } from './store/effects/app.effects';
import { StoreDevtoolsModule } from "@ngrx/store-devtools";


@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
    SignUpComponent,
    DashboardComponent,
    ForgotPasswordComponent,
    VerifyEmailComponent,
    MeetingPlaceAddComponent,
    PlacesComponent,
    PlaceDetailsComponent,
    UserProfileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AngularFireModule.initializeApp(environment.firebase),
    AngularFireAuthModule,
    AngularFirestoreModule,
    ReactiveFormsModule,
    AgmCoreModule.forRoot({
      apiKey: environment.mapKey
    }),
    HttpClientModule,
    FormsModule,
    NgbModule,
    StoreModule.forRoot(appReducers),
    EffectsModule.forRoot([AppEffects]),
    environment.production ? [] : StoreDevtoolsModule.instrument()
  ],
  providers: [AuthService, 
    {
      provide: 'BASE_URL', 
      useValue: environment.baseUrl
    },     
    { 
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: 'MAPS_URL', 
      useValue: environment.mapsUrl
    },
    {
      provide: 'MAPS_APIKEY', 
      useValue: environment.mapKey
    }
  ],
  bootstrap: [AppComponent],
  entryComponents: [ PlaceDetailsComponent ]
})

export class AppModule { }