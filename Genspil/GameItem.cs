using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Genspil
{
    public class GameItem
    {

        // 10 attributter / properties 
        public int Id { get; set; }       
        public string Title { get; set; }
        public string Edition { get; set; }
        public string MinNumberOfPlayers { get; set; }
        public string MaxNumberOfPlayers { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }


        public GameItem(int id, string title, string edition, string minNumberOfPlayers, string maxNumberOfPlayers, string language, string category, string condition, double price, int stock)
        {
            Id = id;
            Title = title;
            Edition = edition;
            MinNumberOfPlayers = minNumberOfPlayers;
            MaxNumberOfPlayers = maxNumberOfPlayers;
            Language = language;
            Category = category;
            Condition = condition;
            Price = price;
            Stock = stock;
        }

        public GameItem(string title, string edition, string minNumberOfPlayers, string maxNumberOfPlayers, string language, string category, string condition, double price, int stock) : this (1, title, edition, minNumberOfPlayers, maxNumberOfPlayers, language, category, condition, price, stock)
        {
        }

        public GameItem()
        {
        }

    }
}
