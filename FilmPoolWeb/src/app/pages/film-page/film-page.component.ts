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
import { HeaderComponent } from "../../shared/header/header.component";
import { CollectionsService } from "src/app/services/collections.services";
import { Collection } from "src/app/models/collection.model";
import { VideoPlayerComponent } from "src/app/shared/video-player/video-player.component";
import { FilmToCollection } from 'src/app/models/filmAddToCollection.model';

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
    collections!: Collection[];
    collection!: Collection;
    addToCollection=false;
    @ViewChild('filmComments') child: FilmCommentsComponent | undefined;
    @ViewChild('filmVideoPlayer') thchild: VideoPlayerComponent| undefined;
    @ViewChild('filmHeaders') secchild: HeaderComponent | undefined;
    userId = localStorage.getItem("userId") || 0;

    constructor(
        private _filmsService: FilmsService,
        private _authService: AuthenticationService,
        private _collectionsService: CollectionsService,
        private route: ActivatedRoute
    ) { }


    ngOnInit() {
        this.isAdmin = this._authService.isUserAdmin();
        this.editMode = false;
        this.filmcontent = new FormGroup({
            title: new FormControl(""),
            fileInput: new FormControl(''),
            year: new FormControl(''),
            duration: new FormControl(''),

        })
        this._collectionsService.getCollection(+this.userId).subscribe(data =>{
            this.collections=data;
            this.collection=this.collections[0];
          });
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
    addFilmToCollection=()=>{
        this.addToCollection=!this.addToCollection;
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
            description: this.film.description,
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

    saveToCollection = () => {
        const FilmToCollection: FilmToCollection={
          collectionId: this.collection.id,
          filmId:this.film.id,
          addededDate: new Date()
        }
        this._collectionsService.addToCollection(FilmToCollection).subscribe();
      }
}