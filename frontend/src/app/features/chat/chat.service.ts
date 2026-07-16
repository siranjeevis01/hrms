import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr';
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
  private apiUrl = '/api/chat';
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
    return this.http.get<Conversation[]>(`${this.apiUrl}/conversations`);
  }

  getMessages(conversationId: string, before?: string): Observable<Message[]> {
    let url = `${this.apiUrl}/conversations/${conversationId}/messages`;
    if (before) url += `?before=${before}`;
    return this.http.get<Message[]>(url);
  }

  sendMessage(request: SendMessageRequest): Observable<Message> {
    return this.http.post<Message>(`${this.apiUrl}/messages`, request);
  }

  createConversation(request: CreateConversationRequest): Observable<Conversation> {
    return this.http.post<Conversation>(`${this.apiUrl}/conversations`, request);
  }

  markAsRead(conversationId: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/conversations/${conversationId}/read`, {});
  }

  sendTypingIndicator(conversationId: string, isTyping: boolean): void {
    if (this.hubConnection?.state === signalR.HubConnectionState.Connected) {
      this.hubConnection.invoke('SendTypingIndicator', conversationId, isTyping);
    }
  }

  startConnection(token: string): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/hubs/chat', {
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
    this.hubConnection?.stop();
    this.hubConnection = null;
  }
}
