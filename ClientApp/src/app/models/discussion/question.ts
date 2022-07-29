export interface IQuestion {
    id?: string;
    elementId?: string;
    userId: string;
    title: string;
    text: string;
    creationDate?: string;
    lastEditDate?: string;
}

export interface IAnswer {
    id?: string;
    questionId?: string;
    userId?: string;
    text?: string;
    creationDate?: string;
    lastEditDate?: string;
}