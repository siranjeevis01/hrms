import { Directive, Input, OnInit, TemplateRef, ViewContainerRef, inject } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';

@Directive({ selector: '[hasRole]', standalone: true })
export class HasRoleDirective implements OnInit {
  private authService = inject(AuthService);
  private templateRef = inject(TemplateRef<unknown>);
  private viewContainer = inject(ViewContainerRef);

  @Input('hasRole') roles: string | string[] = '';

  ngOnInit(): void {
    const roleList = Array.isArray(this.roles) ? this.roles : [this.roles];
    if (roleList.length === 0 || roleList.some((r) => this.authService.hasRole(r))) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainer.clear();
    }
  }
}
