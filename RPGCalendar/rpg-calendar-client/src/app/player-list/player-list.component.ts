import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Player } from '../models/player';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css'],
})
export class PlayerListComponent implements OnInit {
  players: Player[];
  headers: string[];
  gameMasterclass = 'Game Master';
  constructor(private userService: UserService) {
    this.headers = ['ID', 'Name', 'Class', 'Bio'];
  }

  ngOnInit() {
    this.userService
      .GetPlayersForGame()
      .subscribe((result: Player[]) => (this.players = result));
  }
}
