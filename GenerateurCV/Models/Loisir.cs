using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateurCV.Models
{
    public class Loisir
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [ForeignKey("CVid")]
        public virtual CV CV { get; set; }
        public int CVid { get; set; }
    }

}