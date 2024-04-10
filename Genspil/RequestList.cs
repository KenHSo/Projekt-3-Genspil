using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    internal class RequestList
    {
        private List<RequestItem> _requestItems = new List<RequestItem>();
        private List<string> _lines = new List<string>();

        private string _path = @"c:\temp\requestItems.txt";

        RequestItem requestItem = new RequestItem();


        // create-metoden (crud), der opretter en ny forespørgsel
        public void AddRequest()
        {
            bool addRequestInput = false;         

            do
            {
                try
                {
                    requestItem.Id = AddRequestId();

                    Console.WriteLine("Opret forespørgsel:");

                    Console.Write("Indtast spiltitel: ");
                    requestItem.Title = Console.ReadLine().ToLower();

                    Console.Write("Tilføj evt. en udgave eller tryk enter: ");
                    requestItem.Edition = Console.ReadLine().ToLower();

                    Console.Write("Indtast spillets sprog: ");
                    requestItem.Language = Console.ReadLine().ToLower();

                    Console.Write("Indtast det fulde navn på kunden: ");
                    requestItem.CustomerName = Console.ReadLine().ToLower();

                    Console.Write("Indtast en reference på kunden - f.eks. telefonnr.: ");
                    requestItem.Reference = Console.ReadLine().ToLower();

                    _requestItems.Add(new RequestItem(requestItem.Id, requestItem.Title, requestItem.Edition, requestItem.Language, requestItem.CustomerName, requestItem.Reference));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Indtast din forespørgsel igen");
                    continue;

                }

                addRequestInput = true;

            } while (!addRequestInput);

        }

        // pseudo til AddGameId.
        // Læs den sidste gameItems' id.
        // id = sidste gameItems id + 1.
        public int AddRequestId()
        {
            string[] idLine = _lines.Last().Split(',');

            int id;
            id = Convert.ToInt32(idLine[0]);
            id++;

            return id;
        }


        /// <summary>
        /// Tilføjer et RequestItem til bunden af filen
        /// </summary>
        public void WriteFile()
        {
            foreach (RequestItem item in _requestItems)
            {
                // This text is always added, making the file longer over time
                // if it is not deleted.
                string appendText = $"{item.Id},{item.Title},{item.Edition},{item.Language},{item.CustomerName},{item.Reference}" + Environment.NewLine;
                File.AppendAllText(_path, appendText);
            }
        }

        /// <summary>
        /// Overskriver den gamle fil, så det opdaterede RequestItem bliver sat ind på samme plads
        /// </summary>
        public void WriteLineToFile()
        {
            File.Delete(_path);

            foreach (RequestItem item in _requestItems)
            {
                string overwriteText = $"{item.Id},{item.Title},{item.Edition},{item.Language},{item.CustomerName},{item.Reference}" + Environment.NewLine;
                File.AppendAllText(_path, overwriteText);
            }
        }

        /// <summary>
        /// Læs requestItems.txt og indsæt hver linje som element på String listen _lines
        /// </summary>
        public void ReadFile()
        {
            _lines.Clear();
            _lines = File.ReadAllLines(_path).ToList();
        }


        public void ShowRequestItems()
        {
            // konverterer string listen _lines til listen med RequestItem objekter
            ConvertListStringToListRequestItem();

            // Print each element in _requestItems             
            foreach (RequestItem item in _requestItems)
            {

                // vi overvejede at gøre det klart hvilke egenskaber der vises, men det vil skabe tumult i søgning
                //Console.WriteLine($"{0,-20}{1,-15}", item.Title, item.Edition);
                Console.WriteLine($"{item.Id}, {item.Title}, {item.Edition}, {item.Language}, {item.CustomerName}, {item.Reference}");
                //Console.WriteLine($"Navn: {item.Name}, Antal spillere: {item.NumberOfPlayers}");
            }

            _requestItems.Clear();

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
        public void DeleteRequest()
        {

            Console.Write("Skriv hvilken linje du vil slette: ");

            int inputDelete;
            int.TryParse(Console.ReadLine(), out inputDelete);

            inputDelete -= 1;

            if (inputDelete >= 0 && inputDelete < _lines.Count)
            {
                string deletedRequest = _lines[inputDelete];
                _lines.RemoveAt(inputDelete);
                File.WriteAllLines(_path, _lines);
                Console.WriteLine($"Forespørgslen: {deletedRequest} blev slettet");
            }
            else
            {
                Console.WriteLine("Ingen forespørgsler blev slettet");
            }

        }






        // denne metode gør det muligt at fremsøge forespørgsler ved tekst. 
        // vi bruger indbyggede metoder til at søge i _lines som læser requestItems-filen vha metoden ReadFile i linje 110. 
        public void SearchAllRequestItems()
        {

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

        //Det kunne være godt at opdatere ud fra ID (RequestNumber).
        public void UpdateRequest()
        {
            bool exitUpdate = false;

            do
            {

                int inputUpdate;

                Console.Write("Skriv nummeret på den forespørgsel, du vil opdatere: ");

                int.TryParse(Console.ReadLine(), out inputUpdate);

                inputUpdate -= 1;

                if (inputUpdate >= 0 && inputUpdate < _lines.Count)
                {
                    Console.Write(_lines[inputUpdate]);

                    string[] splitString = new string[6];

                    splitString = _lines[inputUpdate].Split(',');

                    Console.Write("\nSkriv det tal, der svarer til den del, du vil opdatere: ");

                    int userInput;
                    int.TryParse(Console.ReadLine(), out userInput);

                    Console.Write("Skriv, hvad der skal opdateres: ");

                    splitString[userInput] = Console.ReadLine().ToLower();

                    string joinedString = string.Join(",", splitString);

                    _lines[inputUpdate] = joinedString;

                    ConvertListStringToListRequestItem();

                    exitUpdate = true;
                }
                else
                {
                    Console.WriteLine("Du vil opdatere en forespørgsel der ikke findes på listen, vælg igen.");
                    continue;
                }

            } while (exitUpdate == false);

        }

        //denne metode opretter en ny fil med requestItems, hvis den ikke allerede er oprettet. 
        //den bruges til at oprette den første requestItem aka forespørgsel.
        // den bruger objektet fra metoden nedenunder AddFirstRequest()
        public void CreateRequestFile()
        {
            if (!File.Exists(_path))
            {
                AddFirstRequest();

                WriteFile();
            }
        }


        // denne metode opretter den første requestItem objekt. 
        public void AddFirstRequest()
        {

            bool addRequestInput = false;

            do
            {
                try
                {
                    Console.Clear();

                    Console.WriteLine("Opret den første forespørgsel:");

                    Console.Write("Indtast spiltitel: ");
                    if ((requestItem.Title = Console.ReadLine().ToLower()) == "")
                    {
                        throw new Exception("Titlen skal udfyldes. Prøv igen.");
                    }

                    Console.Write("Tilføj evt. en udgave eller tryk enter: ");
                    requestItem.Edition = Console.ReadLine().ToLower();

                    Console.Write("Indtast spillets sprog: ");
                    requestItem.Language = Console.ReadLine().ToLower();

                    Console.Write("Indtast det fulde navn på kunden: ");
                    requestItem.CustomerName = Console.ReadLine().ToLower();

                    Console.Write("Indtast en reference på kunden - f.eks. telefonnr.: ");
                    requestItem.Reference = Console.ReadLine().ToLower();

                    _requestItems.Add(new RequestItem(requestItem.Id, requestItem.Title, requestItem.Edition, requestItem.Language, requestItem.CustomerName, requestItem.Reference));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Indtast din forespørgsel igen");
                    continue;

                }

                addRequestInput = true;

            } while (!addRequestInput);

        }


        // Hjælpemetoder.

        /// <summary>
        /// Konverterer en liste af strings (_lines) til en liste af RequestItem (_requestItems)
        /// </summary>
        public void ConvertListStringToListRequestItem()
        {
            _requestItems.Clear();
            _requestItems = _lines.Select(item =>
            {
                var parts = item.Split(',');

                int id = Convert.ToInt32(parts[0]);
                //double price = Convert.ToDouble(parts[8]);
                //int stock = Convert.ToInt32(parts[9]);

                return new RequestItem(id, parts[1], parts[2], parts[3], parts[4], parts[5]);
            }
            ).ToList();
        }

        /// <summary>
        /// Konverterer en liste af GameItem (_gameItems) til en liste af strings (_lines)
        /// </summary>
        //public void ConvertListGameItemToListString()
        //{
        //    _lines.Clear();
        //    _lines = _gameItems.Select(item => item.Title + "," + item.Edition + "," + item.MinNumberOfPlayers + item.MaxNumberOfPlayers + "," + item.Language + "," + item.Category + "," + item.Condition + "," + item.Price + "," + item.Stock).ToList();
        //}
    }
}
