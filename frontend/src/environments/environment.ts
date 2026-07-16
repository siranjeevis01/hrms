export const environment = {
  production: false,
  apiUrl: 'https://hrms-30g6.onrender.com',
  signalRUrl: 'https://hrms-30g6.onrender.com/hubs/chat',
  wakeUpUrl: 'https://hrms-30g6.onrender.com/api/v1/health/live',
  wakeUpDelayMs: 65000,
  firebase: {
    apiKey: 'your-api-key',
    authDomain: 'your-project.firebaseapp.com',
    projectId: 'your-project-id',
    storageBucket: 'your-project.appspot.com',
    messagingSenderId: 'your-sender-id',
    appId: 'your-app-id',
  },
};
