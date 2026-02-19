# Architecture Decision Records  
ADR-001 — Choix du type d’application

**Statut :** Accepted  
**Date :** 2026-02-19  
**Décideurs :** Équipe du projet  
**Contexte projet :** Site web de clinique vétérinaire

---

## 1. Contexte
- **Problème / besoin :**  
La clinique vétérinaire doit gérer plusieurs types d’utilisateurs
(administrateur, réceptionniste, vétérinaire, client) ayant besoin
d’un accès simple et centralisé au système.

- **Contraintes :**  
Projet académique avec un temps limité, nécessité d’une solution accessible sans installation complexe.

- **Forces en présence :**  
Simplicité d’accès, maintenabilité, évolutivité et clarté de
l’architecture.

---

## 2. Décision
> Le système sera développé sous forme d’une application web.

- Je choisis : une application web  
- Pour : permettre un accès facile et centralisé à tous les utilisateurs

---

## 3. Alternatives considérées

### Option A — Application desktop
- **Avantages :**  
  - Performances locales  
- **Inconvénients :**  
  - Installation sur chaque poste  
  - Maintenance plus complexe

### Option B — Application web
- **Avantages :**  
  - Accessible depuis un navigateur  
  - Aucune installation requise  
  - Facilité de mise à jour
- **Inconvénients :**  
  - Dépendance à une connexion internet

---

## 4. Justification (Pourquoi cette décision ?)
- Accès simple pour tous les rôles utilisateurs  
- Maintenance et déploiement facilités  
- Adapté aux contraintes du projet académique

---

## 5. Conséquences

### Positives
- Centralisation des données  
- Accès multiplateforme  
- Évolution facilitée du système

### Négatives / Risques
- Dépendance à la connexion internet  
- Sécurité à prendre en compte dès les phases suivantes

### Impact sur l’architecture / le code
- Mise en place d’une architecture en couches  
- Séparation entre interface web, logique métier et données

---

## 6. Plan d’implémentation (court)
-  Définir l’architecture générale  
-  Identifier les rôles et permissions  
-  Implémenter un prototype web simple

---

## 7. Validation
- **Comment vérifier que c’est bon ?**
  - Le système est accessible via un navigateur  
  - Les rôles utilisateurs sont clairement définis  
  - Le prototype répond aux besoins de base

---
