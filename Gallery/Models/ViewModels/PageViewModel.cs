using System.Collections.Generic;

namespace Gallery.Models.ViewModels
{
    public class PageViewModel
    {
        public List<string> Paths { get; set; }

        public int Current { get; set; }

        public int PageSize { get; set; }
                     
        public int Records { get; set; } 

        public int Pages { get; set; }
    }
}