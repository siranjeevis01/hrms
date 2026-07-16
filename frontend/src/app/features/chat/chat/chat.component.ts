import { Component, OnInit, OnDestroy, inject, signal, ElementRef, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatBadgeModule } from '@angular/material/badge';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Subscription } from 'rxjs';
import { ChatService } from '../chat.service';
import { Conversation, Message, ChatUser } from '../chat.models';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    FormsModule, MatCardModule, MatButtonModule, MatIconModule,
    MatInputModule, MatBadgeModule, MatMenuModule, MatProgressSpinnerModule, MatTooltipModule,
  ],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss',
})
export class ChatComponent implements OnInit, OnDestroy {
  @ViewChild('messagesContainer') private messagesContainer!: ElementRef;
  @ViewChild('messageInput') private messageInput!: ElementRef;

  private chatService = inject(ChatService);
  private subscriptions: Subscription[] = [];

  conversations = signal<Conversation[]>([]);
  messages = signal<Message[]>([]);
  activeConversation = signal<Conversation | null>(null);
  currentUser = signal<ChatUser | null>(null);
  loading = signal(true);
  sending = signal(false);
  connected = signal(true);
  searchQuery = signal('');
  newMessage = signal('');
  typingUsers = signal<Record<string, string[]>>({});
  showMobileChat = signal(false);
  showNewConversation = signal(false);
  isTyping = false;
  typingTimeout: any;

  ngOnInit(): void {
    this.loadConversations();
    this.setupSubscriptions();
    this.chatService.startConnection('mock-token');
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((s) => s.unsubscribe());
    this.chatService.stopConnection();
  }

  loadConversations(): void {
    this.loading.set(true);
    this.chatService.getConversations().subscribe({
      next: (convos) => {
        this.conversations.set(convos);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  setupSubscriptions(): void {
    this.subscriptions.push(
      this.chatService.messageReceived.subscribe((message) => {
        if (message.conversationId === this.activeConversation()?.id) {
          this.messages.update((msgs) => [...msgs, message]);
          this.scrollToBottom();
        }
        this.conversations.update((convos) =>
          convos.map((c) => (c.id === message.conversationId ? { ...c, lastMessage: message, unreadCount: c.id === this.activeConversation()?.id ? 0 : c.unreadCount + 1 } : c)),
        );
      }),
      this.chatService.typingChanged.subscribe((indicator) => {
        this.typingUsers.update((current) => {
          const conv = current[indicator.conversationId] || [];
          if (indicator.isTyping) {
            return { ...current, [indicator.conversationId]: [...conv.filter((u) => u !== indicator.userName), indicator.userName] };
          }
          return { ...current, [indicator.conversationId]: conv.filter((u) => u !== indicator.userName) };
        });
      }),
      this.chatService.connectionStatus.subscribe((status) => this.connected.set(status)),
    );
  }

  selectConversation(conversation: Conversation): void {
    this.activeConversation.set(conversation);
    this.showMobileChat.set(true);
    this.messages.set([]);
    this.chatService.getMessages(conversation.id).subscribe({
      next: (msgs) => { this.messages.set(msgs); this.scrollToBottom(); },
    });
    if (conversation.unreadCount > 0) {
      this.chatService.markAsRead(conversation.id).subscribe();
      this.conversations.update((cs) => cs.map((c) => c.id === conversation.id ? { ...c, unreadCount: 0 } : c));
    }
  }

  sendMessage(): void {
    const content = this.newMessage().trim();
    if (!content || !this.activeConversation()) return;
    this.sending.set(true);
    this.chatService.sendMessage({ conversationId: this.activeConversation()!.id, content, type: 'text' }).subscribe({
      next: (msg) => {
        this.messages.update((msgs) => [...msgs, msg]);
        this.newMessage.set('');
        this.sending.set(false);
        this.scrollToBottom();
      },
      error: () => this.sending.set(false),
    });
  }

  onTyping(): void {
    if (!this.isTyping && this.activeConversation()) {
      this.isTyping = true;
      this.chatService.sendTypingIndicator(this.activeConversation()!.id, true);
    }
    clearTimeout(this.typingTimeout);
    this.typingTimeout = setTimeout(() => {
      this.isTyping = false;
      if (this.activeConversation()) this.chatService.sendTypingIndicator(this.activeConversation()!.id, false);
    }, 2000);
  }

  scrollToBottom(): void {
    setTimeout(() => {
      if (this.messagesContainer) {
        this.messagesContainer.nativeElement.scrollTop = this.messagesContainer.nativeElement.scrollHeight;
      }
    }, 50);
  }

  goBack(): void { this.showMobileChat.set(false); this.activeConversation.set(null); }

  formatTime(dateStr: string): string {
    const date = new Date(dateStr);
    return date.toLocaleTimeString('en-US', { hour: 'numeric', minute: '2-digit' });
  }

  formatDate(dateStr: string): string {
    const date = new Date(dateStr);
    const today = new Date();
    if (date.toDateString() === today.toDateString()) return 'Today';
    const yesterday = new Date(today);
    yesterday.setDate(today.getDate() - 1);
    if (date.toDateString() === yesterday.toDateString()) return 'Yesterday';
    return date.toLocaleDateString('en-US', { month: 'short', day: 'numeric' });
  }

  getOnlineStatus(user: ChatUser): string {
    return user.isOnline ? 'Online' : `Last seen ${this.formatTime(user.lastSeen)}`;
  }

  get filteredConversations(): Conversation[] {
    const q = this.searchQuery().toLowerCase();
    if (!q) return this.conversations();
    return this.conversations().filter((c) => c.name.toLowerCase().includes(q));
  }

  getActiveTypingUsers(): string[] {
    if (!this.activeConversation()) return [];
    return this.typingUsers()[this.activeConversation()!.id] || [];
  }
}
