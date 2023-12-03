import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { UsersService } from 'src/app/services/users.service';
import { HeaderComponent } from '../../shared/header/header.component';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})

export class UserlistComponent implements OnInit {
  users!: User[];
  editMode: boolean | undefined;
  username!: string;
  user!: User;

  @ViewChild('filmHeaders') secchild: HeaderComponent | undefined;
  constructor(
    private _usersService: UsersService
  ) { }


  ngOnInit() {
    this._usersService.getUsers().subscribe(data => {
      this.users = data;
    });

  }

  enableEditMode() {
    this.editMode = !this.editMode;
  }


  fieldsChange=(user:User)=>{
    this.user = user;
    this.username = user.userName

  }

  block=(user:User)=>{
    this.username = '';
    this._usersService.blockUser(user.id).subscribe();
    this._usersService.getUsers().subscribe(data => {
      this.users = data;
    });

  }

  notblock=()=>{
    this.username = '';
    this._usersService.getUsers().subscribe(data => {
      this.users = data;
    });
  }
}
