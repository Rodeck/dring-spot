import { Injectable, NgZone, Inject } from '@angular/core';
import { User } from "../services/user";
import { auth, storage } from 'firebase/app';
import { AngularFireAuth } from "@angular/fire/auth";
import { AngularFirestore, AngularFirestoreDocument } from '@angular/fire/firestore';
import { Router } from "@angular/router";
import { HttpClient } from '@angular/common/http';
import { UserModel } from 'src/app/models/user.model';
import { Observable } from 'rxjs';
import { map, take } from "rxjs/operators";
import { AppState, BaseState } from 'src/app/store/state/app.state';
import { Store, select } from '@ngrx/store';
import { loadPlaces, userLoggedIn } from 'src/app/store/actions/app.actions';
import { selectAppState } from 'src/app/store/selectors/app.selectors';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  userData: any; // Save logged in user data
  url: string = this.baseUrl + "User"

  constructor(
    public afs: AngularFirestore,   // Inject Firestore service
    public afAuth: AngularFireAuth, // Inject Firebase auth service
    public router: Router,  
    public ngZone: NgZone, // NgZone service to remove outside scope warning,
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string,
    private store: Store<BaseState>
  ) {    
    /* Saving user data in localstorage when 
    logged in and setting up null when logged out */
    this.afAuth.authState.subscribe(user => {
      if (user) {
        this.userData = user;
        localStorage.setItem('user', JSON.stringify(this.userData));
        user.getIdToken().then(x => localStorage.setItem('token', x));
        JSON.parse(localStorage.getItem('user'));
      } else {
        localStorage.setItem('user', null);
        localStorage.setItem('token', null)
        JSON.parse(localStorage.getItem('user'));
      }
    })
  }

  // Sign in with email/password
  SignIn(email, password) {
    return this.afAuth.auth.signInWithEmailAndPassword(email, password)
      .then((result) => {
        this.ngZone.run(() => {
          this.redirect(result.user);
        });
      }).catch((error) => {
        window.alert(error.message)
      })
  }

  redirect(user) {
    this.SetUserData(user);
    this.GetUserData().subscribe(x => {
      this.store.dispatch(userLoggedIn({ user: x }));
      this.store.subscribe(() => localStorage.setItem('state', JSON.stringify(this.getState())));
      this.router.navigate(['dashboard']);
    });
  }


  getState(): AppState {
    let state: AppState;

    this.store.pipe(select(selectAppState), take(1)).subscribe(
        s => state = s
    );

    return state;
  }

  // Sign up with email/password
  SignUp(email, password) {
    return this.afAuth.auth.createUserWithEmailAndPassword(email, password)
      .then((result) => {
        /* Call the SendVerificaitonMail() function when new user sign 
        up and returns promise */
        this.SendVerificationMail();
        this.SetUserData(result.user);
        this.SaveUser(result.user);
      }).catch((error) => {
        window.alert(error.message)
      })
  }

  // Send email verfificaiton when new user sign up
  SendVerificationMail() {
    return this.afAuth.auth.currentUser.sendEmailVerification()
    .then(() => {
      this.router.navigate(['verify-email-address']);
    })
  }

  // Reset Forggot password
  ForgotPassword(passwordResetEmail) {
    return this.afAuth.auth.sendPasswordResetEmail(passwordResetEmail)
    .then(() => {
      window.alert('Password reset email sent, check your inbox.');
    }).catch((error) => {
      window.alert(error)
    })
  }

  SaveUser(user) {
    this.http.post<User>(this.url, {
      uid: user.uid,
      email: user.email,
    }).subscribe(x => x);
  }

  // Returns true when user is looged in and email is verified
  get isLoggedIn(): boolean {
    const user = JSON.parse(localStorage.getItem('user'));
    return (user !== null && user.emailVerified !== false) ? true : false;
  }

  // Sign in with Google
  GoogleAuth() {
    return this.AuthLogin(new auth.GoogleAuthProvider());
  }

  // Auth logic to run auth providers
  AuthLogin(provider) {
    return this.afAuth.auth.signInWithPopup(provider)
    .then((result) => {
      this.ngZone.run(() => {
        this.redirect(result.user);
      })
      this.SetUserData(result.user);
    }).catch((error) => {
      window.alert(error)
    })
  }

  /* Setting up user data when sign in with username/password, 
  sign up with username/password and sign in with social auth  
  provider in Firestore database using AngularFirestore + AngularFirestoreDocument service */
  SetUserData(user) {
    const userRef: AngularFirestoreDocument<any> = this.afs.doc(`users/${user.uid}`);
    const userData: User = {
      uid: user.uid,
      email: user.email,
      displayName: user.displayName,
      photoURL: user.photoURL,
      emailVerified: user.emailVerified
    }
    return userRef.set(userData, {
      merge: true
    })
  }

  GetUserId(): string {
    const user = JSON.parse(localStorage.getItem('user'));
    return user.uid;
  } 

  GetUserData(): Observable<UserModel> {
    const userRef: AngularFirestoreDocument<User> = this.afs.doc(`users/${this.GetUserId()}`);
    return userRef.get().pipe(
      map(doc => {
        let user: UserModel = {
          uid: this.GetUserId(),
          firstName: "John",
          lastName: "Doe",
          imageUrl: "data:image/jpeg;base64,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",
          email: doc.get("email"),
        };
        return user;
      }));
  }

  // Sign out 
  SignOut() {
    return this.afAuth.auth.signOut().then(() => {
      localStorage.removeItem('user');
      localStorage.removeItem('token');
      this.router.navigate(['sign-in']);
    })
  }

  GetToken() {
    return localStorage.getItem('token');
  }
}