import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Constants } from 'src/app/models/constan';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient: HttpClient,
              private router: Router,
              private toastr: ToastrService) { }

  baseUrl: string = Constants.BASE_URL + "Authentication"

  loginUser(form: any): void {
      this.httpClient.post(this.baseUrl+'/login-user', form, {withCredentials: true}).subscribe(
       { next: data => {
            localStorage.setItem("data", JSON.stringify(data));
            this.toastr.success('', 'Successfully logged in!');
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