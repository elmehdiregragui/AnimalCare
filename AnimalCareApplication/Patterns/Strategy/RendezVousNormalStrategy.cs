using AnimalCareApplication.Models;

namespace AnimalCareApplication.Patterns.Strategy
{
    public class RendezVousNormalStrategy : IRendezVousStrategy
    {
        public void AppliquerStrategie(RendezVou rendezVous)
        {
            rendezVous.Statut = "Normal";
        }
    }
}