import { DecimalPipe } from "@angular/common";
import { Genre } from "../enums/genre.enum";

export class Film {
    id!: number;
    title!: string;
    genre!: string;
    duration!: string;
    year!: number;
    description!: string;
    picture!: string;
    rating!: number;

}