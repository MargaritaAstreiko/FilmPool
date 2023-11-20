export class Comment {
    id!: number;
    userId!: number;
    filmId!: number; 
    comment!: string;
    userName!: string;
    picture!: string;
    createdDate!: Date|string;
}