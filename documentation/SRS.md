# Software Requirements Specification (SRS)
## Projet : Site web de clinique vétérinaire

## 1. Introduction
Ce document décrit les exigences du site web de clinique vétérinaire
développé dans le cadre du cours Software Engineering. Le système vise
à soutenir la gestion des rendez-vous, des animaux, des propriétaires
et des vétérinaires.


## 2. Objectif du système
L’objectif principal du système est de fournir une plateforme web
permettant une gestion centralisée et efficace des activités d’une
clinique vétérinaire.


## 3. Utilisateurs du système
- Administrateur : gestion globale du système
- Réceptionniste : gestion des rendez-vous et coordination des horaires
- Vétérinaire : consultation des rendez-vous et du planning
- Client / Propriétaire : consultation et prise de rendez-vous


## 4. Fonctionnalités principales
- Gestion des rendez-vous vétérinaires
- Gestion des animaux
- Gestion des propriétaires
- Gestion des vétérinaires
- Gestion des horaires des vétérinaires
- Consultation du planning


## 5. Contraintes
- Projet académique
- Application web
- Respect des bonnes pratiques de génie logiciel
- Développement progressif selon les phases du cours

## 6. Exigences Fonctionnelles (FR)

### FR-01 : Le système doit permettre à la réceptionniste de créer, modifier et annuler un rendez-vous.

**Règles métier :**

- Un rendez-vous doit être associé à un animal existant.
- Un rendez-vous doit être associé à un vétérinaire existant.
- La date du rendez-vous ne peut pas être dans le passé.
- Deux rendez-vous ne peuvent pas se chevaucher pour un même vétérinaire.
- Le statut du rendez-vous doit être : Planifié, Confirmé, Annulé ou Terminé.

**Validations :**

- Date obligatoire
- Heure obligatoire
- Sélection du vétérinaire obligatoire
- Sélection de l’animal obligatoire

**Cas d’erreur possibles :**

- Conflit d’horaire
- Champs obligatoires non remplis
- Vétérinaire indisponible



### FR-02 : Le système doit vérifier la disponibilité du vétérinaire avant confirmation d’un rendez-vous.

**Règles métier :**

- Le système doit vérifier les horaires enregistrés du vétérinaire.
- Un créneau ne peut être proposé que s’il est inclus dans un horaire valide.
- Un vétérinaire ne peut pas avoir deux rendez-vous simultanés.
- La durée du rendez-vous doit respecter les plages horaires définies.

**Validations :**

- Vérification automatique des disponibilités
- Vérification de la cohérence date/heure

**Cas d’erreur possibles :**

- Aucun créneau disponible
- Chevauchement avec un autre rendez-vous


### FR-03 : Le système doit permettre d’associer un animal à un propriétaire.

**Règles métier :**

- Un animal doit être associé à un seul propriétaire.
- Un propriétaire peut posséder plusieurs animaux.
- Les informations de l’animal doivent être complètes (nom, espèce, âge minimum requis).

**Validations :**

- Nom de l’animal obligatoire
- Sélection d’un propriétaire existant obligatoire
- Espèce obligatoire

**Cas d’erreur possibles :**

- Propriétaire inexistant
- Données manquantes
- Tentative de duplication d’un animal déjà enregistré




### FR-04 : Le vétérinaire doit pouvoir consulter son planning .

**Règles métier :**

- Le vétérinaire ne peut consulter que son propre planning.
- Les rendez-vous doivent être affichés par ordre chronologique.
- Les rendez-vous annulés doivent être identifiables.
- L’affichage doit être possible en vue journalière et hebdomadaire.

**Validations :**

- Vérification du rôle utilisateur (vétérinaire)
- Filtrage automatique selon l’identifiant du vétérinaire connecté

**Cas d’erreur possibles :**

- Accès non autorisé
- Aucun rendez-vous planifié


## 7. Exigences Non Fonctionnelles (NFR)

### NFR-01 : Authentification basée sur les rôles.

**Description :**

Le système doit implémenter une authentification sécurisée avec gestion des rôles (Administrateur, Réceptionniste, Vétérinaire, Propriétaire).

**Contraintes :**

- Accès aux fonctionnalités restreint selon le rôle.
- Mot de passe sécurisé.
- Sessions protégées.

**Critères de validation :**

- Un utilisateur ne peut accéder qu’aux fonctionnalités correspondant à son rôle.
- Un utilisateur non authentifié ne peut accéder à aucune page protégée.
- Les tentatives d’accès non autorisées sont bloquées.



### NFR-02 : Temps de réponse inférieur à 3 secondes pour les pages principales.

**Description :**

Les pages principales (connexion, gestion des rendez-vous, consultation planning) doivent charger rapidement.

**Contraintes :**

- Optimisation des requêtes base de données.
- Limitation des traitements lourds côté serveur.

**Critères de validation :**

- Temps de chargement mesuré inférieur à 3 secondes en conditions normales.
- Aucun blocage ou ralentissement majeur lors de l’utilisation standard.


### NFR-03 : Application accessible via navigateur web moderne.

**Description :**

Le système doit être accessible via les navigateurs récents.

**Contraintes :**

- Compatibilité avec Chrome, Edge, Firefox.
- Interface responsive.

**Critères de validation :**

- L’application fonctionne sans erreur sur les navigateurs modernes.
- L’affichage reste cohérent sur différentes résolutions.



### NFR-04 : Création d’un rendez-vous en maximum 4 étapes.

**Description :**

Le processus de création d’un rendez-vous doit être simple et rapide.

**Contraintes :**

- Interface claire.
- Navigation intuitive.

**Critères de validation :**

- Le nombre d’écrans ou d’étapes ne dépasse pas 4.
- L’utilisateur peut compléter la procédure sans assistance.



### NFR-05 : Respect d’une architecture MVC avec séparation des responsabilités.

**Description :**

Le système doit respecter une architecture en couches basée sur MVC.

**Contraintes :**

- Séparation claire entre :
  - Modèle
  - Vue
  - Contrôleur
- Logique métier distincte de l’accès aux données.

**Critères de validation :**

- Présence d’un dossier Models, Views, Controllers.
- Absence de logique métier dans les vues.
- Utilisation d’un DbContext pour l’accès aux données.



## 8. Périmètre (Exclusions)

Le système ne comprend pas :

- Paiements en ligne
- Facturation automatisée
- Notifications SMS ou email
- Gestion du stock de médicaments
- Intégration d’un calendrier externe
