﻿// <auto-generated />
using Application_Service.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApplicationService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Application_Service.Models.Abiturient", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("accessToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("birthDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("managerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nationality")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Abiturients");
                });

            modelBuilder.Entity("Application_Service.Models.Application", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("date_edu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("date_pas")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("document_type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("grade")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("number_edu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("number_pas")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("series")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Application_Service.Models.ApplicationProgram", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<int>("priority")
                        .HasColumnType("integer");

                    b.Property<string>("programId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("programId");

                    b.ToTable("ApplicationPrograms");
                });

            modelBuilder.Entity("Application_Service.Models.EducationProgram", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("Application_Service.Models.Application", b =>
                {
                    b.HasOne("Application_Service.Models.Abiturient", "abiturient")
                        .WithOne("application")
                        .HasForeignKey("Application_Service.Models.Application", "id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("abiturient");
                });

            modelBuilder.Entity("Application_Service.Models.ApplicationProgram", b =>
                {
                    b.HasOne("Application_Service.Models.Application", "application")
                        .WithMany("applicationPrograms")
                        .HasForeignKey("id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application_Service.Models.EducationProgram", "educationProgram")
                        .WithMany("applicationPrograms")
                        .HasForeignKey("programId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("application");

                    b.Navigation("educationProgram");
                });

            modelBuilder.Entity("Application_Service.Models.Abiturient", b =>
                {
                    b.Navigation("application")
                        .IsRequired();
                });

            modelBuilder.Entity("Application_Service.Models.Application", b =>
                {
                    b.Navigation("applicationPrograms");
                });

            modelBuilder.Entity("Application_Service.Models.EducationProgram", b =>
                {
                    b.Navigation("applicationPrograms");
                });
#pragma warning restore 612, 618
        }
    }
}
