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

    }
}
