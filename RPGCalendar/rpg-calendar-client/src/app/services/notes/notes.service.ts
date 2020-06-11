import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Note } from 'src/app/models/note';

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  constructor(private httpClient: HttpClient) { }
  private actionUrl = 'api/Note';

  GetAllNotes(): Observable<Note[]>{
    return this.httpClient.get<Note[]>(this.actionUrl);
  }

  AddNote(note: Note): Observable<Note>{
    return this.httpClient.post<Note>(this.actionUrl, note);
  }

  UpdateNote(id: number, note: Note): Observable<Note>{
    return this.httpClient.put<Note>(this.actionUrl + '/' + id, note);
  }

  DeleteNote(id: number): Observable<any>{
    return this.httpClient.delete(this.actionUrl + '/' + id);
  }

}//end NoteService
