import { Component, OnInit} from '@angular/core';
import { UserService } from '../services/user.service';
import { ItemService } from '../services/item/item.service';
import { Player } from '../models/player';
import { Item } from '../models/item';


@Component({
  selector: 'app-account-component',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit{

  constructor(private userService: UserService, private itemService: ItemService) { }
   user: Player;
   userItems: Item[];
   

  ngOnInit(): void {
    this.userService.GetSelectedUser().subscribe((result: Player) => (this.initItems(result)));
  }

  initItems(result: Player): void{
    this.user = result;
    this.itemService.GetUserItems(this.user.id).subscribe((result: Item[]) => (this.userItems = result));
  }

}
