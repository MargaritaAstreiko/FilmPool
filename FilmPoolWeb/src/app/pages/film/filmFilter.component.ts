import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output } from '@angular/core';
import { Genre } from 'src/app/models/genre.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { GenresService } from 'src/app/services/genres.service';

@Component({
  selector: 'app-filmFilter',
  templateUrl: './filmFilter.component.html',
  styleUrls: ['./filmFilter.component.css']
})
export class FilmFilterComponent implements OnInit {
  genres!: Genre[];
  genreSelected!: number;
  years!: number[];
  yearSelected!: number;
  isAdmin=false;

  @Output() genre = new EventEmitter<Genre>();
  @Output() year= new EventEmitter<number>();
  constructor(
      private _genresService: GenresService,
      private _authService: AuthenticationService,
  ) {}
  

  ngOnInit() {
    this.isAdmin=this._authService.isUserAdmin();
     this._genresService.getGenres().subscribe( data=>{
      this.genres=data.filter(i=>i.id!==14);
      this.years=[];
      this.years.push(new Date().getFullYear(), new Date().getFullYear()-1,new Date().getFullYear()-2)
     });
  }

  getGenre=(genre:Genre)=>{
    this.genre.emit(genre);
    this.genreSelected=genre.id;
  }


  getYear=(year:number)=>{
    this.year.emit(year);
    this.yearSelected=year;
  }

  clean() {
    this.genreSelected = 0;
    this.yearSelected = 0;
  }

}

