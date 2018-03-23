import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Quote } from './quote';
import { Category } from './category';
import { HttpResponse } from '@angular/common/http';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {

    quote: Quote = new Quote(); //переменная для редактирования и создания новой цитаты
    category: Category = new Category(); // Переменная для создания\редактирования категории цитаты
    quotes: Quote[];//Все цитаты
    tableMode: boolean = true;  //Отображение шаблона вывода всех цитат
    name = '';//Хранение имени категории   
    
    

    constructor(private dataService: DataService) { }
    
    ngOnInit() {
        this.loadQuotes();
    }
    //Загрузка всех цитат
    loadQuotes() {
        this.category.id = 0;
        this.category.name = this.name;
        this.quote.category = this.category;        
        this.dataService.getQuotes()
            .subscribe((data: Quote[]) => this.quotes = data);
        
        
    }
    //Сохранение новой или обновление цитаты
    save() {
        if (this.quote.id == null) {            
            this.quote.category = this.category;
            this.dataService.createQuote(this.quote)
                .subscribe((data: HttpResponse<Quote>) => {
                    console.log(data);
                    this.quotes.push(data.body);
                    this.loadQuotes();
                   
                });
            
        } else {
            this.dataService.updateQuote(this.quote)
                .subscribe(data => this.loadQuotes());
        }
        
        this.cancel();
        
    }
    //Передаём в переменную quote цитату для редактирования
    editQuote(p: Quote) {
        this.quote = p;
    }
    //Зануляем все перемнные
    cancel() {
        this.quote = new Quote();
        this.category.id = 0;
        this.category.name = this.name;
        this.quote.category = this.category;
        this.tableMode = true;
    }
    //Удаляем цитату
    delete(p: Quote) {
        this.dataService.deleteQuote(p.id)
            .subscribe(data => this.loadQuotes());
    }
    //Вызываем шаблон для добавления цитаты
    add() {
        this.cancel();
        this.tableMode = false;
    }
}