using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    public class RequestItem
    {
        /// <summary>
        /// Det er ID'et
        /// </summary>
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Language { get; set; }

        // Optional
        // DateTime Create
        // DateTime Update

        /// <summary>
        /// Constructor til RequestItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerId"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="language"></param>
        public RequestItem(int id, Guid customerId, string title, string subtitle, string language)
        {
            Id = id;
            CustomerId = customerId;
            Title = title;
            Subtitle = subtitle;
            Language = language;
        }
    }
}
