using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalCareApplication.Models
{
    public class ClientRendezVousViewModel
    {
        public int IdAnimal { get; set; }
        public int IdVeterinaire { get; set; }
        public DateTime DateRv { get; set; }
        public TimeSpan Heure { get; set; }

        public int IdRendezVous { get; set; }


        public List<SelectListItem> Animaux { get; set; } = new();
        public List<SelectListItem> Veterinaires { get; set; } = new();
    }
}
