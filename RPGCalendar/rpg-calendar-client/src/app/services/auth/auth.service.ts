import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Registration } from '../../models/registration';
import { User } from '../../models/user';
import { Observable, BehaviorSubject } from  'rxjs';
import { Login } from 'src/app/models/login';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private httpClient: HttpClient) { }

  register(registration: Registration): Observable<User> {
    return this.httpClient.post<User>('api/Registration', registration);
  }
  // register(user: User): Observable<JwtResponse> {
  //   return this.httpClient.post<JwtResponse>(`${this.AUTH_SERVER}/Registration`, user).pipe(
  //     tap((res:  JwtResponse ) => {

  //       if (res.user) {
  //         localStorage.set("ACCESS_TOKEN", res.user.access_token);
  //         localStorage.set("EXPIRES_IN", res.user.expires_in);
  //         this.authSubject.next(true);
  //       }
  //     })

  //   );
  // }

  signIn(login: Login): Observable<User> {
    return this.httpClient.post<User>(`api/Login`, login);
  }

  newPassword(email: string) : Observable<any>{
    return this.httpClient.post('api/reset', email);
  }

  logout() : Observable<any>{
    return this.httpClient.get('api/logout');
  }

  

  // signOut() {
  //   localStorage.removeItem("ACCESS_TOKEN");
  //   localStorage.removeItem("EXPIRES_IN");
  //   this.authSubject.next(false);
  // }

  // isAuthenticated() {
  //   return  this.authSubject.asObservable();
}
  
