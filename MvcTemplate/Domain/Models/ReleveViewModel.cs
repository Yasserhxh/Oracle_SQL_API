using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ReleveViewModel
    {
        public int index { get; set; }
        public string installation { get; set; }
        public string libelle { get; set; }
        public string numCompteur { get; set; }
        public string centre { get; set; }
        public string imageJson { get; set; }
        public int? estimation { get; set; }
        public int volume { get; set; }
    }
}
