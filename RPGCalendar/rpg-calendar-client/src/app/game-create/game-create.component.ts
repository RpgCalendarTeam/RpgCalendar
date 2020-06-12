import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MonthInput } from '../models/inputs/month-input';
import { GameInput } from '../models/inputs/game-input';
import { CalendarInput } from '../models/inputs/calendar-input';
import { GameService } from '../services/game/game.service';
import { Game } from '../models/game';
import { Router } from '@angular/router';

@Component({
  selector: 'app-game-create',
  templateUrl: './game-create.component.html',
  styleUrls: ['./game-create.component.css'],
})
export class GameCreateComponent implements OnInit {
  @ViewChild('gameDataForm', { static: false }) gameDataForm: NgForm;
  @ViewChild('calendarDataForm', { static: false }) calendarDataForm: NgForm;
  constructor(private gameService: GameService, private router: Router) {
    this.next = false;
    this.months = [];
    this.days = [];
  }
  gameInput: GameInput;
  calendarInput: CalendarInput;
  title: string;
  description: string;
  gameSystem: string;
  hourLength: number;
  dayLength: number;
  months: MonthInput[];
  days: string[];
  monthName: string;
  monthLength: number;
  dayName: string;
  next: boolean;
  month: boolean;
  day: boolean;

  ngOnInit() {}

  nextScreen() {
    if (this.saveGameData()) {
      this.next = true;
    }
  }
  prevScreen() {
    this.next = false;
  }

  submit() {
    if (this.months.length > 0 && this.days.length > 0) {
      if (this.saveCalendarData()) {
        this.gameService
          .CreateGame(this.createGameInput())
          .subscribe((result: Game) => this.router.navigate(['gameoverview']));
      }
    }
  }
  createGameInput() {
    this.gameInput.calendar = this.calendarInput;
    return this.gameInput;
  }
  public addMonth() {
    console.log('month name: ' + this.monthName);
    console.log('month length: ' + this.monthLength);
    this.months.push({
      name: this.calendarDataForm.value.monthName,
      lengthInDays: 0 + this.calendarDataForm.value.monthLength,
    } as MonthInput);
    this.monthName = '';
    this.monthLength = 0;
    this.month = false;
  }
  showMonth() {
    this.month = true;
  }

  public addDay() {
    this.days.push(this.calendarDataForm.value.dayName);
    this.dayName = null;
    this.day = false;
  }

  showDay() {
    this.day = true;
  }
  saveGameData() {
    console.log('game title: ' + this.gameDataForm.value.title);
    console.log('game description: ' + this.gameDataForm.value.description);
    console.log('game system: ' + this.gameDataForm.value.gameSystem);

    if (this.gameDataForm.valid) {
      this.gameInput = {
        title: this.gameDataForm.value.gameName,
        description: this.gameDataForm.value.description,
        gameSystem: this.gameDataForm.value.gameSystem,
        calendar: null,
      } as GameInput;
      return true;
    }
    return false;
  }

  saveCalendarData(): boolean {
    if (this.calendarDataForm.valid) {
      const hourLengthInSeconds: number =
        this.calendarDataForm.value.hourLength * 60;
      const dayLengthInSeconds: number =
        this.calendarDataForm.value.dayLength * hourLengthInSeconds;
      this.calendarInput = {
        hourLength: hourLengthInSeconds,
        dayLength: dayLengthInSeconds,
        months: this.months,
        daysOfWeek: this.days,
      } as CalendarInput;
      return true;
    }
    return false;
  }
}
