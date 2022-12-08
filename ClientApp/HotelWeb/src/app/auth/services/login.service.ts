import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient: HttpClient,
              private router: Router) { }

  baseUrl: string = "https://localhost:44363/api/Authentication"

  loginUser(form: any): void {
      this.httpClient.post(this.baseUrl+'/login-user', form, {withCredentials: true}).subscribe(
       { next: data => {
            var json = Object.entries(data)[0][1];
            localStorage.setItem('username', json.toString())
            this.router.navigate(['/']);
        },
        error: error => {
            console.error('There was an error!', error);
            console.error('There was an error!', error.message);
        }}
    );
  }
}