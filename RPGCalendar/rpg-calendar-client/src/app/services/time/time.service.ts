import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TimeInput } from 'src/app/models/inputs/time-input';
import { Observable } from 'rxjs';
import { Game } from 'src/app/models/game';

@Injectable({
  providedIn: 'root',
})
export class TimeService {
  constructor(private httpClient: HttpClient) {}

  actionUrl = '/api/Calendar/Time';
  AddTime(timeInput: TimeInput): Observable<Game> {
    return this.httpClient.put<Game>(this.actionUrl, timeInput);
  }
}
