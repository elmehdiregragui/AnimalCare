using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AnimalCareApplication.Models;

public partial class AnimalCareDbContext : DbContext
{
    public AnimalCareDbContext()
    {
    }

    public AnimalCareDbContext(DbContextOptions<AnimalCareDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<Historique> Historiques { get; set; }

    public virtual DbSet<Horaire> Horaires { get; set; }

    public virtual DbSet<Proprietaire> Proprietaires { get; set; }

    public virtual DbSet<RendezVou> RendezVous { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<VAnimauxProprietaire> VAnimauxProprietaires { get; set; }

    public virtual DbSet<VRendezVousDetail> VRendezVousDetails { get; set; }

    public virtual DbSet<Veterinaire> Veterinaires { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.IdAnimal).HasName("PK__Animal__951092F0278CFD1A");

            entity.ToTable("Animal");

            entity.Property(e => e.Espece).HasMaxLength(50);
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Race).HasMaxLength(50);

            entity.HasOne(d => d.IdProprietaireNavigation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.IdProprietaire)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Animal_Proprietaire");
        });

        modelBuilder.Entity<Historique>(entity =>
        {
            entity.HasKey(e => e.IdHistorique).HasName("PK__Historiq__6E2D622BCE8E2AA3");

            entity.ToTable("Historique");

            entity.Property(e => e.DateSoin).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(255);

            entity.HasOne(d => d.IdAnimalNavigation).WithMany(p => p.Historiques)
                .HasForeignKey(d => d.IdAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historique_Animal");

            entity.HasOne(d => d.IdVeterinaireNavigation).WithMany(p => p.Historiques)
                .HasForeignKey(d => d.IdVeterinaire)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historique_Veterinaire");
        });

        modelBuilder.Entity<Horaire>(entity =>
        {
            entity.HasKey(e => e.IdHoraire).HasName("PK__Horaire__2A7ABF7411B7EFB3");

            entity.ToTable("Horaire");

            entity.Property(e => e.HeureDebut).HasPrecision(0);
            entity.Property(e => e.HeureFin).HasPrecision(0);
            entity.Property(e => e.Jour).HasMaxLength(10);

            entity.HasOne(d => d.IdVeterinaireNavigation).WithMany(p => p.Horaires)
                .HasForeignKey(d => d.IdVeterinaire)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Horaire_Veterinaire");
        });

        modelBuilder.Entity<Proprietaire>(entity =>
        {
            entity.HasKey(e => e.IdProprietaire).HasName("PK__Propriet__1B104AF3514AF7DD");

            entity.ToTable("Proprietaire");

            entity.Property(e => e.Adresse).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Prenom).HasMaxLength(50);
            entity.Property(e => e.Telephone).HasMaxLength(20);
        });

        modelBuilder.Entity<RendezVou>(entity =>
        {
            entity.HasKey(e => e.IdRendezVous).HasName("PK__RendezVo__F83E72CC8FDC01F1");

            entity.ToTable(tb => tb.HasTrigger("trg_RendezVous_Termine"));

            entity.Property(e => e.DateRv).HasColumnType("date");
            entity.Property(e => e.Heure).HasPrecision(0);
            entity.Property(e => e.Statut).HasMaxLength(20);

            entity.HasOne(d => d.IdAnimalNavigation).WithMany(p => p.RendezVous)
                .HasForeignKey(d => d.IdAnimal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RendezVous_Animal");

            entity.HasOne(d => d.IdVeterinaireNavigation)
    .WithMany(p => p.RendezVous)  
    .HasForeignKey(d => d.IdVeterinaire)
    .HasConstraintName("FK_RendezVous_Veterinaire");

        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Role__B43690545F585F4C");

            entity.ToTable("Role");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Nom).HasMaxLength(50);
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.IdUtilisateur).HasName("PK__Utilisat__45A4C1573697E109");

            entity.ToTable("Utilisateur");

            entity.HasIndex(e => e.Email, "UQ__Utilisat__A9D10534214EFAD2").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.MotDePasse).HasMaxLength(100);
            entity.Property(e => e.Nom).HasMaxLength(50);
            entity.Property(e => e.Prenom).HasMaxLength(50);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Utilisateur_Role");
        });

        modelBuilder.Entity<VAnimauxProprietaire>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_AnimauxProprietaires");

            entity.Property(e => e.Espece).HasMaxLength(50);
            entity.Property(e => e.NomAnimal).HasMaxLength(50);
            entity.Property(e => e.NomProprietaire).HasMaxLength(50);
            entity.Property(e => e.PrenomProprietaire).HasMaxLength(50);
            entity.Property(e => e.Race).HasMaxLength(50);
        });

        modelBuilder.Entity<VRendezVousDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_RendezVousDetails");

            entity.Property(e => e.DateRv).HasColumnType("date");
            entity.Property(e => e.Heure).HasPrecision(0);
            entity.Property(e => e.NomAnimal).HasMaxLength(50);
            entity.Property(e => e.NomProprietaire).HasMaxLength(50);
            entity.Property(e => e.NomVeterinaire).HasMaxLength(50);
            entity.Property(e => e.PrenomProprietaire).HasMaxLength(50);
            entity.Property(e => e.PrenomVeterinaire).HasMaxLength(50);
            entity.Property(e => e.Statut).HasMaxLength(20);
        });

        modelBuilder.Entity<Veterinaire>(entity =>
        {
            entity.HasKey(e => e.IdVeterinaire).HasName("PK__Veterina__30A2B44106AD276D");

            entity.ToTable("Veterinaire");

            entity.HasIndex(e => e.IdUtilisateur, "UQ__Veterina__45A4C1569E0BC5F9").IsUnique();

            entity.Property(e => e.Specialite).HasMaxLength(100);

            entity.HasOne(d => d.IdUtilisateurNavigation).WithOne(p => p.Veterinaire)
                .HasForeignKey<Veterinaire>(d => d.IdUtilisateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Veterinaire_Utilisateur");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
