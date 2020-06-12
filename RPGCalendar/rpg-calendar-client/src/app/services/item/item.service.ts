import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Item } from 'src/app/models/item';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private httpClient: HttpClient) { }
  private actionUrl = 'api/Item';

  GetAllItems(): Observable<Item[]>{
    return this.httpClient.get<Item[]>(this.actionUrl);
  }

  GetUserItems(id: number): Observable<Item[]>{
    return this.httpClient.get<Item[]>(this.actionUrl + '/user/' + id);
  }

  AddItem(item: Item): Observable<Item>{
    return this.httpClient.post<Item>(this.actionUrl, item);
  }

  UpdateItem(id: number, item: Item): Observable<Item>{
    return this.httpClient.put<Item>(this.actionUrl + '/' +id, item);
  }

  DeleteItem(id: number): Observable<any>{
    return this.httpClient.delete(this.actionUrl + '/' + id);
  }

}//end Item Service
