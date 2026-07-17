import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AppNotification } from '../models/common.model';
import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  private http = inject(HttpClient);
  private authService = inject(AuthService);
  private hubConnection: HubConnection | null = null;

  notifications = signal<AppNotification[]>([]);
  unreadCount = computed(() => this.notifications().filter((n) => !n.isRead).length);

  startConnection(): void {
    const token = this.authService.getToken();
    if (!token) return;

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(environment.signalRUrl, {
        accessTokenFactory: () => token,
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => this.listenForNotifications())
      .catch((err) => console.error('SignalR connection error:', err));
  }

  stopConnection(): void {
    if (this.hubConnection?.state === HubConnectionState.Connected) {
      this.hubConnection.stop().catch((err) => console.error('SignalR disconnect error:', err));
    }
  }

  listenForNotifications(): void {
    this.hubConnection?.on('ReceiveNotification', (notification: AppNotification) => {
      this.notifications.update((notifications) => [notification, ...notifications]);
    });
  }

  markAsRead(id: string): void {
    this.notifications.update((notifications) =>
      notifications.map((n) => (n.id === id ? { ...n, isRead: true } : n))
    );
    this.http.put(`${environment.apiUrl}/api/notifications/Notifications/${id}/read`, {}).subscribe();
  }

  markAllAsRead(): void {
    this.notifications.update((notifications) =>
      notifications.map((n) => ({ ...n, isRead: true }))
    );
    this.http.put(`${environment.apiUrl}/api/notifications/Notifications/read-all`, {}).subscribe();
  }

  getNotifications(): Observable<AppNotification[]> {
    return this.http.get<AppNotification[]>(`${environment.apiUrl}/api/notifications/Notifications`);
  }

  deleteNotification(id: string): void {
    this.notifications.update((notifications) => notifications.filter((n) => n.id !== id));
    this.http.delete(`${environment.apiUrl}/api/notifications/Notifications/${id}`).subscribe();
  }
}
