namespace AnimalCareApplication.Patterns.Decorator
{
    public abstract class RendezVousDecorator : IRendezVousComponent
    {
        protected IRendezVousComponent _component;

        public RendezVousDecorator(IRendezVousComponent component)
        {
            _component = component;
        }

        public virtual void Executer()
        {
            _component.Executer();
        }
    }
}