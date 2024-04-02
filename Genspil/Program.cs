using System.Data;

namespace Genspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameList game = new();

            // Menu rykkes til en class Menu
            bool exit = false;
            int inputMenu;

            do
            {

                Console.WriteLine("Genspil's lagersystem");
                Console.WriteLine("1. Vis lagerstatus");
                Console.WriteLine("2. Søg efter spil");
                Console.WriteLine("3. Tilføj spil");
                Console.WriteLine("4. Fjern spil");
                Console.WriteLine("5. Opdater spil");
                Console.WriteLine("6. Tilføj ordre");


                Console.WriteLine("\n(Tryk menupunkt eller 0 for at afslutte)");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || !int.TryParse(input, out inputMenu))
                {
                    inputMenu = -1;  // Set to -1 if input is empty or can't be parsed
                }


                switch (inputMenu)
                {
                    case -1:
                        goto default;
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        game.ShowGameItems();
                        break;
                    case 2:
                        game.SearchAll();
                        break;
                    case 3:
                        game.AddGame();
                        game.WriteFile();
                        break;
                    case 4:
                        game.ShowGameItems();
                        game.Delete();
                        //game.WriteFile();
                        break;
                    case 5:
                        game.ShowGameItems();
                        game.Update();
                        game.WriteLineToFile();
                        break;
                    case 6:
                        game.ReadFile();
                        game.ShowLines();
                        break;
                    default:
                        Console.Write("Forkert input, prøv igen!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();


            } while (!exit);


            // Liste af metoder
            //game.AddGame();
            //game.WriteFile();
            //game.WriteLineToFile();
            //game.ReadFile();
            //game.ShowGameItems();
            //game.Delete();
            //game.SearchAll();
            //game.Update();

            Console.ReadKey();
        }
    }
}
