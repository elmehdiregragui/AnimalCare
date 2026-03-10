using System;
using System.Collections.Generic;

namespace AnimalCareApplication.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
