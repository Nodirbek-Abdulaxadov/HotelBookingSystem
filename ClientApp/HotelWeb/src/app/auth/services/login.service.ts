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
            localStorage.setItem("data", JSON.stringify(data));
            // const natification = document.getElementById('natification')!;
            // natification.style.display = 'block';
            //setTimeout(() => '', 3000);
            alert('Login successfully!');
            this.router.navigate(['/']);
        },
        error: error => {
          const el = document.getElementById('error')!;
          console.log(error);
            switch(error.status) {
              case 400: {
                var arr = error.error.errors;
                el.innerHTML = "";
                if (arr['Email']) {
                  el.innerHTML += arr['Email'] + '<br/>';
                }
                if (arr['Password']) {
                  el.innerHTML += arr['Password'] + '<br/>';
                }
              }break;
              case 401: el.innerHTML = error.error; break;
              case 0: this.router.navigate(['/server-error']); break;
            }
        }}
    );
  }
}