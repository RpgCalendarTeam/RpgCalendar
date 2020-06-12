import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { GameService } from '../services/game/game.service';
import { NavigationService } from '../services/navigation/navigation.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent implements OnInit {
  constructor(
    private navigationService: NavigationService,
    private router: Router
  ) {
    this.router.events.subscribe(() => {
      this.navigationService.update();
      this.isLoggedIn = this.navigationService.GetLoggedIn();
      this.isGameInSession = this.navigationService.GetGameInSession();
    });
  }
  isExpanded = false;
  isLoggedIn = this.navigationService.isLoggedIn;
  isGameInSession = this.navigationService.isGameInSession;
  ngOnInit(): void {
    this.navigationService.update();
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
