export interface IUser{
    id: string,
    emailAddress: string,
    firstName: string,
    lastName: string,
    isPublisher?: boolean,
    IsAdmin?: boolean
}