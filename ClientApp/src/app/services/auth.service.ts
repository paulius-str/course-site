import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NotifierService } from 'angular-notifier';
import { environment } from 'src/environments/environment';
import { ILoginDto } from '../models/loginDto';
import { ITokenResponse } from '../models/tokenResponse';
import { IUser } from '../models/user';
import { IUserRegisterDto } from '../models/userRegisterDto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  jwtHelper: JwtHelperService = new JwtHelperService();
  appUser: IUser | null;
  token: any;

  constructor(private http: HttpClient, private router: Router, private notifierService: NotifierService) { 
    this.token = localStorage.getItem('token');
    if(this.token){
      this.appUser = this.readClaims();
      this.getUser();
    }
  }
  
  login(loginDto: ILoginDto){
    console.log(loginDto);
    this.http.post<ITokenResponse>(environment.baseUrl + 'auth/login', loginDto).subscribe(response => {
        localStorage.setItem('token', response.token);
        this.token = response.token;
        this.appUser = this.jwtHelper.decodeToken<IUser>(response.token);
        //this.appUser = this.readClaims();
        this.router.navigate(['/']);
        this.notifierService.notify('success', 'Logged In!');

        if(this.token){
          this.appUser = this.readClaims();
          this.getUser();
        }

        return(this.token);

    }, error => {   
        console.log(error);
        return null;
    });
  }

  getUser(){
    this.http.get<IUser>(environment.baseUrl + 'users/' + this.appUser?.id).subscribe(response => {
      this.appUser = response;
    }, error => {
      console.log(error);
    })
  }

  becomePublisher(){
    this.http.post(environment.baseUrl + 'users/makepublisher/' + this.appUser?.id, {}).subscribe(response => {
      this.getUser();
    });
  }

  logout(){
    localStorage.removeItem('token');
    this.appUser = null;
    this.router.navigate(['/']);
  }

  register(registerDto: IUserRegisterDto){
    console.log(registerDto)
    this.http.post<ITokenResponse>(environment.baseUrl + 'auth/register', registerDto).subscribe(response => {
      localStorage.setItem('token', response.token);
        this.token = response.token;
        this.appUser = this.jwtHelper.decodeToken<IUser>(response.token);
        //this.appUser = this.readClaims();
        this.router.navigate(['/']);
        this.notifierService.notify('success', 'Logged In!');

        if(this.token){
          this.appUser = this.readClaims();
          this.getUser();
        }

        return(this.token);
  }, error => {
      console.log(error);
  });
  }

  loggedIn(){
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token?.toString());
  }

  private readClaims(){
    const claims = this.jwtHelper.decodeToken(this.token);
    console.log(claims.firstName);
    return {id: claims.nameid, emailAddress: claims.email, firstName: claims.unique_name, lastName: claims.family_name}
  }
}
