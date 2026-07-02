using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class PrulariacomContext : DbContext
{
    public PrulariacomContext()
    {
    }

    public PrulariacomContext(DbContextOptions<PrulariacomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actiecode> Actiecodes { get; set; }

    public virtual DbSet<Artikel> Artikels { get; set; }

    public virtual DbSet<Categorie> Categorieen { get; set; }

    public virtual DbSet<Leverancier> Leveranciers { get; set; }

    public virtual DbSet<Plaats> Plaatsen { get; set; }

    public virtual DbSet<Gebruikersaccount> Gebruikersaccounts { get; set; }

    public virtual DbSet<PersoneelsLid> Personeelsleden { get; set; }

    public virtual DbSet<Personeelslidaccount> Personeelslidaccounts { get; set; }

    public virtual DbSet<SecurityGroep> Securitygroepen { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actiecode>(entity =>
        {
            entity.HasKey(e => e.ActiecodeId).HasName("PRIMARY");

            entity.ToTable("actiecodes");

            entity.Property(e => e.ActiecodeId).HasColumnName("actiecodeId");
            entity.Property(e => e.GeldigTotDatum)
                .HasColumnType("date")
                .HasColumnName("geldigTotDatum");
            entity.Property(e => e.GeldigVanDatum)
                .HasColumnType("date")
                .HasColumnName("geldigVanDatum");
            entity.Property(e => e.IsEenmalig).HasColumnName("isEenmalig");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");
        });

        modelBuilder.Entity<Artikel>(entity =>
        {
            entity.HasKey(e => e.ArtikelId).HasName("PRIMARY");

            entity.ToTable("artikelen");

            entity.HasIndex(e => e.Ean, "ean_UNIQUE").IsUnique();

            entity.HasIndex(e => e.LeveranciersId, "fk_Artikelen_Leveranciers");

            entity.Property(e => e.ArtikelId).HasColumnName("artikelId");
            entity.Property(e => e.AantalBesteldLeverancier).HasColumnName("aantalBesteldLeverancier");
            entity.Property(e => e.Beschrijving)
                .HasMaxLength(255)
                .HasColumnName("beschrijving");
            entity.Property(e => e.Bestelpeil).HasColumnName("bestelpeil");
            entity.Property(e => e.Ean)
                .HasMaxLength(13)
                .HasColumnName("ean");
            entity.Property(e => e.GewichtInGram).HasColumnName("gewichtInGram");
            entity.Property(e => e.LeveranciersId).HasColumnName("leveranciersId");
            entity.Property(e => e.Levertijd)
                .HasDefaultValueSql("'1'")
                .HasColumnName("levertijd");
            entity.Property(e => e.MaxAantalInMagazijnPlaats).HasColumnName("maxAantalInMagazijnPlaats");
            entity.Property(e => e.MaximumVoorraad).HasColumnName("maximumVoorraad");
            entity.Property(e => e.MinimumVoorraad).HasColumnName("minimumVoorraad");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");
            entity.Property(e => e.Prijs).HasColumnName("prijs");
            entity.Property(e => e.Voorraad).HasColumnName("voorraad");

            entity.HasOne(d => d.Leverancier).WithMany(p => p.Artikels)
                .HasForeignKey(d => d.LeveranciersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Artikelen_Leveranciers");
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.CategorieId).HasName("PRIMARY");

            entity.ToTable("categorieen");

            entity.HasIndex(e => e.HoofdCategorieId, "fk_Categorieen_Categorieen1_idx");

            entity.Property(e => e.CategorieId).HasColumnName("categorieId");
            entity.Property(e => e.HoofdCategorieId).HasColumnName("hoofdCategorieId");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");

            entity.HasOne(d => d.HoofdCategorie).WithMany(p => p.SubCategorieen)
                .HasForeignKey(d => d.HoofdCategorieId)
                .HasConstraintName("fk_Categorieen_Categorieen1");

            entity.HasMany(d => d.Artikels).WithMany(p => p.Categorieen)
                .UsingEntity<Dictionary<string, object>>(
                    "Artikelcategorieen",
                    r => r.HasOne<Artikel>().WithMany()
                        .HasForeignKey("ArtikelId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ArtikelCategorieen_Artikelen1"),
                    l => l.HasOne<Categorie>().WithMany()
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ArtikelCategorieen_Categorieen1"),
                    j =>
                    {
                        j.HasKey("CategorieId", "ArtikelId").HasName("PRIMARY");
                        j.ToTable("artikelcategorieen");
                        j.HasIndex(new[] { "ArtikelId" }, "fk_ArtikelCategorieen_Artikelen1_idx");
                        j.IndexerProperty<int>("CategorieId").HasColumnName("categorieId");
                        j.IndexerProperty<int>("ArtikelId").HasColumnName("artikelId");
                    });
        });

        modelBuilder.Entity<Leverancier>(entity =>
        {
            entity.HasKey(e => e.LeveranciersId).HasName("PRIMARY");

            entity.ToTable("leveranciers");

            entity.HasIndex(e => e.PlaatsId, "fk_Leveranciers_Plaatsen1_idx");

            entity.Property(e => e.LeveranciersId).HasColumnName("leveranciersId");
            entity.Property(e => e.BtwNummer)
                .HasMaxLength(45)
                .HasColumnName("btwNummer");
            entity.Property(e => e.Bus)
                .HasMaxLength(5)
                .HasColumnName("bus");
            entity.Property(e => e.FamilienaamContactpersoon)
                .HasMaxLength(45)
                .HasColumnName("familienaamContactpersoon");
            entity.Property(e => e.HuisNummer)
                .HasMaxLength(5)
                .HasColumnName("huisNummer");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");
            entity.Property(e => e.PlaatsId).HasColumnName("plaatsId");
            entity.Property(e => e.Straat)
                .HasMaxLength(45)
                .HasColumnName("straat");
            entity.Property(e => e.VoornaamContactpersoon)
                .HasMaxLength(45)
                .HasColumnName("voornaamContactpersoon");

            entity.HasOne(d => d.Plaats).WithMany(p => p.Leveranciers)
                .HasForeignKey(d => d.PlaatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Leveranciers_Plaatsen1");
        });

        modelBuilder.Entity<Plaats>(entity =>
        {
            entity.HasKey(e => e.PlaatsId).HasName("PRIMARY");

            entity.ToTable("plaatsen");

            entity.Property(e => e.PlaatsId).HasColumnName("plaatsId");
            entity.Property(e => e.Naam)
                .HasMaxLength(150)
                .HasColumnName("plaats");
            entity.Property(e => e.Postcode)
                .HasMaxLength(4)
                .HasColumnName("postcode");
        });

        modelBuilder.Entity<Gebruikersaccount>(entity =>
        {
            entity.HasKey(e => e.GebruikersAccountId).HasName("PRIMARY");

            entity.ToTable("gebruikersaccounts");

            entity.HasIndex(e => e.Emailadres, "gebrukersnaam_UNIQUE").IsUnique();

            entity.Property(e => e.GebruikersAccountId).HasColumnName("gebruikersAccountId");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.Emailadres)
                .HasMaxLength(45)
                .HasColumnName("emailadres");
            entity.Property(e => e.Paswoord)
                .HasMaxLength(255)
                .HasColumnName("paswoord");
        });

        modelBuilder.Entity<PersoneelsLid>(entity =>
        {
            entity.HasKey(e => e.PersoneelslidId).HasName("PRIMARY");

            entity.ToTable("personeelsleden");

            entity.HasIndex(e => e.PersoneelslidAccountId, "fk_Personeelsleden_PersoneelslidAccounts1_idx");

            entity.Property(e => e.PersoneelslidId).HasColumnName("personeelslidId");
            entity.Property(e => e.Familienaam)
                .HasMaxLength(45)
                .HasColumnName("familienaam");
            entity.Property(e => e.InDienst)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("inDienst");
            entity.Property(e => e.PersoneelslidAccountId).HasColumnName("personeelslidAccountId");
            entity.Property(e => e.Voornaam)
                .HasMaxLength(45)
                .HasColumnName("voornaam");

            entity.HasOne(d => d.PersoneelslidAccount).WithOne(p => p.Personeelslid)
                .HasForeignKey<PersoneelsLid>(d => d.PersoneelslidAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Personeelsleden_PersoneelslidAccounts1");

            entity.HasMany(d => d.SecurityGroeps).WithMany(p => p.Personeelslids)
                .UsingEntity<Dictionary<string, object>>(
                    "Personeelslidsecuritygroepen",
                    r => r.HasOne<SecurityGroep>().WithMany()
                        .HasForeignKey("SecurityGroepId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_PersoneelslidSecurityGroepen_SecurityGroepen1"),
                    l => l.HasOne<PersoneelsLid>().WithMany()
                        .HasForeignKey("PersoneelslidId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_PersoneelslidSecurityGroepen_Personeelsleden1"),
                    j =>
                    {
                        j.HasKey("PersoneelslidId", "SecurityGroepId").HasName("PRIMARY");
                        j.ToTable("personeelslidsecuritygroepen");
                        j.HasIndex(new[] { "SecurityGroepId" }, "fk_PersoneelslidSecurityGroepen_SecurityGroepen1_idx");
                        j.IndexerProperty<int>("PersoneelslidId").HasColumnName("personeelslidId");
                        j.IndexerProperty<int>("SecurityGroepId").HasColumnName("securityGroepId");
                    });
        });

        modelBuilder.Entity<Personeelslidaccount>(entity =>
        {
            entity.HasKey(e => e.PersoneelslidAccountId).HasName("PRIMARY");

            entity.ToTable("personeelslidaccounts");

            entity.HasIndex(e => e.Emailadres, "emailadres_UNIQUE").IsUnique();

            entity.Property(e => e.PersoneelslidAccountId).HasColumnName("personeelslidAccountId");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.Emailadres)
                .HasMaxLength(45)
                .HasColumnName("emailadres");
            entity.Property(e => e.Paswoord)
                .HasMaxLength(255)
                .HasColumnName("paswoord");
        });

        modelBuilder.Entity<SecurityGroep>(entity =>
        {
            entity.HasKey(e => e.SecurityGroepId).HasName("PRIMARY");

            entity.ToTable("securitygroepen");

            entity.Property(e => e.SecurityGroepId).HasColumnName("securityGroepId");
            entity.Property(e => e.Naam)
                .HasMaxLength(45)
                .HasColumnName("naam");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
