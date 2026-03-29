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
## Exemple d'utilisation
Singleton.Instance.Log("Rendez-vous ajouté");
