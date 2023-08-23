import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Film } from 'src/app/models/film.model';

@Component({
  selector: 'app-collection',
  templateUrl: './collection.component.html',
  styleUrls: ['./collection.component.css']
})
export class CollectionComponent {
  films!: Film[];
  collectionInfo!: FormGroup;
  addNewCollection = false;
  saveCollection = (collectionInfoValue: any) => {

  }

  newCollection=()=>{
    this.addNewCollection  = !this.addNewCollection;
  }
}
