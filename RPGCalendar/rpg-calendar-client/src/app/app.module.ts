import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { AdminComponent } from './admin/admin.component';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AccountComponent } from './account/account.component';
import { RegisterComponent } from './register/register.component';
import { ComponentHelpComponent } from './component-help/component-help.component';
import { GameCalendarComponent } from './game-calendar/game-calendar.component';
import { GameOverviewComponent } from './game-overview/game-overview.component';
import { PlayerListComponent } from './player-list/player-list.component';
import { GameListComponent } from './game-list/game-list.component';
import { ForgotPasswordComponent} from './forgot-password/forgot-password.component';
import { ResponseResetComponent} from  './response-reset/response-reset.component';
import { EventsComponent } from './events/events.component';
import { HelpCompComponent } from './help-comp/help-comp.component';
import { GameCreateComponent } from './game-create/game-create.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AccountComponent,
    LoginComponent,
    AdminComponent,
    RegisterComponent,
    ComponentHelpComponent,
    HelpCompComponent,
    GameCalendarComponent,
    GameOverviewComponent,
    PlayerListComponent,
    GameListComponent,
    ForgotPasswordComponent,
    EventsComponent,
    ResponseResetComponent,
    GameCreateComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      //{ path: '', pathMatch: 'full', redirectTo: 'login'},
      { path: 'gameoverview', component: GameOverviewComponent },
      { path: 'account', component: AccountComponent },
      { path: 'login', component: LoginComponent },
      { path: 'admin', component: AdminComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'help', component: HelpCompComponent },
      { path: "gamelist", component: GameListComponent },
      { path: "forgot-password", component: ForgotPasswordComponent },
      {path: "event", component: EventsComponent},
      { path: "password-reset", component: ResponseResetComponent },
      { path: "game-create", component: GameCreateComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
