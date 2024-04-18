﻿// <auto-generated />
using Faculty_Service.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FacultyService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240418140220_addIdField")]
    partial class addIdField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Faculty_Service.Models.Abiturient", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("accessToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Abiturients");
                });

            modelBuilder.Entity("Faculty_Service.Models.EducationDocumentType", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("levelId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("levelId");

                    b.ToTable("EducationDocumentTypes");
                });

            modelBuilder.Entity("Faculty_Service.Models.EducationProgram", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("educationForm")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("facultyId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("language")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("levelId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("facultyId");

                    b.HasIndex("levelId");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("Faculty_Service.Models.Faculty", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Faculty_Service.Models.Level", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("Faculty_Service.Models.NextLevel", b =>
                {
                    b.Property<string>("uuid")
                        .HasColumnType("text");

                    b.Property<string>("levelId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nextLevelId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("uuid");

                    b.HasIndex("levelId");

                    b.HasIndex("nextLevelId");

                    b.ToTable("NextLevels");
                });

            modelBuilder.Entity("Faculty_Service.Models.EducationDocumentType", b =>
                {
                    b.HasOne("Faculty_Service.Models.Level", "level")
                        .WithMany("educationDocumentTypes")
                        .HasForeignKey("levelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("level");
                });

            modelBuilder.Entity("Faculty_Service.Models.EducationProgram", b =>
                {
                    b.HasOne("Faculty_Service.Models.Faculty", "faculty")
                        .WithMany("programs")
                        .HasForeignKey("facultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Faculty_Service.Models.Level", "level")
                        .WithMany()
                        .HasForeignKey("levelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("faculty");

                    b.Navigation("level");
                });

            modelBuilder.Entity("Faculty_Service.Models.NextLevel", b =>
                {
                    b.HasOne("Faculty_Service.Models.Level", "level")
                        .WithMany("nextLevels")
                        .HasForeignKey("levelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Faculty_Service.Models.Level", "nextLevel")
                        .WithMany()
                        .HasForeignKey("nextLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("level");

                    b.Navigation("nextLevel");
                });

            modelBuilder.Entity("Faculty_Service.Models.Faculty", b =>
                {
                    b.Navigation("programs");
                });

            modelBuilder.Entity("Faculty_Service.Models.Level", b =>
                {
                    b.Navigation("educationDocumentTypes");

                    b.Navigation("nextLevels");
                });
#pragma warning restore 612, 618
        }
    }
}
