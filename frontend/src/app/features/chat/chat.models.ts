export type ConversationType = 'direct' | 'group' | 'channel';
export type MessageStatus = 'sending' | 'sent' | 'delivered' | 'read';

export interface Conversation {
  id: string;
  type: ConversationType;
  name: string;
  description: string;
  avatar: string;
  lastMessage: Message | null;
  unreadCount: number;
  participants: ChatUser[];
  isPinned: boolean;
  isMuted: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface ChatUser {
  id: string;
  name: string;
  avatar: string;
  isOnline: boolean;
  lastSeen: string;
  role: string;
}

export interface Message {
  id: string;
  conversationId: string;
  senderId: string;
  senderName: string;
  senderAvatar: string;
  content: string;
  type: 'text' | 'image' | 'file' | 'system';
  status: MessageStatus;
  replyTo: Message | null;
  attachments: MessageAttachment[];
  createdAt: string;
  updatedAt: string;
}

export interface MessageAttachment {
  id: string;
  name: string;
  url: string;
  type: string;
  size: number;
}

export interface TypingIndicator {
  conversationId: string;
  userId: string;
  userName: string;
  isTyping: boolean;
}

export interface SendMessageRequest {
  conversationId: string;
  content: string;
  type: 'text' | 'image' | 'file';
  replyToId?: string;
}

export interface CreateConversationRequest {
  type: ConversationType;
  name: string;
  participantIds: string[];
  description?: string;
}
