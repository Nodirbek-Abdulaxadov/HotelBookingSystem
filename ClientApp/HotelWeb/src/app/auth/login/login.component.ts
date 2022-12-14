import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private loginService: LoginService,
              private formBuilder: FormBuilder){}

  email = new FormControl('', [
    Validators.required,
    Validators.email
  ]);
  password = new FormControl('', [
    Validators.required,
    Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%.#*?&])[A-Za-z\d$@.#$!%*?&].{7,}'),
    Validators.minLength(8),
    Validators.maxLength(25)
  ]);

  public loginForm = this.formBuilder.group({
    email: this.email,
    password: this.password
  });



  onSubmit() {
    this.loginService.loginUser(this.loginForm.getRawValue());
    }
}