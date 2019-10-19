import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs';

export interface Credentials {
  email: string;
  password: string;
}
 
@Injectable({providedIn: 'root'})
export class AuthService {
  readonly authState$: Observable<firebase.User | null> = this.fireAuth.authState;
 
  constructor(private fireAuth: AngularFireAuth) {}
 
  get user(): firebase.User | null {
    return this.fireAuth.auth.currentUser;
  }
 
  login({email, password}: Credentials) {
    return this.fireAuth.auth.signInWithEmailAndPassword(email, password);
  }
 
  register({email, password}: Credentials) {
    return this.fireAuth.auth.createUserWithEmailAndPassword(email, password);
  }
 
  logout() {
    return this.fireAuth.auth.signOut();
  }
}