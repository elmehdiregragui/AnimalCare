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

8. Explication des diagrammes
8.1 Diagramme de composants

Le diagramme de composants présente l’architecture générale du système AnimalCare.
Il permet de visualiser la séparation des différentes couches du système ainsi que leurs interactions.

L’architecture adoptée est une architecture en couches basée sur ASP.NET Core MVC, composée de :

Frontend

Le frontend correspond au client web utilisant les Razor Views.
Il permet aux utilisateurs (vétérinaire, réceptionniste, administrateur, propriétaire) d’interagir avec l’application via un navigateur web.
Les requêtes sont transmises au backend via HTTP.

Backend

Le backend repose sur ASP.NET Core MVC et contient :

La gestion de l’authentification et des rôles

Les services métiers (gestion des rendez-vous, animaux, horaires, utilisateurs)

La logique applicative

Cette couche applique les règles métier définies dans les exigences fonctionnelles (FR).

Data Access Layer

La couche d’accès aux données (DbContext) assure la communication avec la base de données SQL Server.
Elle permet :

La lecture des données

L’insertion

La modification

La suppression

Cette séparation garantit une meilleure maintenabilité, évolutivité et conformité avec les exigences non fonctionnelles liées à la qualité du code.

8.2 Diagramme de cas d’utilisation

Le diagramme de cas d’utilisation illustre les interactions entre les différents acteurs et le système AnimalCare.

Les acteurs identifiés sont :

Vétérinaire

Réceptionniste

Administrateur

Propriétaire

Chaque acteur possède des droits spécifiques selon son rôle.

Tous les cas d’utilisation incluent l’authentification, ce qui garantit que seules les personnes autorisées peuvent accéder aux fonctionnalités.

Exemples :

Le vétérinaire peut :

Consulter ses rendez-vous

Gérer ses disponibilités

Mettre à jour le dossier médical

La réceptionniste peut :

Planifier les rendez-vous

Gérer les informations des propriétaires

Consulter les dossiers des animaux

L’administrateur peut :

Gérer les comptes utilisateurs

Consulter les rapports

Le propriétaire peut :

Gérer ses rendez-vous

Consulter le dossier de son animal

Mettre à jour son profil

Ce diagramme démontre la séparation des responsabilités et le respect de la gestion des rôles définie dans les exigences fonctionnelles.

8.3 Diagramme de classes

Le diagramme de classes représente la structure interne du système et les relations entre les entités principales.

La classe principale est Utilisateur, qui contient les attributs communs :

IdUtilisateur

Nom

Prenom

Email

MotDePasse

Les rôles spécifiques (Administrateur, Réceptionniste, Vétérinaire) sont associés à cette classe.

Les principales classes métier sont :

Propriétaire

Animal

RendezVous

Horaire

Historique

Relations importantes :

Un propriétaire peut posséder plusieurs animaux (relation 1..N).

Un vétérinaire peut avoir plusieurs rendez-vous.

Un animal peut avoir plusieurs entrées dans son historique médical.

Un rendez-vous est associé à un vétérinaire et à un animal.

Ce diagramme confirme la cohérence entre le modèle objet et la structure de la base de données.

8.4 Diagramme de séquence – Gestion des rendez-vous

Le diagramme de séquence illustre le déroulement dynamique du processus de prise de rendez-vous.

Étapes principales :

Le propriétaire s’authentifie.

Il soumet une demande de rendez-vous.

Le système vérifie la disponibilité du vétérinaire selon la date et l’heure demandées.

Si des créneaux sont disponibles :

Le système propose les créneaux

Le propriétaire sélectionne un créneau

Le rendez-vous est créé

Une confirmation est envoyée

Si aucun créneau n’est disponible :

Le système propose des alternatives

Le diagramme montre également les scénarios de modification par la réceptionniste et d’annulation par le propriétaire.

Ce diagramme permet de valider le respect des exigences fonctionnelles liées à la gestion des rendez-vous (FR-01 et FR-02).

Conclusion des diagrammes

Les diagrammes présentés permettent de :

Visualiser l’architecture technique du système

Définir clairement les interactions entre les acteurs et l’application

Structurer les entités et leurs relations

Illustrer le fonctionnement dynamique des processus clés

Ils assurent la cohérence entre les exigences fonctionnelles, l’architecture logicielle et le modèle de données.
