using System;
using System.Collections.Generic;

namespace AnimalCareApplication.Models;

public partial class VRendezVousDetail
{
    public int IdRendezVous { get; set; }

    public DateTime DateRv { get; set; }

    public TimeSpan Heure { get; set; }

    public string Statut { get; set; } = null!;

    public string NomAnimal { get; set; } = null!;

    public string NomProprietaire { get; set; } = null!;

    public string PrenomProprietaire { get; set; } = null!;

    public string NomVeterinaire { get; set; } = null!;
    
    public string PrenomVeterinaire { get; set; } = null!;
}
