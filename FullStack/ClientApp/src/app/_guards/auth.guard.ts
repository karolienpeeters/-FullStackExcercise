import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';


@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate() {
    if (this.authService.currentUserValue != null && !this.authService.isTokenExpired()) {
      return true;
    }

    this.authService.logout();
    this.router.navigate(['/login']);
    return false;
  }
}
