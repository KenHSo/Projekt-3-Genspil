using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    internal class OrderItem
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public int GameItemId { get; set; }

        // Optional
        // DateTime Create
        // DateTime Update
        // DateTime Sent

        public OrderItem(int id, Guid customerId, int gameitemid)
        {
            Id = id;
            CustomerId = customerId;
            GameItemId = gameitemid;
        }



    }
}
