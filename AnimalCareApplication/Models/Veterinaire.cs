namespace AnimalCareApplication.Models
{
    public partial class Veterinaire
    {
        public int IdVeterinaire { get; set; }

        public int IdUtilisateur { get; set; }

        public string Specialite { get; set; } = string.Empty;

        
        public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;

        public virtual ICollection<Horaire> Horaires { get; set; } = new List<Horaire>();
        public virtual ICollection<Historique> Historiques { get; set; } = new List<Historique>();
        public virtual ICollection<RendezVou> RendezVous { get; set; } = new List<RendezVou>();
    }
}
