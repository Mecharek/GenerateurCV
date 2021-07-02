using System.ComponentModel.DataAnnotations.Schema;

namespace GenerateurCV.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public string Poste { get; set; }
        public string Description { get; set; }
        public string Periode { get; set; }
        [ForeignKey("CVid")]
        public virtual CV CV { get; set; }
        public int CVid { get; set; }
    }
}