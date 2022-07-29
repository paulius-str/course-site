export interface IUser{
    id: string,
    emailAddress: string,
    name: string,
    lastName: string,
    isPublisher?: boolean,
    IsAdmin?: boolean
}