using AnimalCareApplication.Models;

namespace AnimalCareApplication.Patterns.Strategy
{
    public interface IRendezVousStrategy
    {
        void AppliquerStrategie(RendezVou rendezVous);
    }
}