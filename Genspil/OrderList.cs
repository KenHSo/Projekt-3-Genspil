using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    //UC1 Medarbejder bruger lagersystemet til salg
    //En kunde henvender sig om et bestemt spil, medarbejderen
    //benytter lagersystemet til at søge efter det pågældende spil,
    //som er på lager.Medarbejderen sælger spillet til kunden, og
    //opdaterer lagersystemet efterfølgende. Opdaterer lagerstatus ved
    //at fjerne spillet fra listen.

    //UC3 Medarbejder bruger lagersystemet til at indregistrere en forespørgelse
    //En kunde henvender sig om et bestemt spil, medarbejderen benytter lagersystemet
    //til at søge efter det pågældende spil, som ikke er på lager.Medarbejderen opdaterer
    //listen forespørgelser i lagersystemet, ved at skrive spillets navn, kundens navn
    //og kontaktoplysninger.



    // Søgning
    // Viser alle match / delvis match med id
    // Vælg AddOrder(), idstock--;
    // Hvis ingen match: AddRequest()


    // orders.txt //
    // requests.txt
    // customers.txt //
    // gameItems.txt

    // request: id, navn, tlf, add

    public class OrderList
    {

        private List<OrderItem> _orderItems = new List<OrderItem>();
        //private List<string> _lines = new List<string>();

        private string _path = @"c:\temp\orderItems.txt";

        private GameList game = new GameList();


        //AddOrder();
        //WriteFileOrder();


        // læs filen
        // liste _lines
        // hive parameter gameItemId ud
        public void AddOrder()
        {

            int gameItemSelect;
            int id = 0;
            Guid customerId = new Guid();

            if (!File.Exists(_path))
            {
                string createText = "";
                File.WriteAllText(_path, createText);
            }

            game.ReadFile();

            //int id = AddOrderId();
            //string customerId = CustomerId;
            //string gameItemId = brugerinput = gameItemId

            Console.WriteLine("Opret ordre: ");

            Console.Write("Vælg id på det spil du vil oprette en ordre på: ");
            int.TryParse(Console.ReadLine(), out gameItemSelect);
            
            string[] splitString = new string[3];
            
            splitString = game._lines[gameItemSelect].Split(',');

            int gameItemId = Convert.ToInt32(splitString[0]);

            _orderItems.Add(new OrderItem(id, customerId, gameItemId));

            foreach (OrderItem item in _orderItems)
            {
                Console.WriteLine($"{item.Id}, {item.CustomerId}, {item.GameItemId}");
            }

        }

        public void WriteFileOrder()
        {
            foreach (OrderItem item in _orderItems)
            {
                // This text is always added, making the file longer over time
                // if it is not deleted.
                string appendText = $"{item.Id},{item.CustomerId},{item.GameItemId}" + Environment.NewLine;
                File.AppendAllText(_path, appendText);
            }
        }



    }
}
