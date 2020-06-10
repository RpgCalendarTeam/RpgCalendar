import { Calendar } from './calendar';

export class Game {
    id: number;
    gameMaster: number;
    title: string;
    description: string;
    gameSystem: string;
    calendar: Calendar;
}
