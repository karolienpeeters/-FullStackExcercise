import { Injectable } from '@angular/core';
import { CanActivate,  Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';


@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {
  
  constructor(private authService: AuthService, private router: Router) { }
  
  canActivate() {
    if (!this.authService.isTokenExpired()) {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }

//   constructor(
//     private router: Router,
//     private authService: AuthService
// ) { }

// canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
//     const currentUser = this.authService.currentUserValue;
//     if (currentUser) {
//         // check if route is restricted by role
     
//         if (this.authService.isTokenExpired()) {
//           this.router.navigate(['/login'])
//           return false;
//             }

//         // authorised so return true
//         return true;
//     }

//     // not logged in so redirect to login page with the return url
//     this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
//     return false;
// }

}
