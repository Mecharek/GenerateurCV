using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GenerateurCV.Models
{
    public class CV
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual List<Experience> Experiences { get; set; }
        public virtual List<FormationDiplome> FormationsDiplomes { get; set; }
        public virtual List<Loisir> Loisirs { get; set; }

        public CV()
        {
            Experiences = new List<Experience> ();
            FormationsDiplomes = new List<FormationDiplome>();
            Loisirs = new List<Loisir>();
        }
    }
}