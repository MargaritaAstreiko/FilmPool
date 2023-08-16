import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { UsersService } from 'src/app/services/users.service';
import { HeaderComponent } from '../header/header.component';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})

export class UserlistComponent implements OnInit {
  users!: User[];

  @ViewChild('filmHeaders') secchild: HeaderComponent | undefined;
  constructor(
      private _usersService: UsersService
  ) {}
  

  ngOnInit() {
     this._usersService.getUsers().subscribe( data=>{
      this.users=data;
     });
  }
}
