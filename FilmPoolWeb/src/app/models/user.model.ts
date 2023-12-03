import { Role } from "../enums/user.enum";

export class User {
    id!: number;
    firstName!: string;
    lastName!: string;
    userName!: string;
    email!: string;
    password!: string;
    userRole!: Role;
    role!: any| null;
    picture!: string;
    isBlocked!: boolean;

}