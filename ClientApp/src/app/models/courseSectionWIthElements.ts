import { ICourseElement } from "./courseElement";
import { ICourseSection } from "./courseSection";

export interface ICourseSectionWithElements{
    section: ICourseSection;
    elements: ICourseElement[];
    closed: boolean;
}