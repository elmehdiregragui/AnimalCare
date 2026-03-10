using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AnimalCareApplication.Models
{
    public partial class RendezVou
    {
        public int IdRendezVous { get; set; }
        public DateTime DateRv { get; set; }
        public TimeSpan Heure { get; set; }
        public string Statut { get; set; } = string.Empty;
        public int IdAnimal { get; set; }
        public int IdVeterinaire { get; set; }

        [ValidateNever]
        public virtual Animal? IdAnimalNavigation { get; set; }

        [ValidateNever]
        public virtual Veterinaire? IdVeterinaireNavigation { get; set; }
    }
}
