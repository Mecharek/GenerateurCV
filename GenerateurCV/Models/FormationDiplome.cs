using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateurCV.Models
{
    public class FormationDiplome
    {
        public int Id { get; set; }
        public string Intitule { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [ForeignKey("CVid")]
        public virtual CV CV { get; set; }
        public int CVid { get; set; }
    }
}