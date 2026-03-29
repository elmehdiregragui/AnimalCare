using LoggerSingleton = AnimalCareApplication.Patterns.Singleton.Singleton;

namespace AnimalCareApplication.Patterns.Decorator
{
    public class RappelRendezVousDecorator : RendezVousDecorator
    {
        public RappelRendezVousDecorator(IRendezVousComponent component)
            : base(component)
        {
        }

        public override void Executer()
        {
            base.Executer();
            LoggerSingleton.Instance.Log("Rappel envoyé au client la veille du rendez-vous");
        }
    }
}