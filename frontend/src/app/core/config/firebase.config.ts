import { initializeApp, type FirebaseApp } from 'firebase/app';
import { getAuth, type Auth } from 'firebase/auth';
import { environment } from '../../../environments/environment';

let firebaseApp: FirebaseApp;
let firebaseAuth: Auth;

export function initializeFirebase(): { app: FirebaseApp; auth: Auth } {
  firebaseApp = initializeApp(environment.firebase);
  firebaseAuth = getAuth(firebaseApp);
  return { app: firebaseApp, auth: firebaseAuth };
}

export function getFirebaseApp(): FirebaseApp {
  if (!firebaseApp) {
    throw new Error('Firebase not initialized. Call initializeFirebase() first.');
  }
  return firebaseApp;
}

export function getFirebaseAuth(): Auth {
  if (!firebaseAuth) {
    throw new Error('Firebase not initialized. Call initializeFirebase() first.');
  }
  return firebaseAuth;
}
