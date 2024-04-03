using System.Data;

namespace Genspil
{
    internal class Program
    {
        static void Main(string[] args)
        {


            GameList game = new();

            game.CreateFile();

            bool exit = false;
            int inputMenu;

            do
            {
                Console.Clear();

                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\r\n                                                                                      \r\n ░▒▓██████▓▒░░▒▓████████▓▒░▒▓███████▓▒░ ░▒▓███████▓▒░▒▓███████▓▒░░▒▓█▓▒░▒▓█▓▒░        \r\n░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░        \r\n░▒▓█▓▒░      ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░        \r\n░▒▓█▓▒▒▓███▓▒░▒▓██████▓▒░ ░▒▓█▓▒░░▒▓█▓▒░░▒▓██████▓▒░░▒▓███████▓▒░░▒▓█▓▒░▒▓█▓▒░        \r\n░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░▒▓█▓▒░        \r\n░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░▒▓█▓▒░        \r\n ░▒▓██████▓▒░░▒▓████████▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓███████▓▒░░▒▓█▓▒░      ░▒▓█▓▒░▒▓████████▓▒░ \r\n                                                                                      \r\n                                                                                      \r\n");
                Console.ResetColor();


                Console.WriteLine("Genspil's lagersystem");
                Console.WriteLine("1. Vis alle brætspil");
                Console.WriteLine("2. Tilføj brætspil");
                Console.WriteLine("3. Opdatér brætspil");
                Console.WriteLine("4. Fjern brætspil");
                Console.WriteLine("5. Søg brætspil");
                Console.WriteLine("------------------------------");
                Console.WriteLine("6. Vis alle forespørgsler");
                Console.WriteLine("7. Tilføj forespørgsel");
                Console.WriteLine("8. Opdatér forespørgsel");
                Console.WriteLine("9. Fjern forespørgsel");
                Console.WriteLine("10. Søg forespørgsel");

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
                    case 1: // vis
                        game.ShowGameItems();
                        break;
                    case 2: // Add
                        game.AddGame();
                        game.WriteFile();
                        Console.WriteLine("Spillet er nu gemt.");
                        break;
                    case 3: // Update
                        game.ShowGameItems();
                        game.Update();
                        game.WriteLineToFile();
                        Console.WriteLine("Spillet er opdateret");
                        break;
                    case 4: // Delete
                        game.ShowGameItems();
                        game.Delete();
                        game.WriteFile();
                        Console.WriteLine("Spillet er slettet");
                        break;
                    case 5: // Search
                        game.SearchAll();
                        break;
                    case 6:
                        Console.WriteLine("Vis alle forespørgsler - request.metode indsættes her");
                        break;
                    case 7:
                        Console.WriteLine("Tilføj forespørgsel - request.metode indsættes her");
                        break;
                    case 8:
                        Console.WriteLine("Opdatér forespørgsel - request.metode indsættes her");
                        break;
                    case 9:
                        Console.WriteLine("Fjern forespørgsel - request.metode indsættes her");
                        break;
                    case 10:
                        Console.WriteLine("Søg forespørgsel - request.metode indsættes her");
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
