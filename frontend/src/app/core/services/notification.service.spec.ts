import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { NotificationService } from './notification.service';

describe('NotificationService', () => {
  let service: NotificationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NotificationService, provideHttpClient()]
    });
    service = TestBed.inject(NotificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return empty notifications initially', () => {
    expect(service.notifications().length).toBe(0);
  });

  it('should return 0 unread count initially', () => {
    expect(service.unreadCount()).toBe(0);
  });
});
