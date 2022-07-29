import { ICourseElement } from "../courseElement";
import { CourseElementArticleContent } from "./courseElementArticleContent";
import { CourseElementVideoContent } from "./courseElementVIdeoContent";


export interface IElementContent {
    courseElement: ICourseElement;
    articleContent: CourseElementArticleContent;
    videoContent: CourseElementVideoContent;
}