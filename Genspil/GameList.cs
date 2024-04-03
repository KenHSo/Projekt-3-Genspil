using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace Genspil
{
    // Principper
    // Brug List<GameItem> frem for List<string>
    // Konverter List<GameItem> til List<string> & omvendt (fjerner string override & string.split & .join)
    // Properties for at få adgang til GameItem. osv.
    // gameItems.txt = ren kommasepareret fil - udskrivning af data klares med $ stringinterpolation

    //todo: Lav Søgning (på et GameItem field) + kombination af fields
    //todo: Lav Opdatering

    //todo: Exception handling + brugerinput
    //todo: Lav Sortering efter navn & genre 
    //todo: DateTime created / DateTime updated - virker måske?
    //todo: Forspørgsler håndteres i en anden klasse end GameItem


    // Menu
    // Exceptionhandling
    // - ingen fil
    // - tom fil

    // if file !exist, create file
    // AddGame() -> AddGameID() -> Readfile(), Readfile can't read empty

    // exceptionhandling på resten af menu - alle steder hvor der parses 
    // index + 1, når du skriver til menu
    // rettet tekster i menu ConsoleWriteLines



    public class GameList
    {

        private List<GameItem> _gameItems = new List<GameItem>();
        internal List<string> _lines = new List<string>();

        private string _path = @"c:\temp\gameItems.txt";


        // create-metoden (crud), der opretter et nyt spil
        public void AddGame()
        {
            bool addGameInput = false;

            do
            {
                Console.Clear();

                try
                {
                    int id = AddGameId();

                    string title;
                    string edition;
                    string minNumberOfPlayers;
                    string maxNumberOfPlayers;
                    string language;
                    string category;
                    string condition;
                    double price;
                    int stock;

                    Console.WriteLine("Opret spil:");

                    Console.Write("Skriv navn: ");
                    title = Console.ReadLine().ToLower();

                    Console.Write("Tilføj evt. en undertitel eller tryk enter: ");
                    edition = Console.ReadLine().ToLower();

                    Console.Write("Skriv minimum antal spillere: ");
                    minNumberOfPlayers = Console.ReadLine();

                    Console.Write("Skriv maksimum antal spillere: ");
                    maxNumberOfPlayers = Console.ReadLine();

                    Console.Write("Skriv spillets sprog: ");
                    language = Console.ReadLine().ToLower();

                    Console.Write("Skriv spillets kategori: ");
                    category = Console.ReadLine().ToLower();

                    Console.Write("Skriv spillets stand (1 - 10): ");
                    condition = Console.ReadLine().ToLower();

                    Console.Write("Skriv spillets pris: ");
                    price = double.Parse(Console.ReadLine());

                    Console.Write("Skriv antal: ");
                    stock = int.Parse(Console.ReadLine());


                    _gameItems.Add(new GameItem(id, title, edition, minNumberOfPlayers, maxNumberOfPlayers, language, category, condition, price, stock));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Indtast dit spil igen");
                    continue;
                }

                addGameInput = true;

            } while (!addGameInput);

        }

        // pseudo til AddGameId.
        // Læs den sidste gameItems' id.
        // id = sidste gameItems id + 1.
        public int AddGameId()
        {
            ReadFile();

            string[] idLine = _lines.Last().Split(',');

            int id;
            id = Convert.ToInt32(idLine[0]);
            id++;

            return id;
        }


        /// <summary>
        /// Tilføjer et GameItem til bunden af filen
        /// </summary>
        public void WriteFile()
        {
            foreach (GameItem item in _gameItems)
            {
                // This text is always added, making the file longer over time
                // if it is not deleted.
                string appendText = $"{item.Id},{item.Title},{item.Edition},{item.MinNumberOfPlayers},{item.MaxNumberOfPlayers},{item.Language},{item.Category},{item.Condition},{item.Price},{item.Stock}" + Environment.NewLine;
                File.AppendAllText(_path, appendText);
            }
        }

        /// <summary>
        /// Overskriver den gamle fil, så det opdaterede GameItem bliver sat ind på samme plads
        /// </summary>
        public void WriteLineToFile()
        {
            File.Delete(_path);

            foreach (GameItem item in _gameItems)
            {
                string overwriteText = $"{item.Id},{item.Title},{item.Edition},{item.MinNumberOfPlayers},{item.MaxNumberOfPlayers},{item.Language},{item.Category},{item.Condition},{item.Price},{item.Stock}" + Environment.NewLine;
                File.AppendAllText(_path, overwriteText);
            }
        }

        /// <summary>
        /// Læs gameItems.txt og indsæt hver linje som element på listen _lines
        /// </summary>
        public void ReadFile()
        {
            _lines.Clear();
            _lines = File.ReadAllLines(_path).ToList();
        }


        public void ShowGameItems()
        {
            ReadFile();
            ConvertListStringToListGameItem();

            // Print each element in _gameItems             
            foreach (GameItem item in _gameItems)
            {
                Console.WriteLine($"{item.Id}, {item.Title}, {item.Edition}, {item.MinNumberOfPlayers} - {item.MaxNumberOfPlayers}, {item.Language}, {item.Category}, {item.Condition}, {item.Price}, {item.Stock}");
                //Console.WriteLine($"Navn: {item.Name}, Antal spillere: {item.NumberOfPlayers}");
            }

            _gameItems.Clear();

        }
        //public void ShowLines()
        //{
        //    // Print each element in _lines           
        //    foreach (string line in _lines)
        //    {
        //        Console.WriteLine(line);
        //    }
        //}



        // Skal laves om så den sletter på ID
        public void Delete()
        {
            ReadFile();

            Console.Write("Skriv hvilken linje du vil slette: ");

            int inputDelete;
            int.TryParse(Console.ReadLine(), out inputDelete);

            if (inputDelete < _lines.Count)
            {
                _lines.RemoveAt(inputDelete);
                File.WriteAllLines(_path, _lines);
            }
        }



        public void SearchAll()
        {
            ReadFile();

            Console.WriteLine("Søgning: ");
            string searchText = Console.ReadLine().ToLower();

            List<string> results = _lines.FindAll(line => line.Contains(searchText));

            foreach (string result in results)
            {
                Console.WriteLine(result);
            }
        }



        // pseudo til update

        // ReadAllLines til List "lines"
        // Show()
        // Bruger input: vælg linje til opdatering - gem i variable og brug igen i WriteToFile (overwrite)
        // Show() valgte linje
        // Array Split med , delimiter
        // Array[0] navn, [1] antal spillere osv.
        // bruger input: vælg den del der skal opdateres
        // bruger input: skriv den del der skal opdateres
        // string .Join til en string
        // WriteToFile til samme linje
        public void Update()
        {
            int inputUpdate;

            ReadFile();

            Console.Write("Skriv nummeret på det spil, du vil opdatere: ");

            int.TryParse(Console.ReadLine(), out inputUpdate);

            Console.WriteLine(_lines[inputUpdate]);

            if (inputUpdate >= 0 && inputUpdate < _lines.Count)
            {
                string[] splitString = new string[2];

                splitString = _lines[inputUpdate].Split(',');

                Console.Write("Skriv det tal, der svarer til den del, du vil opdatere: ");

                int userInput;
                int.TryParse(Console.ReadLine(), out userInput);

                Console.Write("Skriv, hvad der skal opdateres: ");

                splitString[userInput] = Console.ReadLine().ToLower();

                string joinedString = string.Join(",", splitString);

                _lines[inputUpdate] = joinedString;

                ConvertListStringToListGameItem();

            }
        }


        public void CreateFile()
        {
            if (!File.Exists(_path))
            {
                AddFirstGame();

                WriteFile();
            }
        }

        public void AddFirstGame()
        {

            bool addGameInput = false;

            do
            {
                try
                {

                    Console.Clear();

                    string title;
                    string subtitle;
                    string minNumberOfPlayers;
                    string maxNumberOfPlayers;
                    string language;
                    string category;
                    string condition;
                    double price;
                    int stock;

                    Console.WriteLine("Opret det første spil:");

                    Console.Write("Skriv navn: ");
                    title = Console.ReadLine().ToLower();

                    Console.Write("Tilføj evt. en undertitel eller tryk enter: ");
                    subtitle = Console.ReadLine().ToLower();

                    Console.Write("Skriv minimum antal spillere: ");
                    minNumberOfPlayers = Console.ReadLine();

                    Console.Write("Skriv maksimum antal spillere: ");
                    maxNumberOfPlayers = Console.ReadLine();

                    Console.Write("Skriv spillets sprog: ");
                    language = Console.ReadLine().ToLower();

                    Console.Write("Skriv spillets kategori: ");
                    category = Console.ReadLine().ToLower();

                    Console.Write("Skriv spillets stand (1 - 10): ");
                    condition = Console.ReadLine().ToLower();

                    Console.Write("Skriv spillets pris: ");
                    price = double.Parse(Console.ReadLine());

                    Console.Write("Skriv antal: ");
                    stock = int.Parse(Console.ReadLine());

                    _gameItems.Add(new GameItem(title, subtitle, minNumberOfPlayers, maxNumberOfPlayers, language, category, condition, price, stock));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Indtast dit spil igen");
                    continue;
                }

                addGameInput = true;

            } while (!addGameInput);

        }





        // Hjælpemetoder.

        /// <summary>
        /// Konverterer en liste af strings (_lines) til en liste af GameItem (_gameItems)
        /// </summary>
        public void ConvertListStringToListGameItem()
        {
            _gameItems.Clear();
            _gameItems = _lines.Select(item =>
            {
                var parts = item.Split(',');

                int id = Convert.ToInt32(parts[0]);
                double price = Convert.ToDouble(parts[8]);
                int stock = Convert.ToInt32(parts[9]);

                return new GameItem(id, parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], price, stock);
            }
            ).ToList();
        }

        /// <summary>
        /// Konverterer en liste af GameItem (_gameItems) til en liste af strings (_lines)
        /// </summary>
        public void ConvertListGameItemToListString()
        {
            _lines.Clear();
            _lines = _gameItems.Select(item => item.Title + "," + item.Edition + "," + item.MinNumberOfPlayers + item.MaxNumberOfPlayers + "," + item.Language + "," + item.Category + "," + item.Condition + "," + item.Price + "," + item.Stock).ToList();
        }


    }

}
