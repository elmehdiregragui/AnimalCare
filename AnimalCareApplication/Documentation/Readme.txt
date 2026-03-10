AnimalCare — Application de Gestion d’une Clinique Vétérinaire

Projet réalisé en ASP.NET Core MVC + SQL Server

 Description du projet

AnimalCare est une application web complète permettant la gestion d’une clinique vétérinaire.
Elle inclut :

Gestion des propriétaires

Gestion des animaux

Gestion des vétérinaires

Gestion des horaires

Gestion des rendez-vous

Gestion des historiques médicaux

Authentification basée sur les rôles (Admin, Secrétaire, Vétérinaire, Client)

Page d’accueil professionnelle haut de gamme

Interface moderne et responsive

L’objectif était de créer une application fonctionnelle, esthétique et fidèle à une vraie clinique.

 1. Création du projet
 Création d’un projet ASP.NET Core MVC

Nouveau projet → ASP.NET Core Web App (Model-View-Controller)

Framework : .NET 8 / .NET 7

Activation de MVC, Razor Views, et Static Files

 2. Création de la base de données SQL Server
Création des tables principales :

Proprietaires

Animals

Veterinaires

Utilisateurs (employés)

Roles

RendezVous

Horaires

Historiques

Relations mises en place + clés étrangères configurées.

 3. Authentification + Sessions
Implémentation du système de connexion :

Authentification manuelle, sans Identity

Stockage des sessions :

UserId

UserName

UserRole

Plusieurs rôles :

Administrateur

Secrétaire

Vétérinaire

Client

Un attribut :

[AuthorizeRoleAttribute("Administrateur", "Secrétaire", "Vétérinaire")]


 Protège toutes les pages internes.

 4. Fonctionnalités selon le rôle
Administrateur :

Gère les propriétaires

Gère les animaux

Gère les vétérinaires

Gère les rendez-vous

Gère les horaires

Gère l'historique médical

Secrétaire :

Gère propriétaires, animaux, rendez-vous

Vétérinaire :

Voit uniquement ses rendez-vous

Gérère ses horaires

Ajoute / édite les historiques médicaux

Propriétaire (Client) :

Peut créer un compte

Après connexion : peut prendre un rendez-vous

Voit seulement ses données

 5. Flux complet propriétaire
 Cas 1 : propriétaire ajouté par l’admin

Admin crée le propriétaire

Le propriétaire utilise l'email + mot de passe donné

Peut prendre rendez-vous directement

L’admin ajoute l’animal du propriétaire

 Cas 2 : propriétaire s'inscrit lui-même

Formulaire Register

Création du compte

Contact avec la clinique

Admin ajoute l’animal ensuite

Puis propriétaire choisit rendez-vous

 6. Gestion des rendez-vous

Admin et secrétaire peuvent réserver pour n’importe quel animal/vétérinaire

Un vétérinaire voit uniquement ses rendez-vous

Propriétaire → bouton "Prendre un rendez-vous"

Les rendez-vous remplis par le client apparaissent chez le vétérinaire

 7. Création de la page d'accueil haut de gamme
Étapes réalisées :

 Suppression de l’ancienne page basique
 Intégration d’un hero banner avec image en plein écran
 Boutons stylés (Connexion / Créer un compte / Rendez-vous)
 Section "À propos"
 Section "Nos vétérinaires"
 Section "Pourquoi nous choisir"
 Section "Horaires" et "Adresse"
 Palette de couleurs professionnelles
 Design moderne inspiré d’un vrai site de clinique

 8. Mise en page & Styles
Ajout d’un nouveau fichier :
wwwroot/css/site.css


Incluant :

Style premium pour la page d'accueil

Boutons arrondis

Cartes élégantes

Effets hover

Amélioration du layout général

Style personnalisé pour navbar, boutons, sections

 9. Ajout des images

Dans :

wwwroot/images/


Images ajoutées :

yassine.jpg

fatima.jpg

omar.png

Chemins corrigés :

<img src="/images/yassine.jpg" />

 10. Nettoyage et organisation de la Navbar

Remplacement des anciens liens par des boutons stylés

Séparation claire des pages selon chaque rôle

Boutons visibles uniquement selon connexion

 11. Tests complets

Tests réalisés pour chaque rôle :

Connexion / Déconnexion

Ajout / modification / suppression

Prise de rendez-vous côté client

Affichage des rendez-vous côté vétérinaire

Sécurité des pages (pas accès aux pages des autres rôles)

Vérification du design sur toutes les pages