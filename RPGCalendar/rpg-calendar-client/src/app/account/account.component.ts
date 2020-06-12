import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { ItemService } from '../services/item/item.service';
import { Player } from '../models/player';
import { Item } from '../models/item';
import { MatDialog } from '@angular/material/dialog';
import { ItemComponent } from '../modals/item/item.component';

@Component({
  selector: 'app-account-component',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css'],
})
export class AccountComponent implements OnInit {
  user: Player;
  userItems: Item[];

  headers: string[];
  constructor(
    private userService: UserService,
    private itemService: ItemService,
    public dialog: MatDialog
  ) {
    this.headers = [
      'ID',
      'Name',
      'Description',
      'Quantity',
      'Quality',
      'Option',
    ];
  }

  ngOnInit(): void {
    this.userService
      .GetSelectedUser()
      .subscribe((result: Player) => this.initItems(result));
  }

  initItems(result: Player): void {
    this.user = result;
    this.itemService.GetUserItems(this.user.id).subscribe((result: Item[]) => {
      this.userItems = result;
      console.log('result', result);
    });
  }

  openDialog() {
    const dialogRef = this.dialog.open(ItemComponent, {
      data: this.user.id,
    });

    dialogRef.afterClosed().subscribe(() => this.initItems(this.user));
  }

  deleteItem(itemID: number): void {
    this.itemService
      .DeleteItem(itemID)
      .subscribe(() => this.initItems(this.user));
  }
}
