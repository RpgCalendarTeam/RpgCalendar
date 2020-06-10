import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Game } from '../models/game';
import { GameService } from '../services/game/game.service';
import { NgForm } from '@angular/forms';
import { PlayerInfo } from '../models/player-info';

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css'],
})
export class GameListComponent implements OnInit {
  @ViewChild('addGameForm', { static: false }) addGameForm: NgForm;
  headers = ['Id', 'Title', 'Game System', 'Description', 'Select'];
  gameId: number;
  condition: any;
  private games: Game[];
  constructor(private router: Router, private gameService: GameService) {
    this.condition = {
      availGames: false,
    };
  }

  addGame() {
    if (this.addGameForm.valid) {
      this.gameService
        .AddGame(this.addGameForm.value.gameId, {
          bio: this.addGameForm.value.playerBio,
          class: this.addGameForm.value.playerClass,
        } as PlayerInfo)
        .subscribe(() => this.router.navigate(['gameoverview']));
    }
  }

  selectGame(gameId: number) {
    console.log('Submitted game id: ' + gameId);
    this.gameService.GetGame(gameId).subscribe((result: Game) => {
      this.router.navigate(['gameoverview']);
    });
  }
  ngOnInit() {
    this.gameService
      .GetAllGames()
      .subscribe((result: Game[]) => (this.games = result));
  }

  createGame() {
    this.router.navigate(['game-create']);
  }
}
