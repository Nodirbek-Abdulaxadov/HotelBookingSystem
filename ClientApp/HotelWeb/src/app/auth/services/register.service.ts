import { HttpClient } from '@angular/common/http';
import { TagContentType } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  constructor(private httpClient: HttpClient, private router: Router) {}

  baseUrl: string = 'https://localhost:44363/api/Authentication';

  register(form: any): void {
    var user = {
      FirstName: form.firstName,
      LastName: form.lastName,
      Password: form.password,
      ConfirmPassword: form.confirmPassword,
      PhoneNumber: form.phoneNumber,
      Email: form.email,
      UserRole: 'Guest'
    };
    console.log(user);
    this.httpClient
      .post(this.baseUrl + '/register-guest', user)
      .subscribe({
        next: (data) => {
          this.router.navigate(['/login']);
        },
        error: (error) => {
          console.error('There was an error!', error);
        },
      });
  }
}
