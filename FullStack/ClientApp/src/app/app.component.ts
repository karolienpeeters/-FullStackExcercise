import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './_interfaces/user';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  
}
