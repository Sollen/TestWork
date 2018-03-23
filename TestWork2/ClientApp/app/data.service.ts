//Сервис для связи с контроллером
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Quote } from './quote';
import { Category } from './category';

@Injectable()
export class DataService {

    private urlQuote = "/api/quotes";
    
    
    constructor(private http: HttpClient) {
    }

    //Получаем все цитаты
    getQuotes() {
               
        return this.http.get(this.urlQuote);
    }
    //Создаём цитату
    createQuote(quote: Quote) {
        return this.http.post(this.urlQuote, quote);
    }
    //Изменяем цитату
    updateQuote(quote: Quote) {

        return this.http.put(this.urlQuote + '/' + quote.id, quote);
    }
    //Удаляем цитату
    deleteQuote(id: number) {
        return this.http.delete(this.urlQuote + '/' + id);
    }
}