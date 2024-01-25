import { Component } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators, AbstractControl, ValidatorFn, ValidationErrors, AsyncValidator, AsyncValidatorFn} from '@angular/forms'
import { LoginService } from 'src/Services/login.service';
import { User } from '../Models/User.model';
import { debounceTime, map } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  constructor (private loginservice: LoginService, private fb: FormBuilder, private router: Router){}

  register:boolean = false;

  LoginForm = new FormGroup({
    Username: new FormControl<string>(''),
    Password: new FormControl<string>(''),
    Email: new FormControl<string>(''),
  })

  RegisterForm = this.fb.nonNullable.group({
    username: new FormControl('',[Validators.required, Validators.minLength(5)], [CustomValidator.UsernameAvailable(this.loginservice)]),
    password: new FormControl('',[Validators.required, Validators.minLength(5)]),
    verifypassword: new FormControl('',[Validators.required]),
    email: new FormControl('',[Validators.required, Validators.email]),
    verifyemail: new FormControl('',[Validators.required]),
  },{
    updateOn:'blur',
    validators:[CustomValidator.Password(), CustomValidator.EmailMatch()],
  })

  Register(){
    this.register = !this.register;
  }

  RegisterUser():void{
    console.log("register");
    let username = this.RegisterForm.value.username!;
    let password = this.RegisterForm.value.password!;
    let email = this.RegisterForm.value.email!;
    let user:User = {
      Id: '00000000-0000-0000-0000-000000000000',
      Username: username,
      Password: password,
      Email: email,
    }
    console.log(user.Username);
    this.loginservice.CreateUser(user).subscribe({
      next: (user) => console.log(user)
    })
  }

  Login(): void{
    let username = this.LoginForm.value.Username as string;
    let password = this.LoginForm.value.Password as string;
    let email = '';
    let user:User = {
        Id: '00000000-0000-0000-0000-000000000000',
        Username: username,
        Password: password,
        Email: email,
    }
    this.loginservice.Login(user).subscribe(response => {localStorage.setItem('token', JSON.stringify(response)); this.router.navigate(['Index'])});
    console.log("login");
  }

  Clear(){
    localStorage.clear();
  }
}

export class CustomValidator{
  static Password():ValidatorFn {
    return (control: AbstractControl) => {
      const username = control.get('username')?.value;
      const password = control.get('password')?.value;
      const verifypassword = control.get('verifypassword')?.value;
      if(password != verifypassword || password == username){
        return {passwordsMatch: true}
      }
      else{
        return null;
      }
    }
  }

  static EmailMatch():ValidatorFn {
    return (control: AbstractControl) => {
      const email = control.get('email')?.value;
      const verifyEmail = control.get('verifyemail')?.value;
      if(email != verifyEmail){
        return {emailsMatch: true}
      }
      else{
        return null;
      }
    }
  }

  static UsernameAvailable(loginservice: LoginService):AsyncValidatorFn {
    return (control: AbstractControl) => {
      const username:string = control.value;
      return loginservice.GetUser(username).pipe(
        debounceTime(500),
        map((result:Boolean) =>
          result ? {usernameAlreadyExists: true} : null
        )
      )
    }
  }
}