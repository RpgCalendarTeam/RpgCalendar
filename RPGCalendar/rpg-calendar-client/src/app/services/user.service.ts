import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Player } from '../models/player';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private httpClient: HttpClient) {}
  private actionUrl = 'api/User';

  selectedUserId: number;
  SetUserId(id: number) {
    this.selectedUserId = id;
  }
  GetSelectedUser(): Observable<Player> {
    if (this.selectedUserId) {
      return this.httpClient.get<Player>(
        this.actionUrl + '/' + this.selectedUserId
      );
    }
  }

  GetCurrentUser(): Observable<Player> {
    return this.httpClient.get<Player>(this.actionUrl);
  }

  GetPlayersForGame(): Observable<Player[]> {
    return this.httpClient.get<Player[]>(this.actionUrl + '/all');
  }
}
