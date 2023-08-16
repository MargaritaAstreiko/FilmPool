import { User } from "./user.model";

export class AuthResponseDto {
    isAuthSuccessful!: boolean;
    errorMessage!: string;
    token!: string;
    user!: User;
}