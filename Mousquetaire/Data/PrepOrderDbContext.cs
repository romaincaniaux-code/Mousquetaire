using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mousquetaire.Models;

namespace Mousquetaire.Data;

public partial class PrepOrderDbContext : DbContext
{
    public PrepOrderDbContext(DbContextOptions<PrepOrderDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<DemandePalette> DemandePalettes { get; set; }

    public virtual DbSet<Emplacement> Emplacements { get; set; }

    public virtual DbSet<LigneCommande> LigneCommandes { get; set; }

    public virtual DbSet<Magasin> Magasins { get; set; }

    public virtual DbSet<Palette> Palettes { get; set; }

    public virtual DbSet<Preparation> Preparations { get; set; }

    public virtual DbSet<Produit> Produits { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<Zone> Zones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.IdCommande);

            entity.ToTable("Commande");

            entity.HasIndex(e => e.IdMagasin, "IX_Commande_idMagasin");

            entity.Property(e => e.IdCommande).HasColumnName("idCommande");
            entity.Property(e => e.DateImport)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_Commande_dateImport")
                .HasColumnName("dateImport");
            entity.Property(e => e.IdMagasin).HasColumnName("idMagasin");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasDefaultValue("Importee", "DF_Commande_statut")
                .HasColumnName("statut");

            entity.HasOne(d => d.IdMagasinNavigation).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.IdMagasin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Commande_Magasin");
        });

        modelBuilder.Entity<DemandePalette>(entity =>
        {
            entity.HasKey(e => e.IdDemande);

            entity.ToTable("DemandePalette");

            entity.HasIndex(e => e.Statut, "IX_DemandePalette_statut");

            entity.Property(e => e.IdDemande).HasColumnName("idDemande");
            entity.Property(e => e.DateConfirmation)
                .HasPrecision(0)
                .HasColumnName("dateConfirmation");
            entity.Property(e => e.DateDemande)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_DemandePalette_dateDemande")
                .HasColumnName("dateDemande");
            entity.Property(e => e.IdCariste).HasColumnName("idCariste");
            entity.Property(e => e.IdEmplacement).HasColumnName("idEmplacement");
            entity.Property(e => e.IdPalette).HasColumnName("idPalette");
            entity.Property(e => e.IdPreparateur).HasColumnName("idPreparateur");
            entity.Property(e => e.IdProduit).HasColumnName("idProduit");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasDefaultValue("Demandee", "DF_DemandePalette_statut")
                .HasColumnName("statut");

            entity.HasOne(d => d.IdCaristeNavigation).WithMany(p => p.DemandePaletteIdCaristeNavigations)
                .HasForeignKey(d => d.IdCariste)
                .HasConstraintName("FK_DemandePalette_Cariste");

            entity.HasOne(d => d.IdEmplacementNavigation).WithMany(p => p.DemandePalettes)
                .HasForeignKey(d => d.IdEmplacement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DemandePalette_Emplacement");

            entity.HasOne(d => d.IdPaletteNavigation).WithMany(p => p.DemandePalettes)
                .HasForeignKey(d => d.IdPalette)
                .HasConstraintName("FK_DemandePalette_Palette");

            entity.HasOne(d => d.IdPreparateurNavigation).WithMany(p => p.DemandePaletteIdPreparateurNavigations)
                .HasForeignKey(d => d.IdPreparateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DemandePalette_Preparateur");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.DemandePalettes)
                .HasForeignKey(d => d.IdProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DemandePalette_Produit");
        });

        modelBuilder.Entity<Emplacement>(entity =>
        {
            entity.HasKey(e => e.IdEmplacement);

            entity.ToTable("Emplacement");

            entity.HasIndex(e => e.IdZone, "IX_Emplacement_idZone");

            entity.HasIndex(e => e.Code, "UQ_Emplacement_code").IsUnique();

            entity.HasIndex(e => new { e.Allee, e.Etage, e.Numero }, "UQ_Emplacement_position").IsUnique();

            entity.Property(e => e.IdEmplacement).HasColumnName("idEmplacement");
            entity.Property(e => e.Allee).HasColumnName("allee");
            entity.Property(e => e.Code)
                .HasMaxLength(12)
                .HasColumnName("code");
            entity.Property(e => e.Etage).HasColumnName("etage");
            entity.Property(e => e.IdZone).HasColumnName("idZone");
            entity.Property(e => e.Numero).HasColumnName("numero");

            entity.HasOne(d => d.IdZoneNavigation).WithMany(p => p.Emplacements)
                .HasForeignKey(d => d.IdZone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Emplacement_Zone");
        });

        modelBuilder.Entity<LigneCommande>(entity =>
        {
            entity.HasKey(e => new { e.IdCommande, e.IdProduit });

            entity.ToTable("LigneCommande");

            entity.Property(e => e.IdCommande).HasColumnName("idCommande");
            entity.Property(e => e.IdProduit).HasColumnName("idProduit");
            entity.Property(e => e.Quantite).HasColumnName("quantite");
            entity.Property(e => e.QuantitePreparee).HasColumnName("quantitePreparee");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasDefaultValue("APreparer", "DF_LigneCommande_statut")
                .HasColumnName("statut");

            entity.HasOne(d => d.IdCommandeNavigation).WithMany(p => p.LigneCommandes)
                .HasForeignKey(d => d.IdCommande)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LigneCommande_Commande");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.LigneCommandes)
                .HasForeignKey(d => d.IdProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LigneCommande_Produit");
        });

        modelBuilder.Entity<Magasin>(entity =>
        {
            entity.HasKey(e => e.IdMagasin);

            entity.ToTable("Magasin");

            entity.Property(e => e.IdMagasin).HasColumnName("idMagasin");
            entity.Property(e => e.Adresse)
                .HasMaxLength(200)
                .HasColumnName("adresse");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");
        });

        modelBuilder.Entity<Palette>(entity =>
        {
            entity.HasKey(e => e.IdPalette);

            entity.ToTable("Palette");

            entity.HasIndex(e => e.IdProduit, "IX_Palette_idProduit");

            entity.HasIndex(e => e.IdEmplacement, "UQ_Palette_idEmplacement").IsUnique();

            entity.Property(e => e.IdPalette).HasColumnName("idPalette");
            entity.Property(e => e.IdEmplacement).HasColumnName("idEmplacement");
            entity.Property(e => e.IdProduit).HasColumnName("idProduit");
            entity.Property(e => e.Quantite).HasColumnName("quantite");

            entity.HasOne(d => d.IdEmplacementNavigation).WithOne(p => p.Palette)
                .HasForeignKey<Palette>(d => d.IdEmplacement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Palette_Emplacement");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.Palettes)
                .HasForeignKey(d => d.IdProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Palette_Produit");
        });

        modelBuilder.Entity<Preparation>(entity =>
        {
            entity.HasKey(e => e.IdPreparation);

            entity.ToTable("Preparation");

            entity.HasIndex(e => e.IdUtilisateur, "IX_Preparation_idUtilisateur");

            entity.HasIndex(e => new { e.IdCommande, e.IdZone }, "UQ_Preparation_commande_zone").IsUnique();

            entity.Property(e => e.IdPreparation).HasColumnName("idPreparation");
            entity.Property(e => e.DateDebut)
                .HasPrecision(0)
                .HasColumnName("dateDebut");
            entity.Property(e => e.DateFin)
                .HasPrecision(0)
                .HasColumnName("dateFin");
            entity.Property(e => e.IdCommande).HasColumnName("idCommande");
            entity.Property(e => e.IdUtilisateur).HasColumnName("idUtilisateur");
            entity.Property(e => e.IdZone).HasColumnName("idZone");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasDefaultValue("AAssigner", "DF_Preparation_statut")
                .HasColumnName("statut");

            entity.HasOne(d => d.IdCommandeNavigation).WithMany(p => p.Preparations)
                .HasForeignKey(d => d.IdCommande)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Preparation_Commande");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.Preparations)
                .HasForeignKey(d => d.IdUtilisateur)
                .HasConstraintName("FK_Preparation_Utilisateur");

            entity.HasOne(d => d.IdZoneNavigation).WithMany(p => p.Preparations)
                .HasForeignKey(d => d.IdZone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Preparation_Zone");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.IdProduit);

            entity.ToTable("Produit");

            entity.HasIndex(e => e.IdZone, "IX_Produit_idZone");

            entity.HasIndex(e => e.Libelle, "UQ_Produit_libelle").IsUnique();

            entity.Property(e => e.IdProduit).HasColumnName("idProduit");
            entity.Property(e => e.IdZone).HasColumnName("idZone");
            entity.Property(e => e.Libelle)
                .HasMaxLength(100)
                .HasColumnName("libelle");

            entity.HasOne(d => d.IdZoneNavigation).WithMany(p => p.Produits)
                .HasForeignKey(d => d.IdZone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produit_Zone");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole);

            entity.ToTable("Role");

            entity.HasIndex(e => e.Libelle, "UQ_Role_libelle").IsUnique();

            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.Libelle)
                .HasMaxLength(30)
                .HasColumnName("libelle");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.IdUtilisateur);

            entity.ToTable("Utilisateur");

            entity.HasIndex(e => e.IdRole, "IX_Utilisateur_idRole");

            entity.HasIndex(e => e.Login, "UQ_Utilisateur_login").IsUnique();

            entity.Property(e => e.IdUtilisateur).HasColumnName("idUtilisateur");
            entity.Property(e => e.Actif)
                .HasDefaultValue(true, "DF_Utilisateur_actif")
                .HasColumnName("actif");
            entity.Property(e => e.DateCreation)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_Utilisateur_dateCreation")
                .HasColumnName("dateCreation");
            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.IdZone).HasColumnName("idZone");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.MotDePasseHash)
                .HasMaxLength(64)
                .HasColumnName("motDePasseHash");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .HasColumnName("prenom");
            entity.Property(e => e.Sel)
                .HasDefaultValueSql("(newid())", "DF_Utilisateur_sel")
                .HasColumnName("sel");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Utilisateur_Role");

            entity.HasOne(d => d.IdZoneNavigation).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.IdZone)
                .HasConstraintName("FK_Utilisateur_Zone");
        });

        modelBuilder.Entity<Zone>(entity =>
        {
            entity.HasKey(e => e.IdZone);

            entity.ToTable("Zone");

            entity.HasIndex(e => e.Libelle, "UQ_Zone_libelle").IsUnique();

            entity.Property(e => e.IdZone).HasColumnName("idZone");
            entity.Property(e => e.Libelle)
                .HasMaxLength(30)
                .HasColumnName("libelle");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
