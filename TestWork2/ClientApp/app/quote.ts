import { Category } from "./category";

export class Quote {
    constructor(
        public id?: number,
        public author?: string,
        public date?: Date,
        public text?: string,        
        public category?: Category) { }
    
}