import { Directive, Input, OnInit, TemplateRef, ViewContainerRef, inject } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';

@Directive({ selector: '[hasPermission]', standalone: true })
export class HasPermissionDirective implements OnInit {
  private authService = inject(AuthService);
  private templateRef = inject(TemplateRef<unknown>);
  private viewContainer = inject(ViewContainerRef);

  @Input('hasPermission') permissions: string | string[] = '';

  ngOnInit(): void {
    const permList = Array.isArray(this.permissions) ? this.permissions : [this.permissions];
    if (permList.length === 0 || permList.some((p) => this.authService.hasPermission(p))) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainer.clear();
    }
  }
}
