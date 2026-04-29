
# AnimalCare

AnimalCare est une application web de gestion de clinique vétérinaire développée avec ASP.NET Core MVC.

## Description

L’application permet de gérer les utilisateurs, les animaux, les vétérinaires, les horaires, les rendez-vous et les notifications.  
Elle est conçue pour faciliter le travail d’une clinique vétérinaire avec les informations dans une seule plateforme.

## Technologies utilisées

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- HTML / CSS
- Bootstrap
- JavaScript
- Visual Studio

## Rôles de l’application

L’application contient plusieurs types d’utilisateurs :

- Administrateur
- Vétérinaire
- Client
- Récéptionnsite

Chaque utilisateur accède à des fonctionnalités différentes selon son rôle.

## Fonctionnalités principales

### Administrateur

L’administrateur peut :

- gérer les utilisateurs
- gérer les vétérinaires
- consulter les rendez-vous
- supprimer ou modifier des informations
- recevoir des notifications

### Vétérinaire

Le vétérinaire peut :

- consulter ses rendez-vous
- gérer ses horaires
- voir les animaux associés à ses rendez-vous
- recevoir des notifications liées aux rendez-vous

### Client

Le client peut :

- consulter ses animaux
- prendre un rendez-vous
- voir ses rendez-vous
- modifier ou annuler un rendez-vous selon les règles prévues

## Gestion des animaux

L’application permet d’ajouter, modifier, supprimer et afficher les animaux.

Chaque animal est lié à un propriétaire.

Les informations principales d’un animal sont :

- nom
- espèce
- race
- âge
- propriétaire

## Gestion des vétérinaires

L’application permet de gérer les vétérinaires de la clinique.

Chaque vétérinaire possède :

- un nom
- un prénom
- une spécialité
- des horaires de disponibilité

## Gestion des horaires

Les vétérinaires peuvent ajouter leurs disponibilités.

Un horaire contient :

- le jour
- l’heure de début
- l’heure de fin
- le vétérinaire associé

Cette partie permet d’éviter les rendez-vous en dehors des heures disponibles.

## Gestion des rendez-vous

La gestion des rendez-vous est une partie centrale de l’application.

Un rendez-vous contient :

- une date
- une heure
- un animal
- un vétérinaire
- un statut
- un état

Le système vérifie les disponibilités avant d’ajouter un rendez-vous.

Cela permet d’éviter les conflits d’horaires.

## Statut et état du rendez-vous

L’application utilise deux notions différentes :

### Statut

Le statut indique le niveau du rendez-vous :

- Normal
- Urgent

Par exemple, un rendez-vous prévu pour la journée actuelle peut être considéré comme urgent.

### État

L’état indique la situation du rendez-vous :

- Prévu
- Confirmé
- Annulé

Cela permet de mieux suivre l’évolution des rendez-vous.

## Notifications

L’application contient un système de notifications.

Quand un rendez-vous est ajouté, modifié ou supprimé, une notification peut être créée pour informer les utilisateurs concernés.

Les notifications peuvent être vues par :

- l’administrateur
- le vétérinaire concerné

## Design Patterns utilisés

### Strategy

Le pattern Strategy permet de changer une logique sans modifier tout le code.

Dans AnimalCare, il est utilisé dans la gestion des rendez-vous.

Il permet de déterminer automatiquement le statut d’un rendez-vous :

- si la date du rendez-vous est aujourd’hui donc le rendez-vou est considéré comme urgent
- sinon il est considéré comme normal

Ce pattern est utilisé au moment de la création du rendez-vous dans le contrôleur des rendez-vous (RendezVousController).


### Observer

Le pattern Observer permet d’avertir automatiquement plusieurs parties lorsqu’un événement se produit.

Dans AnimalCare, il est utilisé pour gérer les notifications liées aux rendez-vous.

Il est déclenché lorsque :

- un rendez-vous est créé
- un rendez-vous est modifié
- un rendez-vous est supprimé

Les utilisateurs concernés (administrateur, vétérinaire) reçoivent une notification.


### Decorator

Le pattern Decorator permet d’ajouter une fonctionnalité sans modifier directement la classe principale.

Dans AnimalCare, il est utilisé pour ajouter une fonctionnalité de rappel.

Par exemple :

- envoyer un rappel avant un rendez-vous
- ajouter une notification supplémentaire sans modifier la logique principale


### Singleton

Le pattern Singleton permet d’avoir une seule instance d’un objet dans toute l’application.

Par exemple :

- enregistrer les actions importantes comme la suppression d’un rendez-vous
- garder une seule instance accessible partout

Ce pattern est utilisé avec la classe Singleton (Logger) appelée dans les contrôleurs.

## Architecture du projet

Le projet suit l’architecture **MVC (Model - View - Controller)**.

### Model

Les modèles représentent les données de l’application.

Exemples :

- Animal  
- Veterinaire  
- RendezVous  
- Horaire  
- Utilisateur  
- Notification  


### View

Les vues représentent les pages affichées à l’utilisateur.

Elles permettent :

- d’afficher les formulaires  
- d’afficher les listes  
- d’afficher les détails  


### Controller

Les contrôleurs gèrent la logique entre les modèles et les vues.

Ils :

- reçoivent les actions de l’utilisateur  
- traitent les données  
- retournent les pages nécessaires  

Exemples :

- RendezVousController  
- VeterinairesController  
- HorairesController  
- AnimalsController  


## Base de données

La base de données contient plusieurs tables principales :

- Utilisateurs  
- Animaux  
- Vétérinaires  
- RendezVous  
- Horaires  
- Notifications  

### Relations importantes

- un propriétaire peut avoir plusieurs animaux  
- un animal peut avoir plusieurs rendez-vous  
- un vétérinaire peut avoir plusieurs horaires  
- un vétérinaire peut avoir plusieurs rendez-vous  
