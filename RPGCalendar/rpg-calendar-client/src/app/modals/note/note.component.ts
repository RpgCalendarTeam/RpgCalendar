import { Component, OnInit, Inject } from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import { NotesService } from 'src/app/services/notes/notes.service';
import { Note } from 'src/app/models/note';



@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})

export class NoteComponent implements OnInit {
  notes: Note[];

  constructor(private noteService: NotesService) {}
  
  ngOnInit(): void {
    this.noteService.GetAllNotes().subscribe((result: Note[]) => (this.notes = result));
  }

}
