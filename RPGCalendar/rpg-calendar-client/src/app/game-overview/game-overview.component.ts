import { Component, OnInit } from '@angular/core';
//import {NoteComponent} from '../modals/note';
import { MatDialog } from '@angular/material/dialog';
import { NoteComponent } from '../modals/note/note.component';



@Component({
  selector: 'app-game-overview',
  templateUrl: './game-overview.component.html',
  styleUrls: ['./game-overview.component.css']
})
export class GameOverviewComponent implements OnInit {
  constructor(public dialog: MatDialog) {}


  openDialog() {
    this.dialog.open(NoteComponent, {
    });
  }

  ngOnInit() {
  }

}//end GameOverviewComponent
