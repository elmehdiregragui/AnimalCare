using AnimalCareApplication.Models;

namespace AnimalCareApplication.Patterns.Strategy
{
    public class RendezVousUrgentStrategy : IRendezVousStrategy
    {
        public void AppliquerStrategie(RendezVou rendezVous)
        {
            rendezVous.Statut = "Urgent";
        }
    }
}