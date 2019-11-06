import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { User } from '../interfaces/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  currentUser: User;

  constructor(private router: Router,private authService: AuthService) {

    this.authService.currentUser.subscribe(x => this.currentUser = x);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }


}
