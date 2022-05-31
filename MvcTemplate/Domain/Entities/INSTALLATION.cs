using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("INSTALLATION")]
    public class INSTALLATION
    {
        public string ETAT_INST { get; set; }   
        public string TYP_EXIST { get; set; }   
        public string TYP_INST { get; set; }   
        public string S_ENERGIE { get; set; }   
        public string NUM_INST { get; set; }   
        public DateTime? DATE_SERV { get; set; }   
        public int? CAPACITE { get; set; }   
        public string LIB_INST { get; set; }   
        public string NATURE_INST { get; set; }   
        public int? DEBIT_EQ { get; set; }   
        public string CODCT { get; set; }   
        public string NUM_RESS { get; set; }   
        public string NUM_TRONC { get; set; }   
        public string NUM_AR { get; set; }   
        public string NUM_INSTP { get; set; }   
        public string TYPE_TRAIT { get; set; }   
        public int? PUISS_INST { get; set; }   
        public string COD_PROV { get; set; }   
        public int? COEF_RABAT { get; set; }   
        public string CREE_PAR { get; set; }   
        public DateTime? DATECREA { get; set; }   
        public string MODI_PAR { get; set; }   
        public DateTime? DATEMODI { get; set; }   
    }
}
