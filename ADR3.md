
# Architecture Decision Records  
ADR-002 — Choix de l’architecture et des Design Patterns

**Statut :** Accepted  
**Date :** 2026-03-29  
**Décideurs :** Équipe du projet  
**Contexte projet :** Organisation de l’architecture du système AnimalCare



## 1. Contexte
- **Problème / besoin :**  
L’application AnimalCare doit gérer plusieurs modules
(utilisateurs, animaux, rendez-vous, horaires, notifications).
Une structure claire est nécessaire pour séparer les responsabilités
et faciliter l’évolution du système.

- **Contraintes :**  
Projet académique, temps limité, besoin d’un code lisible et maintenable.

- **Forces en présence :**  
Maintenabilité et démonstration de design patterns.


## 2. Décision
> L’application adopte une architecture MVC combinée à l’utilisation de Design Patterns.

- Je choisis : Architecture MVC et les designs Patterns (Singleton, Observer, Strategy, Decorator)  
- Pour : améliorer l’organisation, la flexibilité et la maintenabilité du code



## 3. Alternatives considérées

### Option A — Architecture simple sans patterns
- **Avantages :**  
  - Développement plus rapide  
  - Moins de classes  
- **Inconvénients :**  
  - Code difficile à maintenir

### Option B — Architecture MVC avec Design Patterns
- **Avantages :**  
  - Séparation claire des responsabilités  
  - Code simple
  - Réutilisation du code
- **Inconvénients :**  
  - Complexité plus élevée  
  - Plus de fichiers à gérer


## 4. Justification
- MVC permet de séparer la logique métier, l’interface et les données  
- Les design patterns améliorent la flexibilité du système  
- Le projet devient plus structuré et évolutif  
- Cette décision respecte les bonnes pratiques de développement


## 5. Conséquences

### Positives
- Architecture claire et organisée  
- Faible couplage entre les composants    
- Code plus maintenable et plus structuré

### Négatives / Risques
- Complexité supplémentaire  
- Temps de développement plus long

### Impact sur l’architecture / le code
- Séparation en Models, Views et Controllers  
- Ajout d’un dossier Patterns  
- Implémentation des interfaces et classes abstraites


## 6. Plan d’implémentation
-  Définir les modèles  
-  Créer les contrôleurs  
-  Implémenter les vues  
-  Ajouter les design patterns  
-  Tester l’intégration globale


## 7. Validation
  - L’application respecte l’architecture MVC  
  - Les design patterns sont utilisés dans le code  
  - Les responsabilités sont bien séparées  
  - Le système est fonctionnable

