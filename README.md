#  AnimalCare – Application de Gestion de Clinique Vétérinaire

##  Description
AnimalCare est une application web développée en ASP.NET Core MVC permettant la gestion d’une clinique vétérinaire.  
Le système permet de gérer les utilisateurs, les vétérinaires, les propriétaires, les animaux, les rendez-vous et les horaires.  

Le projet met plusieurs design patterns afin d'améliorer la structure du code.



#  Architecture du projet
L'application suit l'architecture MVC :

- Models : représentation des données
- Views : interface utilisateur
- Controllers : logique métier
- Patterns : implémentation des design patterns



#  Design Patterns utilisés
Les patterns suivants ont été implémentés dans le projet :

- Singleton
- Observer
- Decorator
- Strategy



# 1. Singleton Pattern

## Objectif
Garantir qu’une seule instance d’une classe existe dans toute l’application.

## Utilisation dans le projet
Le Singleton est utilisé pour centraliser un comportement global

## Extrait du code
```csharp
namespace AnimalCareApplication.Patterns.Singleton
{
    public class Singleton
    {
        private static Singleton _instance;
        private static readonly object _lock = new object();

        public static Singleton Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                    return _instance;
                }
            }
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}

## 2. Observer Pattern

### Objectif
Notifier automatiquement les utilisateurs lorsqu'un événement se produit.

### Utilisation dans le projet
Utilisé pour envoyer des notifications lors de la création ou modification d’un rendez-vous.

### Extrait du code

```csharp
namespace AnimalCareApplication.Patterns.Observer
{
    public interface IObserver
    {
        void Update(string message);
    }
}

namespace AnimalCareApplication.Patterns.Observer
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
}

using System.Collections.Generic;

namespace AnimalCareApplication.Patterns.Observer
{
    public class RendezVousSubject : ISubject
    {
        private readonly List<IObserver> _observers = new();
        private string _message;

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void SetMessage(string message)
        {
            _message = message;
            Notify();
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_message);
            }
        }
    }
}


