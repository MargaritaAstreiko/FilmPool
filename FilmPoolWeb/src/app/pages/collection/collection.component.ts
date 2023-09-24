import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Collection } from 'src/app/models/collection.model';
import { Film } from 'src/app/models/film.model';
import { FilmLightModel } from 'src/app/models/filmLight.model';
import { CollectionsService } from 'src/app/services/collections.services';
import { FilmsService } from 'src/app/services/films.service';

@Component({
  selector: 'app-collection',
  templateUrl: './collection.component.html',
  styleUrls: ['./collection.component.css']
})
export class CollectionComponent implements OnInit {

  films!: FilmLightModel[];
  collectionInfo!: FormGroup;
  addNewCollection = false;
  collections!: Collection[];
  filmSelected!: FilmLightModel;
  activeCollection!: number;
  userId = localStorage.getItem("userId") || 0;

  constructor(
    private _collectionsService: CollectionsService,

  ) { }
  ngOnInit() {
    this._collectionsService.getCollection(+this.userId).subscribe(data => {
      this.collections = data;
    });
    this.collectionInfo = new FormGroup({
      collectionName: new FormControl(""),
      isPublic: new FormControl(""),
      //  collectionItem: new FormControl(""),
    })
  }
  saveCollection = (collectionInfoValue: any) => {
    this.newCollection();
    const name = { ...collectionInfoValue }
    const newCollection: Collection = {
      id: 0,
      collectionName: name.collectionName,
      userId: +this.userId,
      filmId: 0,
      createdDate: new Date(),
      isPublic: !name.isPublic,
      filmNames:[],
    }
    this._collectionsService.createCollection(newCollection).subscribe();
    this._collectionsService.getCollection(+this.userId).subscribe(data => {
      this.collections = data;
    });
  }

  activateCollection = (id: number) => {
    this.activeCollection = id;

  }
  newCollection = () => {
    this.addNewCollection = !this.addNewCollection;
  }

  removeCollection = (id: number) => {
    this._collectionsService.removeCollection(id).subscribe();

  }
}

