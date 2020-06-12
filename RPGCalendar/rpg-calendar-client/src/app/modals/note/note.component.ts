import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { NotesService } from 'src/app/services/notes/notes.service';
import { Note } from 'src/app/models/note';
import { NgForm } from '@angular/forms';




@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})

export class NoteComponent implements OnInit {
  @ViewChild('addNoteForm', {static: false}) addNoteForm: NgForm;
  notes: Note[];
  addDisplay: boolean;


  constructor(private noteService: NotesService,public dialogRef: MatDialogRef<NoteComponent>) {
  }
  
  flipBoolean(){
    this.addDisplay = true;
  }

  addNote(){
    if(this.addNoteForm.valid){
      this.noteService.AddNote({
        title: this.addNoteForm.value.noteTitle,
        text: this.addNoteForm.value.noteText,  
      } as Note).subscribe(() => (this.dialogRef.close()));
    }
  } 

  deleteNote(id: number){
    this.noteService.DeleteNote(id).subscribe(() => (this.initNotes()));
  }

  ngOnInit(): void {
    this.initNotes();
  }

  initNotes():void{
    this.noteService.GetAllNotes().subscribe((result: Note[]) => (this.notes = result));
    this.addDisplay = false;
  }

}
