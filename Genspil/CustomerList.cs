using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    public class CustomerList
    {
        private List<CustomerItem> _customerItems = new List<CustomerItem>();
        private List<string> _lines = new List<string>();

        private string _path = @"c:\temp\customerItems.txt";

    }
}
