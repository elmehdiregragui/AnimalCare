using System;
using System.Collections.Generic;

namespace AnimalCareApplication.Models;

public partial class Animal
{
    public int IdAnimal { get; set; }

    public string Nom { get; set; } = null!;

    public string Espece { get; set; } = null!;

    public string? Race { get; set; }

    public int? Age { get; set; }

    public int IdProprietaire { get; set; }

    public virtual ICollection<Historique> Historiques { get; set; } = new List<Historique>();

    public virtual Proprietaire? IdProprietaireNavigation { get; set; } = null!;


    public virtual ICollection<RendezVou> RendezVous { get; set; } = new List<RendezVou>();
}
