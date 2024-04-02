using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    public class CustomerItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        //public string City { get; set; }
        //public string PostalCode { get; set; }
        //public string Country { get; set; }
        public string Phone { get; set; }


        public CustomerItem(string name, string address, string phone) 
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Phone = phone;
        }



    }
}
