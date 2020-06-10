import { Component, OnInit} from '@angular/core';
import { UserService } from '../services/account/user.service';
import { AccUser } from '../models/acc-user';


@Component({
  selector: 'app-account-component',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit{

  constructor(private userService: UserService) { }

  user: AccUser;

  ngOnInit(): void {
    this.userService.GetUser().subscribe((result: AccUser) => (this.user = result));
  }


}
