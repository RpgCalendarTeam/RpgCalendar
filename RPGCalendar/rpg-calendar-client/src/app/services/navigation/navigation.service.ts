import { Injectable, OnInit } from '@angular/core';
import { GameService } from '../game/game.service';
import { UserService } from '../user.service';

@Injectable({
  providedIn: 'root',
})
export class NavigationService implements OnInit {
  public isLoggedIn: boolean;
  public isGameInSession: boolean;
  constructor(
    private userService: UserService,
    private gameService: GameService
  ) {}
  ngOnInit(): void {
    this.update();
  }

  update() {
    this.checkLogIn();
    this.gameInSession();
  }
  GetLoggedIn(): boolean {
    return this.isLoggedIn;
  }
  GetGameInSession(): boolean {
    return this.isGameInSession;
  }
  checkLogIn() {
    this.userService.GetCurrentUser().subscribe(
      (res) => (this.isLoggedIn = true),
      (err) => (this.isLoggedIn = false),
      () => {}
    );
  }

  gameInSession() {
    this.gameService.GetSessionGame().subscribe(
      (res) => (this.isGameInSession = true),
      (err) => (this.isGameInSession = false),
      () => {}
    );
  }
}
