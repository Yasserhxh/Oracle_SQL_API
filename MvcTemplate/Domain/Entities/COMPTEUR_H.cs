using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("COMPTEUR_H")]
    public class COMPTEUR_H
    {
        public string TYP_EXIST { get; set; }
        public string  NUM_SR { get; set; }
        public string  DIAM_CTR { get; set; }
        public DateTime DAT_DER_REP { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string NUM_CTR { get; set; }
        public DateTime DATE_POSE { get; set; }
        public decimal COEF_COMPT { get; set; }
        public string DISPOSIT { get; set; }
        public string NATURE { get; set; }
        public string ETAT_CTR { get; set; }
        public decimal IDX_DEP { get; set; }
        public DateTime DATE_DEP { get; set; }
        public string TYP_COMPT { get; set; }
        public string CODCT { get; set; }
        public string NUM_INST { get; set; }
        public string COD_CLT { get; set; }
        public string LIB_CTR { get; set; }
        public decimal IDX_MAX { get; set; }
        public string TYP_REL { get; set; }
        public string CODE_VILLE { get; set; }
        public string RECYCLAGE { get; set; }
        public string MARQUE { get; set; }
        public string TYPE_AB { get; set; }
        public string VILLE_ALIM { get; set; }
        public string CREE_PAR { get; set; }
        public DateTime DATECREA { get; set; }
        public string MODI_PAR { get; set; }
        public DateTime DATEMODI { get; set; }
    }
}
