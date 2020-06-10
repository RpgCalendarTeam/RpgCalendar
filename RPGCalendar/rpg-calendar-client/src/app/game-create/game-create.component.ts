import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-game-create',
  templateUrl: './game-create.component.html',
  styleUrls: ['./game-create.component.css'],
})
export class GameCreateComponent implements OnInit {
  @ViewChild('createGameForm', { static: false }) createGameForm: NgForm;
  gameForm;
  constructor() {}

  ngOnInit() {}

  submitGameForm() {}
}
