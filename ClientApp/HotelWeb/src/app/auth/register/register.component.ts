import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { RegisterService } from '../services/register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  constructor(private formBuilder: FormBuilder,
              private registerService: RegisterService) {}
  password = new FormControl('', [
    Validators.required,
    Validators.pattern(
      '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%.#*?&])[A-Za-zd$@.#$!%*?&].{7,}'
    ),
    Validators.minLength(8),
    Validators.maxLength(25),
    this.matchValidator('confirmPassword', true),
  ]);

  email = new FormControl('', [
    Validators.required,
    Validators.email
  ]);

  confirmPassword = new FormControl('', [
    Validators.required,
    this.matchValidator('password'),
  ]);

  public registerForm = this.formBuilder.group({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    phoneNumber: new FormControl('', Validators.required),
    email: this.email,
    password: this.password,
    confirmPassword: this.confirmPassword,
    userRole: '',
  });

  onSubmit() {
    this.registerService.register(this.registerForm.getRawValue());
  }

  matchValidator(matchTo: string, reverse?: boolean): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (control.parent && reverse) {
        const c = (control.parent?.controls as any)[matchTo] as AbstractControl;
        if (c) {
          c.updateValueAndValidity();
        }
        return null;
      }
      return !!control.parent &&
        !!control.parent.value &&
        control.value === (control.parent?.controls as any)[matchTo].value
        ? null
        : { matching: true };
    };
  }
}
