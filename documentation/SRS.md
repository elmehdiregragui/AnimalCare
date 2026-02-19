# Software Requirements Specification (SRS)
## Projet : Site web de clinique vétérinaire

---

## 1. Introduction
Ce document décrit les exigences du site web de clinique vétérinaire
développé dans le cadre du cours Software Engineering. Le système vise
à soutenir la gestion des rendez-vous, des animaux, des propriétaires
et des vétérinaires.

---

## 2. Objectif du système
L’objectif principal du système est de fournir une plateforme web
permettant une gestion centralisée et efficace des activités d’une
clinique vétérinaire.

---

## 3. Utilisateurs du système
- Administrateur : gestion globale du système
- Réceptionniste : gestion des rendez-vous et coordination des horaires
- Vétérinaire : consultation des rendez-vous et du planning
- Client / Propriétaire : consultation et prise de rendez-vous

---

## 4. Fonctionnalités principales
- Gestion des rendez-vous vétérinaires
- Gestion des animaux
- Gestion des propriétaires
- Gestion des vétérinaires
- Gestion des horaires des vétérinaires
- Consultation du planning

---

## 5. Limites du système
Les fonctionnalités avancées telles que la facturation, les paiements
en ligne et les notifications automatiques ne sont pas incluses dans
cette première version du système.

---

## 6. Contraintes
- Projet académique
- Application web
- Respect des bonnes pratiques de génie logiciel
- Développement progressif selon les phases du cours

## 7. Exigences Fonctionnelles (FR)

FR-01 : Le système doit permettre à la réceptionniste de créer, modifier et annuler un rendez-vous.

FR-02 : Le système doit vérifier la disponibilité du vétérinaire avant confirmation d’un rendez-vous.

FR-03 : Le système doit permettre d’associer un animal à un propriétaire.

FR-04 : Le vétérinaire doit pouvoir consulter son planning par jour et par semaine.

FR-05 : L’administrateur doit pouvoir gérer les comptes utilisateurs (création, activation/désactivation, gestion des rôles).

---

## 8. Exigences Non Fonctionnelles (NFR)

NFR-01 : Authentification basée sur les rôles.

NFR-02 : Temps de réponse inférieur à 3 secondes pour les pages principales.

NFR-03 : Application accessible via navigateur web moderne.

NFR-04 : Création d’un rendez-vous en maximum 4 étapes.

NFR-05 : Respect d’une architecture MVC avec séparation des responsabilités.

---

## 9. Périmètre (Exclusions)

Le système ne comprend pas :

- Paiements en ligne
- Facturation automatisée
- Notifications SMS ou email
- Gestion du stock de médicaments
- Intégration d’un calendrier externe
