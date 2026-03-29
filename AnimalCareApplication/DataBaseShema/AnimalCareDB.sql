

IF DB_ID('AnimalCareDB') IS NOT NULL
    DROP DATABASE AnimalCareDB;
GO

CREATE DATABASE AnimalCareDB;
GO
                   

USE AnimalCareDB;
GO



-- TABLE ROLE
CREATE TABLE Role (
    IdRole INT IDENTITY(1,1) PRIMARY KEY,
    Nom NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255)
);
GO

-- TABLE UTILISATEUR
CREATE TABLE Utilisateur (
    IdUtilisateur INT IDENTITY(1,1) PRIMARY KEY,
    Nom NVARCHAR(50) NOT NULL,
    Prenom NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    MotDePasse NVARCHAR(100) NOT NULL,
    IdRole INT NOT NULL,
    CONSTRAINT FK_Utilisateur_Role FOREIGN KEY (IdRole)
        REFERENCES Role(IdRole)
);
GO

-- TABLE PROPRIETAIRE
CREATE TABLE Proprietaire (
    IdProprietaire INT IDENTITY(1,1) PRIMARY KEY,
    Nom NVARCHAR(50) NOT NULL,
    Prenom NVARCHAR(50) NOT NULL,
    Adresse NVARCHAR(255) NOT NULL,
    Telephone NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);
GO

-- TABLE VETERINAIRE
CREATE TABLE Veterinaire (
    IdVeterinaire INT IDENTITY(1,1) PRIMARY KEY,
    Specialite NVARCHAR(100) NOT NULL,
    IdUtilisateur INT NOT NULL UNIQUE,
    CONSTRAINT FK_Veterinaire_Utilisateur FOREIGN KEY (IdUtilisateur)
        REFERENCES Utilisateur(IdUtilisateur)
);
GO

-- TABLE ANIMAL
CREATE TABLE Animal (
    IdAnimal INT IDENTITY(1,1) PRIMARY KEY,
    Nom NVARCHAR(50) NOT NULL,
    Espece NVARCHAR(50) NOT NULL,
    Race NVARCHAR(50),
    Age INT CHECK (Age >= 0),
    IdProprietaire INT NOT NULL,
    CONSTRAINT FK_Animal_Proprietaire FOREIGN KEY (IdProprietaire)
        REFERENCES Proprietaire(IdProprietaire)
);
GO

-- TABLE RENDEZVOUS
CREATE TABLE RendezVous (
    IdRendezVous INT IDENTITY(1,1) PRIMARY KEY,
    DateRv DATE NOT NULL,
    Heure TIME(0) NOT NULL,
    Statut NVARCHAR(20) NOT NULL
        CONSTRAINT CK_RendezVous_Statut CHECK (Statut IN ('Pr�vu', 'Annul�', 'Termin�')),
    IdAnimal INT NOT NULL,
    IdVeterinaire INT NOT NULL,
    CONSTRAINT FK_RendezVous_Animal FOREIGN KEY (IdAnimal) REFERENCES Animal(IdAnimal),
    CONSTRAINT FK_RendezVous_Veterinaire FOREIGN KEY (IdVeterinaire) REFERENCES Veterinaire(IdVeterinaire)
);
GO

-- TABLE HORAIRE
CREATE TABLE Horaire (
    IdHoraire INT IDENTITY(1,1) PRIMARY KEY,
    Jour NVARCHAR(10) NOT NULL
        CHECK (Jour IN ('Lundi','Mardi','Mercredi','Jeudi','Vendredi','Samedi')),
    HeureDebut TIME(0) NOT NULL,
    HeureFin TIME(0) NOT NULL,
    IdVeterinaire INT NOT NULL,
    CONSTRAINT FK_Horaire_Veterinaire FOREIGN KEY (IdVeterinaire)
        REFERENCES Veterinaire(IdVeterinaire),
    CONSTRAINT CK_Horaire_Period CHECK (HeureFin > HeureDebut)
);
GO

-- TABLE HISTORIQUE
CREATE TABLE Historique (
    IdHistorique INT IDENTITY(1,1) PRIMARY KEY,
    DateSoin DATE NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    IdAnimal INT NOT NULL,
    IdVeterinaire INT NOT NULL,
    CONSTRAINT FK_Historique_Animal FOREIGN KEY (IdAnimal) REFERENCES Animal(IdAnimal),
    CONSTRAINT FK_Historique_Veterinaire FOREIGN KEY (IdVeterinaire) REFERENCES Veterinaire(IdVeterinaire)
);
GO



-- ROLE
INSERT INTO Role (Nom, Description) VALUES
('Administrateur', 'Gestion compl�te du syst�me'),
('V�t�rinaire', 'Acc�s aux dossiers m�dicaux'),
('Secr�taire', 'Gestion des clients et rendez-vous');


-- UTILISATEUR
INSERT INTO Utilisateur (Nom, Prenom, Email, MotDePasse, IdRole) VALUES
('Alami', 'Mohammed', 'mohammed.alami@gmail.com', 'pass123', 1),
('Benali', 'Yassine', 'yassine.benali@gmail.com', 'pass123', 2),
('Mansouri', 'Fatima', 'fatima.mansouri@gmail.com', 'pass123', 2),
('Haddad', 'Samira', 'samira.haddad@gmail.com', 'pass123', 3),
('Khaled', 'Omar', 'omar.khaled@gmail.com', 'pass123', 3);


-- VETERINAIRE
INSERT INTO Veterinaire (Specialite, IdUtilisateur) VALUES
('Chirurgie animale', 2),
('Soins g�n�raux', 3);


-- PROPRIETAIRE
INSERT INTO Proprietaire (Nom, Prenom, Adresse, Telephone, Email) VALUES
('El Idrissi', 'Amina', '123 Rue Casablanca', '514-234-5678', 'amina.elidrissi@gmail.com'),
('Bouhrou', 'Karim', '45 Avenue Rabat', '438-776-8899', 'karim.bouhrou@gmail.com'),
('Saadi', 'Lina', '90 Rue Marrakech', '514-999-8877', 'lina.saadi@gmail.com');


-- ANIMAL
INSERT INTO Animal (Nom, Espece, Race, Age, IdProprietaire) VALUES
('Simba', 'Chat', 'Persan', 3, 1),
('Rex', 'Chien', 'Berger Allemand', 5, 2),
('Kiko', 'Chat', 'Siamois', 2, 3),
('Bella', 'Chien', 'Husky', 4, 1);


-- RENDEZVOUS
INSERT INTO RendezVous (DateRv, Heure, Statut, IdAnimal, IdVeterinaire) VALUES
('2025-02-10', '14:00', 'Pr�vu', 1, 1),
('2025-02-11', '09:30', 'Pr�vu', 2, 2),
('2025-02-12', '11:15', 'Annul�', 3, 1),
('2025-02-13', '10:00', 'Termin�', 4, 2);


-- HORAIRE
INSERT INTO Horaire (Jour, HeureDebut, HeureFin, IdVeterinaire) VALUES
('Lundi', '09:00', '12:00', 1),
('Lundi', '13:00', '17:00', 1),
('Mardi', '09:00', '12:00', 2),
('Mercredi', '10:00', '15:00', 2);


-- HISTORIQUE
INSERT INTO Historique (DateSoin, Description, IdAnimal, IdVeterinaire) VALUES
('2025-01-05', 'Vaccination annuelle', 1, 1),
('2025-01-10', 'Traitement anti-parasitaire', 2, 2),
('2025-01-15', 'Consultation de contr�le', 3, 1),
('2025-01-20', 'Soins dentaires', 4, 2);


GO
CREATE VIEW v_AnimauxProprietaires AS
SELECT a.IdAnimal, a.Nom AS NomAnimal, a.Espece, a.Race,
       p.IdProprietaire, p.Nom AS NomProprietaire, p.Prenom AS PrenomProprietaire
FROM Animal a
JOIN Proprietaire p ON a.IdProprietaire = p.IdProprietaire;
GO

CREATE VIEW v_RendezVousDetails AS
SELECT rv.IdRendezVous, rv.DateRv, rv.Heure, rv.Statut,
       a.Nom AS NomAnimal, p.Nom AS NomProprietaire, p.Prenom AS PrenomProprietaire,
       u.Nom AS NomVeterinaire, u.Prenom AS PrenomVeterinaire
FROM RendezVous rv
JOIN Animal a ON rv.IdAnimal = a.IdAnimal
JOIN Proprietaire p ON a.IdProprietaire = p.IdProprietaire
JOIN Veterinaire v ON rv.IdVeterinaire = v.IdVeterinaire
JOIN Utilisateur u ON v.IdUtilisateur = u.IdUtilisateur;
GO



CREATE PROCEDURE sp_AjouterProprietaire
    @Nom NVARCHAR(50),
    @Prenom NVARCHAR(50),
    @Adresse NVARCHAR(255),
    @Telephone NVARCHAR(20),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Proprietaire (Nom, Prenom, Adresse, Telephone, Email)
    VALUES (@Nom, @Prenom, @Adresse, @Telephone, @Email);
END;
GO

CREATE PROCEDURE sp_AjouterAnimal
    @Nom NVARCHAR(50),
    @Espece NVARCHAR(50),
    @Race NVARCHAR(50) = NULL,
    @Age INT = NULL,
    @IdProprietaire INT
AS
BEGIN
    INSERT INTO Animal (Nom, Espece, Race, Age, IdProprietaire)
    VALUES (@Nom, @Espece, @Race, @Age, @IdProprietaire);
END;
GO




CREATE TRIGGER trg_RendezVous_Termine
ON RendezVous
AFTER UPDATE
AS
BEGIN
    INSERT INTO Historique (DateSoin, Description, IdAnimal, IdVeterinaire)
    SELECT i.DateRv,
           'Soin effectu� lors du rendez-vous ' + CAST(i.IdRendezVous AS NVARCHAR(10)),
           i.IdAnimal,
           i.IdVeterinaire
    FROM inserted i
    JOIN deleted d ON i.IdRendezVous = d.IdRendezVous
    WHERE d.Statut <> 'Termin�' AND i.Statut = 'Termin�';
END;
GO
