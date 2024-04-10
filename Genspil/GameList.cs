using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

    //todo: Lav Sortering efter navn & genre 


    /// <summary>
    /// Indeholder alle vores game objekter
    /// </summary>
    public class GameList
    {
        /// <summary>
        /// Liste der indeholder hvert GameItem objekt
        /// </summary>
        private List<GameItem> _gameItems = new List<GameItem>();
        /// <summary>
        /// Liste der indeholder hver linje læst fra filen som string
        /// </summary>
        private List<string> _lines = new List<string>();

        private string _path = @"c:\temp\gameItems.txt";

        GameItem GameItem = new GameItem();


        /// <summary>
        /// Laver en fil hvis filen ikke findes - kører AddFirstGame, så den ansatte får lavet en lagerliste
        /// </summary>
        public void CreateGameFile()
        {
            if (!File.Exists(_path))
            {
                AddFirstGame();

                WriteFile();
            }
        }


        /// <summary>
        /// Tilføjer et spil første gang programmet startes
        /// </summary>
        public void AddFirstGame()
        {

            bool addGameInput = false;

            do
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine("Opret det første spil:");

                    Console.Write("Skriv navn: ");
                    if ((GameItem.Title = Console.ReadLine().ToLower()) == "")
                    {
                        throw new Exception("Titlen skal udfyldes, opret spil igen");
                    }

                    Console.Write("Tilføj evt. en udgave eller tryk enter: ");
                    GameItem.Edition = Console.ReadLine().ToLower();

                    Console.Write("Skriv minimum antal spillere: ");
                    GameItem.MinNumberOfPlayers = Console.ReadLine();

                    Console.Write("Skriv maksimum antal spillere: ");
                    GameItem.MaxNumberOfPlayers = Console.ReadLine();

                    Console.Write("Skriv spillets sprog: ");
                    GameItem.Language = Console.ReadLine().ToLower();

                    Console.Write("Skriv spillets kategori: ");
                    GameItem.Category = Console.ReadLine().ToLower();

                    Console.Write("Skriv spillets stand (1 - 10): ");
                    GameItem.Condition = Console.ReadLine().ToLower();

                    try
                    {
                        Console.Write("Skriv spillets pris: ");
                        GameItem.Price = double.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw new Exception("Pris skal udfyldes med et tal");
                    }

                    try
                    {
                        Console.Write("Skriv antal: ");
                        GameItem.Stock = int.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw new Exception("Antal udfyldes med et tal");
                    }

                    _gameItems.Add(new GameItem(GameItem.Id, GameItem.Title, GameItem.Edition, GameItem.MinNumberOfPlayers, GameItem.MaxNumberOfPlayers, GameItem.Language, GameItem.Category, GameItem.Condition, GameItem.Price, GameItem.Stock));

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



        // create-metoden (crud), der opretter et nyt spil

        /// <summary>
        /// Tilføjer spil 
        /// </summary>
        public void AddGame()
        {
            bool addGameInput = false;

            do
            {
                try
                {
                    GameItem.Id = AddGameId();

                    Console.WriteLine("Opret spil:");

                    Console.Write("Skriv navn: ");
                    GameItem.Title = Console.ReadLine().ToLower();

                    Console.Write("Tilføj evt. en udgave eller tryk enter: ");
                    GameItem.Edition = Console.ReadLine().ToLower();

                    Console.Write("Skriv minimum antal spillere: ");
                    GameItem.MinNumberOfPlayers = Console.ReadLine();

                    Console.Write("Skriv maksimum antal spillere: ");
                    GameItem.MaxNumberOfPlayers = Console.ReadLine();

                    Console.Write("Skriv spillets sprog: ");
                    GameItem.Language = Console.ReadLine().ToLower();

                    Console.Write("Skriv spillets kategori: ");
                    GameItem.Category = Console.ReadLine().ToLower();

                    Console.Write("Skriv spillets stand (1 - 10): ");
                    GameItem.Condition = Console.ReadLine().ToLower();

                    try
                    {
                        Console.Write("Skriv spillets pris: ");
                        GameItem.Price = double.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw new Exception("Pris skal udfyldes med et tal");
                    }

                    try
                    {
                        Console.Write("Skriv antal: ");
                        GameItem.Stock = int.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw new Exception("Antal udfyldes med et tal");
                    }

                    _gameItems.Add(new GameItem(GameItem.Id, GameItem.Title, GameItem.Edition, GameItem.MinNumberOfPlayers, GameItem.MaxNumberOfPlayers, GameItem.Language, GameItem.Category, GameItem.Condition, GameItem.Price, GameItem.Stock));

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


        /// <summary>
        /// Læser den sidste gameItems' id, og sætter tilføjede GameItems' id +1
        /// </summary>
        /// <returns></returns>
        public int AddGameId()
        {
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

        /// <summary>
        /// Opdaterer et spil på listen
        /// </summary>
        public void UpdateGame()
        {
            bool exitUpdate = false;

            do
            {

                int inputUpdate;

                Console.Write("Skriv nummeret på det spil, du vil opdatere: ");

                int.TryParse(Console.ReadLine(), out inputUpdate);

                inputUpdate -= 1;

                if (inputUpdate >= 0 && inputUpdate < _lines.Count)
                {
                    Console.Write(_lines[inputUpdate]);

                    string[] splitString = new string[2]; // declared

                    splitString = _lines[inputUpdate].Split(','); // init

                    Console.Write("Skriv det tal, der svarer til den del, du vil opdatere: ");

                    int userInput;
                    int.TryParse(Console.ReadLine(), out userInput);

                    Console.Write("Skriv, hvad der skal opdateres: ");

                    splitString[userInput] = Console.ReadLine().ToLower();

                    string joinedString = string.Join(",", splitString);

                    _lines[inputUpdate] = joinedString;

                    ConvertListStringToListGameItem();

                    exitUpdate = true;
                }
                else
                {
                    Console.WriteLine("Du vil opdatere et spil der ikke findes på listen, vælg igen");
                    continue;
                }

            } while (!exitUpdate);

        }



        // Skal laves om så den sletter på ID

        /// <summary>
        /// Sletter et spil på en linje indtastet af brugeren 
        /// </summary>
        public void DeleteGame()
        {

            Console.Write("Skriv hvilken linje du vil slette: ");

            int inputDelete;
            int.TryParse(Console.ReadLine(), out inputDelete);

            inputDelete -= 1;

            if (inputDelete < _lines.Count)
            {
                string deletedGame = _lines[inputDelete];
                _lines.RemoveAt(inputDelete);
                File.WriteAllLines(_path, _lines);
                Console.WriteLine($"Spillet: {deletedGame} blev slettet");
            }
            else
            {
                Console.WriteLine("Ingen spil blev slettet");
            }

        }

        /// <summary>
        /// Konverterer fra string til GameItem objekt og viser listen
        /// </summary>
        public void ShowGameItems()
        {

            ConvertListStringToListGameItem();

            Console.WriteLine();
            Console.WriteLine("{0, -5} {1, -15} {2, -15} {3, 3} - {4, -6} {5, -10} {6, -10} {7, -10} {8, -10} {9, -10}", "Id", 
                "Navn", "Edition", "Min", "Max", "Sprog", "Kategori", "Stand", "Pris", "Lagerstatus");

            Console.WriteLine();

            // Print each element in _gameItems             
            foreach (GameItem item in _gameItems)
            {
                Console.WriteLine("{0, -5} {1, -15} {2, -15} {3, 3} - {4, -6} {5, -10} {6, -10} {7, -10} {8, -10} {9, -10}", 
                    item.Id, item.Title, item.Edition, item.MinNumberOfPlayers, item.MaxNumberOfPlayers, item.Language, item.Category, item.Condition, item.Price, item.Stock);
            }

            _gameItems.Clear();

        }

        /// <summary>
        /// Søger på hele string (ikke parametre)
        /// </summary>
        public void SearchAllGameItems()
        {

            Console.WriteLine("Søgning: ");
            string searchText = Console.ReadLine().ToLower();

            List<string> results = _lines.FindAll(line => line.Contains(searchText));

            foreach (string result in results)
            {
                Console.WriteLine(result);
            }
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

    }

}
