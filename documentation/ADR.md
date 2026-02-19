# ADR-001 — Adoption de l’architecture MVC et du pattern Repository

Statut : Accepted  
Date : 2025-12-XX  
Décideurs : Équipe AnimalCare  
Contexte projet : Application web de gestion d’une clinique vétérinaire (AnimalCare)

---

## 1. Contexte

### Problème / besoin

Le projet AnimalCare nécessite une architecture claire permettant :

- La séparation entre l’interface utilisateur, la logique métier et l’accès aux données
- Une maintenance facilitée
- Une évolution progressive du système

### Contraintes

- Projet académique
- Temps limité
- Utilisation obligatoire d’ASP.NET Core
- Base de données SQL Server

### Forces en présence

- Simplicité d’implémentation
- Maintenabilité
- Respect des bonnes pratiques de génie logiciel
- Testabilité

---

## 2. Décision

Nous choisissons d’utiliser l’architecture ASP.NET Core MVC combinée avec le pattern Repository.

Pour :

- Séparer clairement les responsabilités
- Réduire le couplage entre la logique métier et la base de données
- Améliorer la lisibilité et l’organisation du code

---

## 3. Alternatives considérées

### Option A — Architecture monolithique sans séparation claire

Avantages :
- Implémentation rapide
- Moins de fichiers

Inconvénients :
- Code difficile à maintenir
- Couplage fort entre les composants
- Peu évolutif

---

### Option B — Architecture en couches (MVC + Repository)

Avantages :
- Séparation des responsabilités
- Meilleure organisation
- Code plus testable
- Respect des principes SOLID

Inconvénients :
- Structure plus complexe
- Légèrement plus longue à mettre en place

---

## 4. Justification

Cette décision permet :

- Une meilleure évolutivité du système
- Une maintenance plus simple
- Une meilleure compréhension du projet par les membres de l’équipe
- Le respect des bonnes pratiques en génie logiciel

---

## 5. Conséquences

### Positives

- Code structuré
- Responsabilités bien définies
- Réduction du couplage

### Négatives / Risques

- Complexité légèrement plus élevée
- Nécessite une bonne organisation

### Impact sur l’architecture / le code

- Utilisation de Controllers
- Séparation Models / Services / Data Access
- Introduction d’un Repository pour la gestion des données

---

## 6. Plan d’implémentation

- Étape 1 : Création de la structure MVC
- Étape 2 : Mise en place du DbContext
- Étape 3 : Implémentation des Repositories
- Étape 4 : Implémentation des Controllers

---

## 7. Validation

La décision est validée si :

- Les Controllers ne contiennent pas d’accès direct à la base de données
- Les opérations CRUD passent par une couche Repository
- L’application respecte la séparation des responsabilités

---

## 8. Liens

UML : documentation/composants.png  
Issue/Tâche : Gestion des patients  
Référence : Cours de génie logiciel – Design Patterns & Architecture
