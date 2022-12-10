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
    var fullName = localStorage.getItem('fullname')
    if (fullName) {
      return true;
    }
    return false;
  }
  
  getUserName(): string {
    return localStorage.getItem('fullname')??"Error!";
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/']);
  }
}
