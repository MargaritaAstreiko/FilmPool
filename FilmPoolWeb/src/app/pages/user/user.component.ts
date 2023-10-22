import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UsersService } from 'src/app/services/users.service';
import { HeaderComponent } from 'src/app/shared/header/header.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  editMode: boolean | undefined;
  usercontent!: FormGroup;
  user!: User;
  userId = localStorage.getItem("userId") || 0;
  fileToUpload: File | null | undefined;
  url!: any;
  @ViewChild('filmHeaders') secchild: HeaderComponent | undefined;

  constructor(
    private authService: AuthenticationService,
    private _usersService: UsersService
  ) { }

  ngOnInit() {
    this.editMode = false;
    this.usercontent = new FormGroup({
      firstName: new FormControl(""),
      lastName: new FormControl(''),
      userName: new FormControl(""),
      email: new FormControl(''),
    })
    this.authService.getUserInfo(+this.userId).subscribe(data => {
      this.user = data;
      this.url = this.user.picture?.length > 0 ? `data:image/jpg;base64,${this.user.picture}` : "/assets/nofilm.png";
    });

  }

  onSelectFile(event: any) {

    this.fileToUpload = event.target.files[0]
    if (this.fileToUpload?.name) {

      const fileReader: FileReader = new FileReader();
      fileReader.readAsDataURL(this.fileToUpload);

      fileReader.onload = (event: any) => {
        this.url = event.target.result;
      };
    }

  };

  enableEditMode() {
    this.editMode = !this.editMode;
  }

  updateUser = (userContentValue: any) => {
    this.enableEditMode();
    const userInfo = { ...userContentValue };

    const userUpdateInfo: User = {
      id: +this.userId,
      firstName: userInfo.firstName,
      lastName: userInfo.firstName,
      userName: userInfo.userName,
      email: userInfo.email,
      password: this.user.password,
      userRole: this.user.userRole,
      role: this.user.role,
      picture: ''
    }

    if (this.fileToUpload?.name) {

      const fileReader: FileReader = new FileReader();
      fileReader.readAsDataURL(this.fileToUpload);

      fileReader.onload = (event: any) => {
        this.url = event.target.result;
      };
      let files = []
      files.push({ data: this.fileToUpload, fileName: this.fileToUpload.name });
      let form = new FormData();
      form.append("file", this.fileToUpload);
      this._usersService.picture(+this.userId, form)
        .subscribe()
    }
    // this._filmsService.updateFilm(filmUpdateInfo).subscribe()

  }
}