import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CalendarService {
  constructor(private httpClient: HttpClient) {}
}

//   getCalendar() : Observable<Calendar> {
//     return this.httpClient.get('api/logout');
//   }

//   getMonthName() : string {
//     return this.monthName;
//   }

// }
