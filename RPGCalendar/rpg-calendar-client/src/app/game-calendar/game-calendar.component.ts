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

  getMonthName(indexMod: number): string {
    const months = this.calendar.monthNames;
    const nextMonth = this.calendar.month + indexMod;
    return months[(nextMonth + months.length) % months.length];
  }

  getWeekName(indexMod: number): string {
    const days = this.calendar.dayNames;
    const currentDayOfWeek = days.indexOf(this.calendar.day);
    const nextDay = currentDayOfWeek + indexMod;
    return days[(nextDay + days.length) % days.length];
  }

  getDate(indexMod: number): number {
    const currentDate = this.calendar.date;
    const nextDate = currentDate + indexMod;
    if (nextDate < 0) {
      return null;
    }
    return nextDate;
  }
}
