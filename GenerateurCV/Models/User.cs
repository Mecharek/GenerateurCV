using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenerateurCV.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Adresse { get; set; }
        public int Telephone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Photo { get; set; }
        public virtual List<CV> Cvs { get; set; }

        public User()
        {
            Cvs = new List<CV>();
        }
    }
}