export interface IUserRegisterDto {
    emailAddress: string;
    username: string;
    firstName: string;
    lastName: string;
    birthDate: Date;
    password: string;
    repeatedPassword: string;
}