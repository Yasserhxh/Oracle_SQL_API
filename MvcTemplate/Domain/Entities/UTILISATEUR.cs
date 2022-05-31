using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("UTILISATEUR")]
    public class UTILISATEUR
    {
        [Key]
        public int CODUTIAP { get; set; }
        public string NOMUTISY { get; set; }
        public string NOM_PREN { get; set; }
        public DateTime? DATEEFFE { get; set; }
        public DateTime? DATEABRO { get; set; }
        public string NOM_POST { get; set; }
        public string ADRE__IP { get; set; }
        public string INDVERPI { get; set; }
        public DateTime? DATEPASS { get; set; }
        public string INDIACTI { get; set; }
        public string FONCTUTIL { get; set; }
        public string CODEENTI { get; set; }
        public string TYPEENTI { get; set; }
        public string PROFIL { get; set; }
        public string MAILOFFICE { get; set; }
        public string CODECENTRE { get; set; }
    }
}
