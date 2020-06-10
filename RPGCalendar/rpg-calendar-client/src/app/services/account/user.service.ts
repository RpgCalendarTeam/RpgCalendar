import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccUser } from 'src/app/models/acc-user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }
  private actionUrl = 'api/User';

  GetUser(): Observable<AccUser>{
      return this.httpClient.get<AccUser>(this.actionUrl);
  }
}
