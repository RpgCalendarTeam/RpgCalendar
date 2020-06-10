import { Component, OnInit } from '@angular/core';
import { Calendar } from '../models/calendar';
import { GameService } from '../services/game/game.service';
import { Game } from '../models/game';

@Component({
  selector: 'app-game-calendar',
  templateUrl: './game-calendar.component.html',
  styleUrls: ['./game-calendar.component.css'],
})
export class GameCalendarComponent implements OnInit {
  constructor(private gameService: GameService) {}
  calendar: Calendar;

  ngOnInit() {
    this.gameService
      .GetSessionGame()
      .subscribe((result: Game) => (this.calendar = result.calendar));
  }
}
