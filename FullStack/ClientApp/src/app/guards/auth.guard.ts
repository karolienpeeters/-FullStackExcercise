import { Injectable } from '@angular/core';
import { CanActivate,  Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';


@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {
  
  constructor(private authService: AuthService, private router: Router) { }
  
  canActivate() {
    if (!this.authService.isTokenExpired() && this.authService.currentUserValue) {
      return true;
    }
    this.authService.logout();
    this.router.navigate(['/login']);
    return false;
  }
}
