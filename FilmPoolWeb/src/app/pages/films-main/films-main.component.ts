import { Component, HostListener, NgModule, OnInit, ViewChild } from "@angular/core";
import { Genre } from "src/app/enums/genre.enum";
import { Film } from "src/app/models/film.model";
import { FilmsService } from "src/app/services/films.service";
import { FilmFilterComponent } from "../film/filmFilter.component";
import { FormGroup, FormControl, Validators, RangeValueAccessor } from '@angular/forms';
import { AuthenticationService } from "src/app/services/authentication.service";
import { ActivatedRoute, Router } from "@angular/router";
import { RatingService } from "src/app/services/rating.service";
import { Rating } from "src/app/models/rating.model";
import { HeaderComponent } from "../../shared/header/header.component";

@Component({
    selector: '.app-filmpool-main-page',
    templateUrl: './films-main.component.html',
    styleUrls: ['./films-main.component.css'],

})



export class FilmsListComponent implements OnInit {
    films!: Film[];
    isAdmin = false;
    config: any;
    pageSize = 3;
    currentPage = 1;
    searchText = '';
    getScreenHeight!: number
    max = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    rating = false;
    year!: number;
    genre: Genre | undefined;
    @ViewChild('filmFilter') child: FilmFilterComponent | undefined;
    @ViewChild('filmHeasers') secchild: HeaderComponent | undefined;
    constructor(
        private _filmsService: FilmsService,
        private _ratingService: RatingService,
        private _authService: AuthenticationService,
        private router: Router,
        private route: ActivatedRoute

    ) { }


    ngOnInit() {
        this.isAdmin = this._authService.isUserAdmin();
        this.getScreenHeight = window.innerHeight;
        this.determinePageSize(this.getScreenHeight);
    }

    @HostListener('window:resize', ['$event'])
    onWindowResize() {
        this.getScreenHeight = window.innerHeight;
        this.determinePageSize(this.getScreenHeight);
    }

    getFilms() {
        this._filmsService.getFilms(this.pageSize, this.currentPage, this.year, this.searchText, this.genre, this.rating).subscribe(data => {
            this.films = data.films.map(i => {
                i.picture = i.picture?.length > 0 ? `data:image/jpg;base64,${i.picture}` : "/assets/nofilm.png";
                return i
            });
            this.config = {
                id: 'basicPaginate',
                itemsPerPage: this.pageSize,
                currentPage: this.currentPage ? this.currentPage : 1,
                totalItems: data.totalFilms
            }
        })
    }

    determinePageSize(size: number) {
        if (size > 1100 && size <= 1400) {
            this.pageSize = 4
        }
        if (size <= 1100 && size > 800) {
            this.pageSize = 3
        }
        if (size <= 800) {
            this.pageSize = 2
        }

        this.getFilms();
    }

    pageChanged(event: any) {
        this.config.currentPage = event;
        this.currentPage = event;
        this.getFilms();
    }

    onValueChanged(event: any) {
        this.getFilms();
    }

    toFilm(id: number) {
        this.router.navigate(['film', id], {
        });
    }

    filterGenre = (event: any) => {
        this.genre = event;
        this.getFilms();
    }

    filterYear = (event: any) => {
        this.year = event;
        this.getFilms();
    }

    toggleRating = (i: number, filmId: number) => {
        let userId = localStorage.getItem("userId");
        let uid = userId && userId?.length > 0 ? +userId : 0;
        const rating = {
            id: 0,
            filmId,
            userId: uid,
            score: i,
        }
        this._ratingService.putRating(rating).subscribe();

        this.getFilms();
    }

    genreConvention = (genre: string) => {
        return Number(genre) in Genre ? Genre[Number(genre)] : undefined;
    }

    ratingSort = () => {
        this.rating = !this.rating;
        this.getFilms();
    }
}
