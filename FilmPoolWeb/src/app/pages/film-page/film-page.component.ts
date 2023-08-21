import { Component, NgModule, OnInit, ViewChild } from "@angular/core";
import { Genre } from "src/app/enums/genre.enum";
import { Film } from "src/app/models/film.model";
import { FilmsService } from "src/app/services/films.service";
import { FilmFilterComponent } from "../film/filmFilter.component";
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from "src/app/services/authentication.service";
import { ActivatedRoute } from "@angular/router";
import { FileModel } from "src/app/models/file.model";
import { FilmCommentsComponent } from "../comments/comment.component";
import { Comment } from 'src/app/models/comment.model';
import { HeaderComponent } from "../header/header.component";

@Component({
    selector: '.app-filmpool-film-page',
    templateUrl: './film-page.component.html',
    styleUrls: ['./film-page.component.css'],

})



export class FilmComponent implements OnInit {
    film!: Film;
    idInt = 0
    fileToUpload: File | null | undefined;
    url: any;
    isAdmin: boolean | undefined;
    editMode: boolean | undefined;
    filmcontent!: FormGroup;
    rating: number | undefined;
    comment!: Comment;
    @ViewChild('filmComments') child: FilmCommentsComponent | undefined;
    @ViewChild('filmHeasers') secchild: HeaderComponent | undefined;

    constructor(
        private _filmsService: FilmsService,
        private _authService: AuthenticationService,
        private route: ActivatedRoute
    ) { }


    ngOnInit() {
        this.isAdmin = this._authService.isUserAdmin();
        this.editMode = false;
        this.filmcontent = new FormGroup({
            title: new FormControl(""),
            fileInput: new FormControl(''),
            year: new FormControl(''),
            description: new FormControl(''),
            duration: new FormControl(''),

        })
        this.route.params.subscribe(params => {
            const id = params['id'];
            if (id) {
                this.idInt = parseInt(id)
                this._filmsService.getFilm(this.idInt).subscribe(data => {
                    this.film = data.film;
                    this.rating = data.rating;
                    this.url = this.film.picture?.length > 0 ? `data:image/jpg;base64,${this.film.picture}` : "/assets/nofilm.png";
                }
                )
            }
        })
    }

    onSelectFile(event: any) {
        //const target= event.target as HTMLInputElement;
        //this.fileToUpload = (target.files as FileList)[0];
        //this.fileToUpload = files.item(0);
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

    addComment = (event: any) => {
        this.comment = event;
    }

    genreConvention = (genre: string) => {
        return Number(genre) in Genre ? Genre[Number(genre)] : undefined;
    }

    updateFilm = (filmValue: any) => {
        this.enableEditMode(); 
        const filmInfo = { ...filmValue };

        const filmUpdateInfo: Film = {
            id: this.film.id,
            title: filmInfo.title,
            year: filmInfo.year,
            description: filmInfo.description,
            duration: filmInfo.duration,
            genre: filmInfo.genre,
            rating: 0,
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
            this._filmsService.picture(this.idInt, form)
                .subscribe()
        }
        this._filmsService.updateFilm(filmUpdateInfo).subscribe()

    }
}