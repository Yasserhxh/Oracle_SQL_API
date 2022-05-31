using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("CENTRE")]
    public class CENTRE
    {
        public string COD_PROV { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public string CODCT { get;set ; }
        public string LIBCT { get;set ; }
        public string TYP_CT { get;set ; }
        public string UNITE { get;set ; }
        public string CATEG { get;set ; }
        public string NATURE { get;set ; }
        public string CODEGEST { get;set ; }
    }
}
