import { iBook } from "./book";

export interface iTranslator {
    translatorId: number;
    translatorName: string;
    books: iBook[];
}
