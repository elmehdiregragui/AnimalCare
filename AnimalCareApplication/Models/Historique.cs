using System;
using System.Collections.Generic;

namespace AnimalCareApplication.Models;

public partial class Historique
{
    public int IdHistorique { get; set; }

    public DateTime DateSoin { get; set; }

    public string Description { get; set; } = null!;

    public int IdAnimal { get; set; }
    
    public int IdVeterinaire { get; set; }

    public virtual Animal IdAnimalNavigation { get; set; } = null!;

    public virtual Veterinaire IdVeterinaireNavigation { get; set; } = null!;
}
