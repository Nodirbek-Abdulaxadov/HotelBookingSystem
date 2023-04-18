import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Constants } from 'src/app/models/constan';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  constructor(private httpClient: HttpClient, 
              private router: Router,
              private toastr: ToastrService) {}

  baseUrl: string =  Constants.BASE_URL + 'Authentication';

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
          this.toastr.success('Please login now!', 'Successfully registered');
          this.router.navigate(['/login']);
        },
        error: (error) => {
          const el = document.getElementById('error')!;
          console.log(error);
            switch(error.status) {
              case 401: {
                el.innerHTML = error.error;
              } break;
              case 400: {
                var arr = error.error.errors;
                el.innerHTML = "";
                if (arr['FirstName']) {
                  el.innerHTML += arr['FirstName'] + '<br/>';
                }
                if (arr['LastName']) {
                  el.innerHTML += arr['LastName'] + '<br/>';
                }
                if (arr['PhoneNumber']) {
                  el.innerHTML += arr['PhoneNumber'] + '<br/>';
                }
                if (arr['Password']) {
                  el.innerHTML += arr['Password'] + '<br/>';
                }
                if (arr['ConfirmPassword']) {
                  el.innerHTML += arr['ConfirmPassword'] + '<br/>';
                }
              }break;
              case 500:
              case 0: this.router.navigate(['/server-error']); break;
            }
        },
      });
  }
}
