#  Rapport – Projet AnimalCare

## 1. Introduction

Le projet AnimalCare est une application web développée dans le cadre du cours implémentation d'un système d'information.  
Son objectif principal est de faciliter la gestion d’une clinique vétérinaire en donnant toutes les informations liées aux utilisateurs, aux animaux, aux vétérinaires et aux rendez-vous.

Selon le SRS, le système vise à « soutenir la gestion des rendez-vous, des animaux, des propriétaires et des vétérinaires »

Ce rapport présente :
- le cahier des charges
- la solution proposée
- les choix techniques et architecturaux
- les design patterns utilisés
- les contraintes du projet



## 2. Cahier des charges (SRS)

### 2.1 Objectif du système

L’objectif principal est de fournir une plateforme web permettant une gestion centralisée et efficace des activités d’une clinique vétérinaire.

Cela signifie que :
- toutes les données sont présentes dans un seul système
- les utilisateurs peuvent accéder facilement aux informations
- la gestion devient plus rapide et plus organisée



### 2.2 Utilisateurs du système

Le système gère plusieurs types d’utilisateurs :

- Administrateur : gestion complète du système
- Réceptionniste : gestion des rendez-vous
- Vétérinaire : consultation du planning
- Client : prise et consultation de rendez-vous



### 2.3 Fonctionnalités principales

Les fonctionnalités principales sont :

- Gestion des rendez-vous
- Gestion des animaux
- Gestion des propriétaires
- Gestion des vétérinaires
- Gestion des horaires
- Consultation du planning

 Ces fonctionnalités représentent le cœur du système.



### 2.4 Exigences fonctionnelles importantes

#### FR-01 : Gestion des rendez-vous
- Création, modification et suppression
- Vérification des conflits
- Association à un animal et un vétérinaire

#### FR-02 : Vérification des disponibilités
- Contrôle automatique des horaires
- Aucun conflit autorisé

#### FR-03 : Gestion des animaux
- Association avec un propriétaire
- Données obligatoires (nom, espèce…)

#### FR-04 : Consultation du planning
- Accès uniquement au vétérinaire concerné



## 3. Contraintes du projet

### 3.1 Contraintes générales
- Projet académique
- Temps limité
- Respect des bonnes pratiques
- Développement progressif

### 3.2 Contraintes techniques
- Application web obligatoire
- Architecture MVC

### 3.3 Contraintes non fonctionnelles
- Authentification sécurisée
- Temps de réponse < 3 secondes
- Interface simple (max 4 étapes pour créer un rendez-vous)



## 4. Solution proposée

### 4.1 Choix du type d’application (ADR-001)

Selon l’ADR-001, le choix s’est porté sur une application web.

**Décision :**
 Le système sera développé sous forme d’une application web 

### Raisons :
- accès simple via navigateur
- aucune installation
- centralisation des données

### Alternatives rejetées :
- application desktop (plus complexe à maintenir)

 Ce choix est adapté au contexte académique et aux besoins des utilisateurs.



### 4.2 Conséquences de ce choix

#### Positives :
- accès depuis n’importe quelle machine
- facilité de mise à jour
- système centralisé

#### Négatives :
- dépendance à internet
- sécurité à gérer



## 5. Architecture du système (ADR-002)

### 5.1 Choix de l’architecture

Selon l’ADR-002 :

 L’application adopte une architecture MVC combinée aux Design Patterns

### Pourquoi MVC ?
- séparation des responsabilités
- code plus organisé
- facilité de maintenance



### 5.2 Structure MVC

- **Model** : données (Animal, RendezVous, Client, Vétérinaire , récéptionniste)
- **View** : interface utilisateur
- **Controller** : logique métier
- 
 Cette structure rend le projet plus propre et évolutif.


## 6. Design Patterns utilisés

### 6.1 Singleton
- utilisé pour le Logger
- permet une seule instance

 évite la duplication et centralise les logs



### 6.2 Observer
- utilisé pour les notifications
- déclenché lors d’un changement comme dans les rendez-vous

permet de notifier automatiquement les utilisateurs


### 6.3 Strategy
- utilisé pour définir le statut d’un rendez-vous
- exemple : urgent ou normal

 permet de changer le comportement facilement

---

### 6.4 Decorator
- utilisé pour ajouter des fonctionnalités
- exemple : rappel de rendez-vous

 permet d’ajouter des fonctionnalités sans modifier le code existant


## 7. Impact des choix techniques

### Positifs :
- code structuré
- facilité de maintenance

### Négatifs :
- complexité plus élevée
- plus de fichiers
- plus de code


## 8. Entités métier

Les principales entités sont :

- Utilisateur (abstraite)
- Proprietaire
- Animal
- Veterinaire
- RendezVous
- Horaire

  ## 9. Diagrammes

### 9.1 Diagramme de classes

![Diagramme de classes](DiagrammeDeClasseAnimalCare.jpg)

Le diagramme de classes représente les différentes classes du système, leurs attributs, leurs méthodes ainsi que les relations entre elles.  
On peut y voir les relations d’héritage (comme la classe Utilisateur abstraite) ainsi que les associations entre les entités comme Animal, Vétérinaire et RendezVous.

---

### 9.2 Diagramme de cas d’utilisation

![Diagramme de cas d'utilisation](DiagrammeDeCasDutilisationAnimalCare.jpg)

Le diagramme de cas d’utilisation montre les différents acteurs du système (Administrateur, Réceptionniste, Vétérinaire, Client) ainsi que les actions qu’ils peuvent effectuer.  
Il permet de comprendre rapidement les fonctionnalités du système et les interactions entre les utilisateurs et l’application.

---

### 9.3 Diagramme de séquence

![Diagramme de séquence](DiagrammeSequenceAnimalCare.jpg)

Le diagramme de séquence illustre le déroulement d’une interaction dans le système, par exemple la création d’un rendez-vous.  
Il montre les échanges entre les différentes parties du système comme l’utilisateur, le contrôleur, le service métier et la base de données.  
Ce diagramme permet de comprendre l’ordre des opérations et la communication entre les composants.

---

### 9.4 Diagramme de composants

![Diagramme de composants](DiagrammeComposantsAnimalCare.jpg)

Le diagramme de composants représente l’architecture globale de l’application.  
Il montre les différents modules du système comme :
- l’interface utilisateur (Views)
- les contrôleurs (Controllers)
- la logique métier (Services)
- la base de données

Ce diagramme permet de visualiser comment les différentes parties du système communiquent entre elles.


## 10. Facilité d’exécution

Le projet est conçu pour être facile à exécuter.

### Étapes :
1. Cloner le projet
2. Ouvrir avec Visual Studio
3. Lancer le fichier `.sln`
4. Exécuter l’application


---

## 11. Conclusion

Le projet AnimalCare répond aux exigences du cahier des charges en proposant une solution web adaptée.

Les choix techniques (MVC + Design Patterns) permettent :
- une meilleure organisation
- une bonne maintenabilité
- une évolution facile du système

Malgré une complexité plus élevée, ces choix sont justifiés et montrent une bonne compréhension des concepts de génie logiciel.
