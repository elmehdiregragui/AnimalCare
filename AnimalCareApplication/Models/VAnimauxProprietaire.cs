using System;
using System.Collections.Generic;

namespace AnimalCareApplication.Models;

public partial class VAnimauxProprietaire
{
    public int IdAnimal { get; set; }

    public string NomAnimal { get; set; } = null!;

    public string Espece { get; set; } = null!;

    public string? Race { get; set; }

    public int IdProprietaire { get; set; }

    public string NomProprietaire { get; set; } = null!;

    public string PrenomProprietaire { get; set; } = null!;
}
