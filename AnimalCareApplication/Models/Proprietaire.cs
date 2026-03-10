using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimalCareApplication.Models
{
    public partial class Proprietaire
    {
        [Key]
        public int IdProprietaire { get; set; }  

        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MotDePasse { get; set; } = string.Empty;

        public virtual ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
