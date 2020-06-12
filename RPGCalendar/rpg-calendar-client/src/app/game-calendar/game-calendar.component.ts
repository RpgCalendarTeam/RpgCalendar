import { Component, OnInit, ViewChild } from '@angular/core';
import { Calendar } from '../models/calendar';
import { GameService } from '../services/game/game.service';
import { Game } from '../models/game';
import { NgForm } from '@angular/forms';
import { TimeInput } from '../models/inputs/time-input';
import { TimeService } from '../services/time/time.service';

@Component({
  selector: 'app-game-calendar',
  templateUrl: './game-calendar.component.html',
  styleUrls: ['./game-calendar.component.css'],
})
export class GameCalendarComponent implements OnInit {
  @ViewChild('progressTimeForm', { static: false }) progressTimeForm: NgForm;
  constructor(
    private gameService: GameService,
    private timeService: TimeService
  ) {}
  calendar: Calendar;
  time: boolean;
  ngOnInit() {
    this.time = false;
    this.getGame();
  }
  getGame() {
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
  addTime() {
    this.time = true;
  }
  submitTimeForm() {
    this.timeService
      .AddTime({
        seconds: this.progressTimeForm.value.seconds,
        minutes: this.progressTimeForm.value.minutes,
        hours: this.progressTimeForm.value.hours,
        days: this.progressTimeForm.value.days,
        years: this.progressTimeForm.value.years,
      } as TimeInput)
      .subscribe((game: Game) => {
        this.getGame();
        this.time = false;
        this.progressTimeForm.resetForm();
      });
  }
}
