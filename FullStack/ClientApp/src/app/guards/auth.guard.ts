import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';


@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate() {
    console.log("auth guard activated")
    if (this.authService.currentUserValue != null && !this.authService.isTokenExpired()) {
      return true;
    }

    this.authService.logout();
    this.router.navigate(['/login']);
    return false;
  }
}
