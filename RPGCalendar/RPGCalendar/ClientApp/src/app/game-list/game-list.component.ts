import { Component, OnInit } from '@angular/core';
import { readdir } from 'fs';
import { Router } from  '@angular/router';

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css']
})
export class GameListComponent implements OnInit {
  headers: any;

  rows: any;

  aheaders: any;

  arows: any;

  condition: any;
  
  constructor(private router: Router) {
    this.condition ={
      availGames : false
    };

    this.headers = ['Title', 'Game Master', 'Num. of Players'];

    this.rows = [
      {
        "Title" : "D&D",
        "Game Master" : "Kalipso",
        "Num. of Players" : "4",
      },
      {
        "Title" : "GloomHaven",
        "Game Master" : "Clifficus",
        "Num. of Players" : "3",
      },
      {
        "Title" : "Mice & Mystics",
        "Game Master" : "Ni",
        "Num. of Players" : "5",
      }
    ];
    
    this.aheaders = ['Title', 'Game Master', 'Select'];

    this.arows = [
      {
        "Title" : "Possible Game 1", 
        "Game Master" : "Random Dude",
        "Select" : '<input type="checkbox">'
      },
      {
        "Title" : "Possible Game 2", 
        "Game Master" : "Random Dudett",
        "Select" : "<input type='checkbox'>"
      },
      {
        "Title" : "Possible Game 3", 
        "Game Master" : "Random Persona",
        "Select" : "<input type='checkbox'>"
      }
    ];

   }

  ngOnInit() {
  }

  createGame(){
    this.router.navigateByUrl('./gameoverview');
  }

}
