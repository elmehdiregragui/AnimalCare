## Corrections effectuées suite à vos commentaires

Suite aux remarques reçues sur la première version du SRS, nous avons apporté les améliorations suivantes :

### 1. Amélioration de l’accès à la documentation

Nous avons ajouté une section "Documentation" dans le README avec des liens directs vers :

- documentation/SRS.md  
- documentation/ADR.md  

Cela permet d’ouvrir les documents principaux en un seul clic, sans navigation complexe dans les dossiers.


### 2. Transformation des fonctionnalités en exigences fonctionnelles (FR)

La simple liste des fonctionnalités a été restructurée sous forme d’exigences fonctionnelles testables.

Exemples :

FR-01 : Le système doit permettre à la réceptionniste de créer, modifier et annuler un rendez-vous.  
FR-02 : Le système doit vérifier la disponibilité du vétérinaire avant de confirmer un rendez-vous.  
FR-03 : Le système doit permettre d’associer un animal à un propriétaire.  
FR-04 : Le vétérinaire doit pouvoir consulter son planning par jour et par semaine.  
FR-05 : L’administrateur doit pouvoir gérer les comptes utilisateurs (création, activation/désactivation, gestion des rôles).

Pour chaque exigence fonctionnelle, nous avons ajouté :
- Les règles métier
- Les validations des champs obligatoires
- Les cas d’erreur possibles

Cela permet de rendre le SRS pilotable et vérifiable.



### 3. Ajout d’exigences non fonctionnelles (NFR)

Nous avons ajouté des exigences non fonctionnelles concrètes, notamment :

- Sécurité : authentification obligatoire, gestion des rôles, règles de mot de passe.
- Performance : temps de réponse cible inférieur à 3 secondes pour les pages principales.
- Disponibilité : application accessible via navigateur web moderne avec connexion internet.
- Expérience utilisateur : création d’un rendez-vous en maximum 4 étapes.
- Qualité du code : respect d’une architecture en couches (MVC), séparation des responsabilités.

Ces NFR permettent d’encadrer la qualité globale du système.


### 4. Clarification du périmètre (IN / OUT)

La section "Limites" a été précisée afin d’éviter toute ambiguïté sur le périmètre du projet.

Exclusions ajoutées :

- Gestion des paiements en ligne
- Facturation automatisée
- Notifications SMS ou email automatiques
- Gestion du stock de médicaments
- Intégration avec un calendrier externe
- Dossier médical complet avancé

Cela permet de mieux cadrer le projet dans un contexte académique.

---

## Conclusion

Les modifications apportées permettent de transformer le SRS initial en un document structuré, testable et exploitable pour le pilotage du projet, conformément aux exigences du cours.
