using System;
using System.Collections.Generic;

namespace AnimalCareApplication.Models;

public partial class Horaire
{
    public int IdHoraire { get; set; }

    public string Jour { get; set; } = null!;

    public TimeSpan HeureDebut { get; set; }

    public TimeSpan HeureFin { get; set; }

    public int IdVeterinaire { get; set; }

    public virtual Veterinaire IdVeterinaireNavigation { get; set; } = null!;
}
