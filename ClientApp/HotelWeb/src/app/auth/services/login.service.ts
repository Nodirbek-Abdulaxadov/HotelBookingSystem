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
            var fullname = Object.entries(data)[0][1];
            var phoneNumber = Object.entries(data)[1][1];
            localStorage.setItem('fullname', fullname.toString());
            localStorage.setItem('phoneNumber', phoneNumber.toString());
            const natification = document.getElementById('natification')!;
            natification.style.display = 'block';
            //setTimeout(() => '', 3000);
            this.router.navigate(['/']);
        },
        error: error => {
          const el = document.getElementById('error')!;
          console.log(error);
            switch(error.status) {
              case 400: {
                var arr = error.error.errors;
                el.innerHTML = "";
                if (arr['PhoneNumber']) {
                  el.innerHTML += arr['PhoneNumber'] + '<br/>';
                }
                if (arr['Password']) {
                  el.innerHTML += arr['Password'] + '<br/>';
                }
              }break;
              case 401: el.innerHTML = error.error; break;
              case 0: el.innerHTML = "Connection lost with server!"; break;
            }
        }}
    );
  }
}