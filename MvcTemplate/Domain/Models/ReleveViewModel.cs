using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ReleveViewModel
    {
        public string index { get; set; }
        public string installation { get; set; }
        public string libelle { get; set; }
        public string numCompteur { get; set; }
        public string centre { get; set; }
        public string date_Rel { get; set; }
        public string imageJson { get; set; }
        public int? estimation { get; set; }
        public int volume { get; set; }
        public string etat_rel { get; set; }
        public string coherence { get; set; }
        public string motif { get; set; }
        public string utilisateur { get; set; }
        public string type_saisie { get; set; }
    }
}
