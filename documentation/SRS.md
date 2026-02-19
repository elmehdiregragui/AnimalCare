# Software Requirements Specification (SRS)

## 1. Introduction

Ce document décrit les exigences du système AnimalCare, une application web de gestion d’une clinique vétérinaire.

---

## 2. Exigences Fonctionnelles (FR)

FR-01 : Le système doit permettre à la réceptionniste de créer, modifier et annuler un rendez-vous.

FR-02 : Le système doit vérifier la disponibilité du vétérinaire avant confirmation d’un rendez-vous.

FR-03 : Le système doit permettre d’associer un animal à un propriétaire.

FR-04 : Le vétérinaire doit pouvoir consulter son planning par jour et par semaine.

FR-05 : L’administrateur doit pouvoir gérer les comptes utilisateurs (création, activation/désactivation, gestion des rôles).

---

## 3. Exigences Non Fonctionnelles (NFR)

NFR-01 : Authentification basée sur les rôles.

NFR-02 : Temps de réponse inférieur à 3 secondes pour les pages principales.

NFR-03 : Application accessible via navigateur web moderne.

NFR-04 : Création d’un rendez-vous en maximum 4 étapes.

NFR-05 : Respect d’une architecture MVC avec séparation des responsabilités.

---

## 4. Périmètre (Exclusions)

Le système ne comprend pas :

- Paiements en ligne
- Facturation automatisée
- Notifications SMS ou email
- Gestion du stock de médicaments
- Intégration d’un calendrier externe
