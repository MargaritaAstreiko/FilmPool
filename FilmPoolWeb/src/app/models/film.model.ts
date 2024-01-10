import { Genre } from "../enums/genre.enum";

 
export class Film {
    id!: number;
    title!: string;
    genre!: Genre;
    duration!: string;
    year!: number;
    description!: string;
    picture!: string;
    rating!: number;
    filmUrl!: string;

}