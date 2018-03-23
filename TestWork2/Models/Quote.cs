using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//Модель данных цитаты

namespace TestWork2.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public Category Category { get; set; }
    }
}
