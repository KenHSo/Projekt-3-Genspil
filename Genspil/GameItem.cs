﻿using System;
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
        // 10 attributter
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string MinNumberOfPlayers { get; set; }
        public string MaxNumberOfPlayers { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        //public string Title
        //{
        //    get { return _title; }
        //    set { _title = value; }
        //}


        public GameItem(int id, string title, string subtitle, string minNumberOfPlayers, string maxNumberOfPlayers, string language, string category, string condition, double price, int stock)
        {
            Id = id;
            Title = title;
            Subtitle = subtitle;
            MinNumberOfPlayers = minNumberOfPlayers;
            MaxNumberOfPlayers = maxNumberOfPlayers;
            Language = language;
            Category = category;
            Condition = condition;
            Price = price;
            Stock = stock;
        }



    }
}
