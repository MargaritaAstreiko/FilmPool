export class Comment {
    id!: number;
    userId!: number;
    filmId!: number; 
    comment!: string;
    createdDate!: Date|string;
}