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


## Conclusion

Les modifications apportées permettent de transformer le SRS initial en un document structuré, testable et exploitable pour le pilotage du projet, conformément aux exigences du cours.

### 5. Ajout et clarification des diagrammes

Afin de renforcer la compréhension technique du système AnimalCare, nous avons ajouté et détaillé les diagrammes suivants :


### 5.1 Diagramme de composants

Le diagramme de composants présente l’architecture globale du système.

Il met en évidence une architecture en couches composée de :

- Frontend (Razor Views)  
- Backend (ASP.NET Core MVC)  
- Data Access Layer (DbContext)  
- Base de données SQL Server  

Ce diagramme illustre :

- La séparation des responsabilités entre interface, logique métier et données  
- La communication via HTTP entre le client et le serveur  
- L’isolation des services métiers  
- L’utilisation d’un DbContext pour la gestion des données  

Cela permet de démontrer le respect des exigences non fonctionnelles liées à la qualité du code et à l’architecture en couches (MVC).



### 5.2 Diagramme de cas d’utilisation

Le diagramme de cas d’utilisation illustre les interactions entre les différents acteurs et le système.

Les acteurs identifiés sont :

- Vétérinaire  
- Réceptionniste  
- Administrateur  
- Propriétaire  

Chaque acteur dispose de permissions spécifiques selon son rôle.

Les principales fonctionnalités représentées sont :

- Gestion des rendez-vous  
- Gestion des disponibilités  
- Gestion des utilisateurs  
- Consultation des dossiers animaux  
- Mise à jour des profils  

Tous les cas d’utilisation incluent l’authentification, garantissant que seules les personnes autorisées peuvent accéder aux fonctionnalités du système.

Ce diagramme permet de valider la cohérence entre les rôles définis et les exigences fonctionnelles (FR).



### 5.3 Diagramme de classes

Le diagramme de classes décrit la structure interne du système et les relations entre les entités principales.

Les classes principales sont :

- Utilisateur  
- Administrateur  
- Réceptionniste  
- Vétérinaire  
- Propriétaire  
- Animal  
- RendezVous  
- Horaire  
- Historique  

Les relations importantes incluent :

- Un propriétaire peut posséder plusieurs animaux (1..N)  
- Un vétérinaire peut avoir plusieurs rendez-vous  
- Un animal peut avoir plusieurs entrées d’historique  
- Un rendez-vous est associé à un vétérinaire et à un animal  

Ce diagramme assure la cohérence entre le modèle objet et la structure de la base de données.



### 5.4 Diagramme de séquence – Gestion des rendez-vous

Le diagramme de séquence représente le déroulement dynamique du processus de prise, modification et annulation d’un rendez-vous.

Le scénario principal comprend :

- Authentification de l’utilisateur  
- Demande de rendez-vous  
- Vérification des disponibilités du vétérinaire  
- Proposition de créneaux disponibles  
- Confirmation et création du rendez-vous  

Des scénarios alternatifs sont également représentés :

- Modification par la réceptionniste  
- Annulation par le propriétaire  
- Indisponibilité et proposition d’alternatives  

Ce diagramme permet de valider le respect des exigences fonctionnelles liées à la gestion des rendez-vous (FR-01 et FR-02).



## Conclusion

Les modifications apportées permettent de transformer le SRS initial en un document structuré, testable et exploitable pour le pilotage du projet, conformément aux exigences du cours.

L’ajout des diagrammes renforce la cohérence entre :

- Les exigences fonctionnelles (FR)  
- Les exigences non fonctionnelles (NFR)  
- L’architecture logicielle  
- Le modèle de données  
- Les scénarios métier  

Le document est désormais complet, structuré et conforme aux attentes académiques.
