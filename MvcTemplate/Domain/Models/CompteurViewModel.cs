using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CompteurViewModel
    {
        public string Code_Centre { get; set; }
        public string Installation { get; set; }
        public string Libelle { get; set; }
        public string Code_Compteur { get; set; }
        public int Index { get; set; }    
    }
}
