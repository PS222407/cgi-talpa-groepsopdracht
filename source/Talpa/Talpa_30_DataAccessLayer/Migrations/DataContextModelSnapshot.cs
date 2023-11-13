﻿// <auto-generated />
using System;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BusinessLogicLayer.Models.DateVote", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("OutingDateId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "OutingDateId");

                    b.HasIndex("OutingDateId");

                    b.ToTable("DateVotes");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.Outing", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeadLine")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Outings");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.OutingDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OutingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OutingId");

                    b.ToTable("OutingDates");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.Restriction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Restrictions");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.Suggestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.SuggestionDate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SuggestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SuggestionId");

                    b.ToTable("SuggestionDates");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.SuggestionVote", b =>
                {
                    b.Property<int>("SuggestionId")
                        .HasColumnType("int");

                    b.Property<int>("OutingId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("SuggestionId", "OutingId", "UserId");

                    b.HasIndex("OutingId");

                    b.ToTable("SuggestionVote");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.Team", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("OutingSuggestion", b =>
                {
                    b.Property<int>("OutingId")
                        .HasColumnType("int");

                    b.Property<int>("SuggestionsId")
                        .HasColumnType("int");

                    b.HasKey("OutingId", "SuggestionsId");

                    b.HasIndex("SuggestionsId");

                    b.ToTable("OutingSuggestion");
                });

            modelBuilder.Entity("RestrictionSuggestion", b =>
                {
                    b.Property<int>("RestrictionsId")
                        .HasColumnType("int");

                    b.Property<int>("SuggestionsId")
                        .HasColumnType("int");

                    b.HasKey("RestrictionsId", "SuggestionsId");

                    b.HasIndex("SuggestionsId");

                    b.ToTable("RestrictionSuggestion");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.DateVote", b =>
                {
                    b.HasOne("BusinessLogicLayer.Models.OutingDate", "OutingDate")
                        .WithMany("DateVotes")
                        .HasForeignKey("OutingDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OutingDate");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.Outing", b =>
                {
                    b.HasOne("BusinessLogicLayer.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.OutingDate", b =>
                {
                    b.HasOne("BusinessLogicLayer.Models.Outing", "Outing")
                        .WithMany("OutingDates")
                        .HasForeignKey("OutingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Outing");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.SuggestionDate", b =>
                {
                    b.HasOne("BusinessLogicLayer.Models.Suggestion", "Suggestion")
                        .WithMany()
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.SuggestionVote", b =>
                {
                    b.HasOne("BusinessLogicLayer.Models.Outing", "Outing")
                        .WithMany("SuggestionVotes")
                        .HasForeignKey("OutingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLogicLayer.Models.Suggestion", "Suggestion")
                        .WithMany("SuggestionVotes")
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Outing");

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("OutingSuggestion", b =>
                {
                    b.HasOne("BusinessLogicLayer.Models.Outing", null)
                        .WithMany()
                        .HasForeignKey("OutingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLogicLayer.Models.Suggestion", null)
                        .WithMany()
                        .HasForeignKey("SuggestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RestrictionSuggestion", b =>
                {
                    b.HasOne("BusinessLogicLayer.Models.Restriction", null)
                        .WithMany()
                        .HasForeignKey("RestrictionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLogicLayer.Models.Suggestion", null)
                        .WithMany()
                        .HasForeignKey("SuggestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.Outing", b =>
                {
                    b.Navigation("OutingDates");

                    b.Navigation("SuggestionVotes");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.OutingDate", b =>
                {
                    b.Navigation("DateVotes");
                });

            modelBuilder.Entity("BusinessLogicLayer.Models.Suggestion", b =>
                {
                    b.Navigation("SuggestionVotes");
                });
#pragma warning restore 612, 618
        }
    }
}
