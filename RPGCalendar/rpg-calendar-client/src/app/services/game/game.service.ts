import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from 'src/app/models/game';
import { GameCreate } from 'src/app/models/game-create';
import { PlayerInfo } from 'src/app/models/player-info';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  constructor(private httpClient: HttpClient) {}
  private actionUrl = 'api/Game';

  GetAllGames(): Observable<Game[]> {
    return this.httpClient.get<Game[]>(this.actionUrl);
  }

  GetSessionGame(): Observable<Game> {
    return this.httpClient.get<Game>(this.actionUrl + '/currentgame');
  }
  GetGame(gameId: number): Observable<Game> {
    return this.httpClient.get<Game>(this.actionUrl + '/' + gameId);
  }

  AddGame(gameid: number, playerInfo: PlayerInfo): Observable<Game> {
    return this.httpClient.post<Game>(
      this.actionUrl + '/add/' + gameid,
      playerInfo
    );
  }
  CreateGame(game: GameCreate): Observable<Game> {
    return this.httpClient.post<Game>(this.actionUrl, game);
  }

  UpdateGame(gameId: number, game: GameCreate): Observable<Game> {
    return this.httpClient.put<Game>(this.actionUrl + '/' + gameId, game);
  }

  DeleteGame(gameId: number): Observable<any> {
    return this.httpClient.delete(this.actionUrl + '/' + gameId);
  }
}
