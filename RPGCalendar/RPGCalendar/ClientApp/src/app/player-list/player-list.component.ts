import { Component, OnInit, } from '@angular/core';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})


export class PlayerListComponent implements OnInit {
  headers: any;

  rows: any;

  constructor() { 
    this.headers = ['ID', 'Name', 'Class', 'Bio'];

    this.rows = [
      {
        "ID" : "1",
        "Name" : "Kalipso",
        "Class" : "Rogue",
        "Bio" : "Dark past, need to be a downer all the time"
      },
      {
        "ID" : "2",
        "Name" : "Clifficus",
        "Class" : "Dwarf",
        "Bio" : "Smash! and Wobble, thats my gig"
      },
      {
        "ID" : "3",
        "Name" : "Ni",
        "Class" : "Knight",
        "Bio" : "We are the knights of Ni!"
      },
      {
        "ID" : "4",
        "Name" : "Merlin",
        "Class" : "Wizard",
        "Bio" : "totally not crazy, magic is real trust me"
      }
    ];
  }
  
  ngOnInit() {
  }
//Comment
}