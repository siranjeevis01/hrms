export const environment = {
  production: false,
  apiUrl: 'http://localhost:8080',
  firebase: {
    apiKey: 'your-api-key',
    authDomain: 'your-project.firebaseapp.com',
    projectId: 'your-project-id',
    storageBucket: 'your-project.appspot.com',
    messagingSenderId: 'your-sender-id',
    appId: 'your-app-id',
  },
  signalRUrl: 'http://localhost:8080/hub/chat',
  wakeUpUrl: 'http://localhost:8080/api/v1/health/live',
  wakeUpDelayMs: 5000,
};
