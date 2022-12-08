import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'HotelWeb';
  emailTest: boolean = true;
  emailHelp: any;

  validateEmail(email: any): void {
    this.emailHelp = document.getElementById('emailHelp');
    if (email.value == '') {
      this.emailHelp.innerText = "Incorrect email address!";
    }
    if(email.value!=''){
      const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      console.log(re.test(String(email.value).toLowerCase()))
      this.emailTest = re.test(String(email.value).toLowerCase())
      if (this.emailTest != true) {
        this.emailHelp.innerText = "Incorrect email address!";
        return;
      }
      else {
        this.emailHelp.innerText = "";
          return;
      }
    }
  }
}
