
export interface IRating{
    id: string;
    purchasedCourseId?: string;
    studentId?: string;
    score: number;
    review: string;
    crationDate?: string;
    lastEditDate?: string;
}