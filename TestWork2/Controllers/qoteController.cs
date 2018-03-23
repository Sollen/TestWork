//Основной контроллер сриложения
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestWork2.Data;
using TestWork2.Models;

namespace TestWork2.Controllers
{
    [Route("api/quotes")]
    public class QuoteController : Controller
    {
        ApplicationContext db;
        
        public QuoteController(ApplicationContext context)
        {
            db = context;
            //Для теста. Если в БД нет элементов, то заполняются тестовыми элементами
            if (!db.Categories.Any())
            {
                db.Categories.Add(new Category { Name = "Философия" });
                db.Categories.Add(new Category { Name = "Юмор" });
                db.Categories.Add(new Category { Name = "Позновательно" });
                db.Categories.Add(new Category { Name = "Разное" });
                db.SaveChanges();
            }
            if (!db.Quotes.Any())
            {
                db.Quotes.Add(new Quote { Author = "Лев Толстой", Text = "Цитата Льва Толстого", Date = DateTime.UtcNow, Category = db.Categories.FirstOrDefault(x => x.Name == "Философия")});
                db.Quotes.Add(new Quote { Author = "Михаил Задорнов", Text = "Цитата Михаила Задорнова", Date = DateTime.UtcNow, Category = db.Categories.FirstOrDefault(x => x.Name == "Юмор") });
                db.Quotes.Add(new Quote { Author = "Василий Кожухов", Text = "Цитата Василия Кожухова", Date = DateTime.UtcNow, Category = db.Categories.FirstOrDefault(x => x.Name == "Позновательно") });

                db.SaveChanges();
            }
            
            
        }
        //Выборка всех цитат
        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            return db.Quotes.Include(q => q.Category).ToList(); ;
        }

        //Выборка цитаты по id
        [HttpGet("{id}")]
        public Quote Get(int id)
        {
            Quote product = db.Quotes.FirstOrDefault(x => x.Id == id);
            return product;
        }
        //Создание цитаты
        [HttpPost]
        public IActionResult Post([FromBody]Quote quote)
        {
            
            if (ModelState.IsValid)
            {
                quote.Date = DateTime.UtcNow; //Дата в часовом поясе UTC
                if (db.Categories.FirstOrDefault(q => q.Name == quote.Category.Name) == null)
                {
                    quote.Category =new Category { Name = quote.Category.Name };
                        
                }
                    
                db.Quotes.Add(quote);
                db.SaveChanges();
                return Ok(quote);
            }
            
            
            
            return BadRequest(ModelState);
        }
        //Изменение цитаты. Тут тоже сделать изменение времени?
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Quote quote)
        {
           
            if (ModelState.IsValid)
            {                    
                db.Update(quote);
                db.SaveChanges();
                return Ok(quote);
            }
            return BadRequest(ModelState);
            
        }
        //Удаление цитаты
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Quote product = db.Quotes.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                db.Quotes.Remove(product);
                db.SaveChanges();
            }
            return Ok(product);
        }
    }
}
