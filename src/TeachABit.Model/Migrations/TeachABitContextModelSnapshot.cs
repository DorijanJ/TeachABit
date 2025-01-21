﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TeachABit.Model;

#nullable disable

namespace TeachABit.Model.Migrations
{
    [DbContext(typeof(TeachABitContext))]
    partial class TeachABitContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TeachABit.Model.Models.Korisnici.Korisnik", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("ProfilnaSlikaVersion")
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int?>("VerifikacijaStatusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("VerifikacijaStatusId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("TeachABit.Model.Models.Korisnici.VerifikacijaStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("VerifikacijaStatus");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Objave.Komentar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastUpdatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("NadKomentarId")
                        .HasColumnType("integer");

                    b.Property<int>("ObjavaId")
                        .HasColumnType("integer");

                    b.Property<bool?>("OznacenTocan")
                        .HasColumnType("boolean");

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VlasnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NadKomentarId");

                    b.HasIndex("ObjavaId");

                    b.HasIndex("VlasnikId");

                    b.ToTable("Komentar");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Objave.KomentarReakcija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("KomentarId")
                        .HasColumnType("integer");

                    b.Property<string>("KorisnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Liked")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("KomentarId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("KomentarReakcija");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Objave.Objava", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VlasnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("VlasnikId");

                    b.ToTable("Objava");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Objave.ObjavaReakcija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("KorisnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Liked")
                        .HasColumnType("boolean");

                    b.Property<int>("ObjavaId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("ObjavaId");

                    b.ToTable("ObjavaReakcija");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Radionice.KomentarRadionica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastUpdatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("NadKomentarId")
                        .HasColumnType("integer");

                    b.Property<int>("RadionicaId")
                        .HasColumnType("integer");

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VlasnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NadKomentarId");

                    b.HasIndex("RadionicaId");

                    b.HasIndex("VlasnikId");

                    b.ToTable("KomentarRadionica");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Radionice.KomentarRadionicaReakcija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("KomentarId")
                        .HasColumnType("integer");

                    b.Property<string>("KorisnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Liked")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("KomentarId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("KomentarRadionicaReakcija");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Radionice.Radionica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Cijena")
                        .HasColumnType("numeric");

                    b.Property<int>("MaksimalniKapacitet")
                        .HasColumnType("integer");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VlasnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("VrijemeRadionice")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("VlasnikId");

                    b.ToTable("Radionica");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Radionice.RadionicaFavorit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("KorisnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RadionicaId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("RadionicaId");

                    b.ToTable("RadionicaFavorit");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.KomentarTecaj", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastUpdatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("NadKomentarId")
                        .HasColumnType("integer");

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TecajId")
                        .HasColumnType("integer");

                    b.Property<string>("VlasnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NadKomentarId");

                    b.HasIndex("TecajId");

                    b.HasIndex("VlasnikId");

                    b.ToTable("KomentarTecaj");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.KomentarTecajReakcija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("KomentarId")
                        .HasColumnType("integer");

                    b.Property<string>("KorisnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Liked")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("KomentarId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("KomentarTecajReakcija");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.Lekcija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastUpdatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TecajId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TecajId");

                    b.ToTable("Lekcija");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.Tecaj", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Cijena")
                        .HasColumnType("numeric");

                    b.Property<string>("NaslovnaSlikaVersion")
                        .HasColumnType("text");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VlasnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isPublished")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("VlasnikId");

                    b.ToTable("Tecaj");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.TecajFavorit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("KorisnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TecajId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("TecajId");

                    b.ToTable("TecajFavorit");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.TecajPlacanje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("KorisnikId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TecajId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("TecajId");

                    b.ToTable("TecajPlacanje");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Uloge.Uloga", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<int>("LevelPristupa")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Uloge.Uloga", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Uloge.Uloga", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeachABit.Model.Models.Korisnici.Korisnik", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Korisnici.VerifikacijaStatus", "VerifikacijaStatus")
                        .WithMany()
                        .HasForeignKey("VerifikacijaStatusId");

                    b.Navigation("VerifikacijaStatus");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Objave.Komentar", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Objave.Komentar", "NadKomentar")
                        .WithMany("PodKomentarList")
                        .HasForeignKey("NadKomentarId");

                    b.HasOne("TeachABit.Model.Models.Objave.Objava", "Objava")
                        .WithMany("Komentari")
                        .HasForeignKey("ObjavaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Vlasnik")
                        .WithMany()
                        .HasForeignKey("VlasnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NadKomentar");

                    b.Navigation("Objava");

                    b.Navigation("Vlasnik");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Objave.KomentarReakcija", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Objave.Komentar", "Komentar")
                        .WithMany("KomentarReakcijaList")
                        .HasForeignKey("KomentarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Korisnik")
                        .WithMany("KomentarReakcijaList")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Komentar");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Objave.Objava", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Vlasnik")
                        .WithMany("Objave")
                        .HasForeignKey("VlasnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vlasnik");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Objave.ObjavaReakcija", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Korisnik")
                        .WithMany("ObjavaReakcijaList")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachABit.Model.Models.Objave.Objava", "Objava")
                        .WithMany("ObjavaReakcijaList")
                        .HasForeignKey("ObjavaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("Objava");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Radionice.KomentarRadionica", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Radionice.KomentarRadionica", "NadKomentar")
                        .WithMany("PodKomentarList")
                        .HasForeignKey("NadKomentarId");

                    b.HasOne("TeachABit.Model.Models.Radionice.Radionica", "Radionica")
                        .WithMany("Komentari")
                        .HasForeignKey("RadionicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Vlasnik")
                        .WithMany()
                        .HasForeignKey("VlasnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NadKomentar");

                    b.Navigation("Radionica");

                    b.Navigation("Vlasnik");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Radionice.KomentarRadionicaReakcija", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Radionice.KomentarRadionica", "Komentar")
                        .WithMany("KomentarRadionicaReakcijaList")
                        .HasForeignKey("KomentarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Komentar");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Radionice.Radionica", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Vlasnik")
                        .WithMany()
                        .HasForeignKey("VlasnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vlasnik");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Radionice.RadionicaFavorit", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachABit.Model.Models.Radionice.Radionica", "Radionica")
                        .WithMany()
                        .HasForeignKey("RadionicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("Radionica");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.KomentarTecaj", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Tecajevi.KomentarTecaj", "NadKomentar")
                        .WithMany("PodKomentarList")
                        .HasForeignKey("NadKomentarId");

                    b.HasOne("TeachABit.Model.Models.Tecajevi.Tecaj", "Tecaj")
                        .WithMany("Komentari")
                        .HasForeignKey("TecajId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Vlasnik")
                        .WithMany()
                        .HasForeignKey("VlasnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NadKomentar");

                    b.Navigation("Tecaj");

                    b.Navigation("Vlasnik");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.KomentarTecajReakcija", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Tecajevi.KomentarTecaj", "Komentar")
                        .WithMany("KomentarReakcijaList")
                        .HasForeignKey("KomentarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Komentar");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.Lekcija", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Tecajevi.Tecaj", "Tecaj")
                        .WithMany("Lekcije")
                        .HasForeignKey("TecajId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tecaj");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.Tecaj", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Vlasnik")
                        .WithMany()
                        .HasForeignKey("VlasnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vlasnik");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.TecajFavorit", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachABit.Model.Models.Tecajevi.Tecaj", "Tecaj")
                        .WithMany()
                        .HasForeignKey("TecajId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("Tecaj");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.TecajPlacanje", b =>
                {
                    b.HasOne("TeachABit.Model.Models.Korisnici.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeachABit.Model.Models.Tecajevi.Tecaj", "Tecaj")
                        .WithMany("TecajPlacanja")
                        .HasForeignKey("TecajId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("Tecaj");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Korisnici.Korisnik", b =>
                {
                    b.Navigation("KomentarReakcijaList");

                    b.Navigation("ObjavaReakcijaList");

                    b.Navigation("Objave");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Objave.Komentar", b =>
                {
                    b.Navigation("KomentarReakcijaList");

                    b.Navigation("PodKomentarList");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Objave.Objava", b =>
                {
                    b.Navigation("Komentari");

                    b.Navigation("ObjavaReakcijaList");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Radionice.KomentarRadionica", b =>
                {
                    b.Navigation("KomentarRadionicaReakcijaList");

                    b.Navigation("PodKomentarList");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Radionice.Radionica", b =>
                {
                    b.Navigation("Komentari");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.KomentarTecaj", b =>
                {
                    b.Navigation("KomentarReakcijaList");

                    b.Navigation("PodKomentarList");
                });

            modelBuilder.Entity("TeachABit.Model.Models.Tecajevi.Tecaj", b =>
                {
                    b.Navigation("Komentari");

                    b.Navigation("Lekcije");

                    b.Navigation("TecajPlacanja");
                });
#pragma warning restore 612, 618
        }
    }
}
