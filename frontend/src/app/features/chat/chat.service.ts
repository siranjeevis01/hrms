import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import {
  Conversation,
  Message,
  TypingIndicator,
  SendMessageRequest,
  CreateConversationRequest,
} from './chat.models';

@Injectable({ providedIn: 'root' })
export class ChatService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/api/chat`;
  private hubConnection: signalR.HubConnection | null = null;

  private messageReceived$ = new Subject<Message>();
  private typingIndicator$ = new Subject<TypingIndicator>();
  private conversationUpdated$ = new Subject<Conversation>();
  private connectionStatus$ = new Subject<boolean>();

  messageReceived = this.messageReceived$.asObservable();
  typingChanged = this.typingIndicator$.asObservable();
  conversationUpdated = this.conversationUpdated$.asObservable();
  connectionStatus = this.connectionStatus$.asObservable();

  getConversations(): Observable<Conversation[]> {
    return this.http.get<Conversation[]>(`${this.apiUrl}/Conversations`).pipe(
      catchError(() => of([]))
    );
  }

  getMessages(conversationId: string, before?: string): Observable<Message[]> {
    let url = `${this.apiUrl}/Conversations/${conversationId}/messages`;
    if (before) url += `?before=${before}`;
    return this.http.get<Message[]>(url).pipe(
      catchError(() => of([]))
    );
  }

  sendMessage(request: SendMessageRequest): Observable<Message> {
    return this.http.post<Message>(`${this.apiUrl}/Messages`, request);
  }

  createConversation(request: CreateConversationRequest): Observable<Conversation> {
    return this.http.post<Conversation>(`${this.apiUrl}/Conversations`, request);
  }

  markAsRead(conversationId: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/Conversations/${conversationId}/read`, {});
  }

  sendTypingIndicator(conversationId: string, isTyping: boolean): void {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      this.hubConnection.invoke('SendTypingIndicator', conversationId, isTyping).catch(() => {});
    }
  }

  startConnection(token: string): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiUrl}/hubs/notifications`, {
        accessTokenFactory: () => token,
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.on('ReceiveMessage', (message: Message) => {
      this.messageReceived$.next(message);
    });

    this.hubConnection.on('ReceiveTypingIndicator', (indicator: TypingIndicator) => {
      this.typingIndicator$.next(indicator);
    });

    this.hubConnection.on('ConversationUpdated', (conversation: Conversation) => {
      this.conversationUpdated$.next(conversation);
    });

    this.hubConnection.onreconnected(() => {
      this.connectionStatus$.next(true);
    });

    this.hubConnection.onclose(() => {
      this.connectionStatus$.next(false);
    });

    this.hubConnection
      .start()
      .then(() => this.connectionStatus$.next(true))
      .catch(() => this.connectionStatus$.next(false));
  }

  stopConnection(): void {
    try {
      this.hubConnection?.stop();
    } catch {
      // Ignore errors on disconnect
    }
    this.hubConnection = null;
  }
}
