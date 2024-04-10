using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    public class RequestItem
    {
        //Egenskaben Id burde her hedde RequestNumber - vi retter det, hvis vi får tid.
        public int Id { get; set; }
        public string Title { get; set; }
        public string Edition { get; set; }
        public string Language { get; set; }
        public string CustomerName { get; set; }
        public string Reference { get; set; }
        //Det kunne være en ide med en egenskab mere, som hedder "comment".

        // Optional
        // DateTime Create
        // DateTime Update

        public RequestItem(int id, string title, string edition, string language, string customerName, string reference)
        {
            Id = id;
            Title = title;
            Edition = edition;
            Language = language;
            CustomerName = customerName;
            Reference = reference;
        }

        public RequestItem() 
        {
        }


    }
}
