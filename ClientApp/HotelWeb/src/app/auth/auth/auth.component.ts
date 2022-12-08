import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {
    constructor(private router: Router){}

  IsLoggedIn(): boolean {
    var username = localStorage.getItem('username')
    if (username) {
      return true;
    }
    return false;
  }
  
  getUserName(): string {
    return localStorage.getItem('username')??"Error!";
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/']);
  }
}
