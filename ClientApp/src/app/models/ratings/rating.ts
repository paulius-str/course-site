
export interface IRating{
    id: string;
    purchasedCourseId?: string;
    userId: string;
    score: number;
    review: string;
    crationDate?: string;
    lastEditDate?: string;
}