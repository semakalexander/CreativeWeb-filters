using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeWeb.Models
{
    public class ItemAndTypes
    {
      public  Item Item { get; set; }
        public List<string> Types { get; set; }
    }
}