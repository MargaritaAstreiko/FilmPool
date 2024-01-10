import { Component, NgModule, OnInit, ViewChild } from "@angular/core";
import { Genre } from "src/app/enums/genre.enum";
import { Film } from "src/app/models/film.model";
import { FilmsService } from "src/app/services/films.service";
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from "src/app/services/authentication.service";
import { ActivatedRoute } from "@angular/router";
import { HeaderComponent } from "../../shared/header/header.component";


@Component({
    selector: '.app-filmpool-film-new',
    templateUrl: './create-film.component.html',
    styleUrls: ['./create-film.component.css'],

})



export class FilmCreateComponent implements OnInit {
    film!: Film;
    idInt = 0
    fileToUpload: File | null | undefined;
    url: any;
    eGenre = Genre;
    genres = Object.keys(this.eGenre).filter(k => !isNaN(Number(k)));
    description ='';
    genre!: Genre;
    filmcontent!: FormGroup;
    @ViewChild('filmHeaders') secchild: HeaderComponent | undefined;
    userId = localStorage.getItem("userId") || 0;

    constructor(
        private _filmsService: FilmsService,

    ) { }


    ngOnInit() {
        this.filmcontent = new FormGroup({
            title: new FormControl(""),
            fileInput: new FormControl(''),
            year: new FormControl(''),
            duration: new FormControl(''),
            genre: new FormControl(''),
            filmUrl: new FormControl(''),

        })
        this.url = "/assets/nofilm.png";
        console.log(this.genres)

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

    genreConvention = (genre: Genre) => {
        return Number(genre) in Genre ? Genre[Number(genre)] : undefined;
    }

    createFilm = (filmValue: any) => {
        const filmInfo = { ...filmValue };

        const filmUpdateInfo: Film = {
            id: 0,
            title: filmInfo.title,
            year: filmInfo.year,
            description: this.description,
            duration: filmInfo.duration,
            genre: +filmInfo.genre,
            rating: 0,
            picture: '',
            filmUrl: filmInfo.filmUrl,
        }

        this._filmsService.createFilm(filmUpdateInfo).subscribe(data=>{
            this.idInt = data;
            
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
        } )

    }

}