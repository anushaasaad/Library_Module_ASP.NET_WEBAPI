using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace libraria_WebAPI.Models
{
    public class BookClass
    {
        public int book_Id { get; set; }
        public string book_name { get; set; }

        public string genre { get; set; }
        public string edition { get; set; }
        public string language { get; set; }
        public int pages { get; set; }
        public string description { get; set; }
        public string author_name { get; set; }
        public int actual_stock { get; set; }
        public int current_stock { get; set; }
        public int book_cost { get; set; }
        public string img_link { get; set; }

    }
}