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
    var json = localStorage.getItem("data");
    if (json) {
      return true;
    }
    return false;
  }
  
  getUserName(): string {
    var json = JSON.parse(localStorage.getItem("data")??"");
    var fullName = json["FullName"];
    
    return fullName;
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/']);
  }
}
