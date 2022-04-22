using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("RELEVE_EAU")]

    public class RELEVE_EAU
    {
        public DateTime DATE_REL { get; set; }
        public int IDX { get; set; }
        public int? ESTIM { get; set; }
        public string ETAT_CTR { get; set; }
        public int? PROD_GRV { get; set; }
        public int? PROD_GAS { get; set; }
        public string NUM_CTR { get; set; }
        public string CODCT { get; set; }
        public string CODE_CAUSE { get; set; }
        public int? VOLUME { get; set; }
        public int? TOTAL { get; set; }
        public string CREE_PAR { get; set; }
        public string MODI_PAR { get; set; }
        public DateTime? DATECREA { get; set; }
        public DateTime? DATEMODI { get; set; }
        public string IMG_REL { get; set; }
        public string INST_CPT { get; set; }
        public string LIB_CPT { get; set; }
        public string STATUT_REL { get; set; }
        public string MOTIF { get; set; }
        public string COHERENCE { get; set; }
    }
}
