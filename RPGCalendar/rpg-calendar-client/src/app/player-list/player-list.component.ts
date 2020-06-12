import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Player } from '../models/player';
import { Router } from '@angular/router';
@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css'],
})
export class PlayerListComponent implements OnInit {
  players: Player[];
  headers: string[];
  gameMasterclass = 'Game Master';
  constructor(private userService: UserService, private router: Router) {
    this.headers = ['ID', 'Name', 'Class', 'Bio', 'View'];
  }

  ngOnInit() {
    this.userService
      .GetPlayersForGame()
      .subscribe((result: Player[]) => (this.players = result));
  }

  setUserId(id: number): void{
    this.userService.SetUserId(id);
    this.router.navigate(['account']);
  }

}
