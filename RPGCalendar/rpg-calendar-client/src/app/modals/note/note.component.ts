import { Component, OnInit, Inject } from '@angular/core';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';


export interface NoteData {
  animal: 'panda' | 'unicorn' | 'lion';
}
@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})

export class NoteComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: NoteData) {}
  
  ngOnInit(): void {
  }

}
