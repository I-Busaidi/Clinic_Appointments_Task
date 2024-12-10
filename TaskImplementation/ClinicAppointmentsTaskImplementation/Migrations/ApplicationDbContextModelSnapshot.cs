﻿// <auto-generated />
using System;
using ClinicAppointmentsTaskImplementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClinicAppointmentsTaskImplementation.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClinicAppointmentsTaskImplementation.Models.Appointment", b =>
                {
                    b.Property<int>("patientId")
                        .HasColumnType("int");

                    b.Property<int>("clinicId")
                        .HasColumnType("int");

                    b.Property<int>("appointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("appointmentId"));

                    b.Property<DateTime>("appointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("slotNumber")
                        .HasColumnType("int");

                    b.HasKey("patientId", "clinicId", "appointmentId");

                    b.HasIndex("clinicId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("ClinicAppointmentsTaskImplementation.Models.Clinic", b =>
                {
                    b.Property<int>("clinicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("clinicId"));

                    b.Property<string>("clinicSpec")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("numberOfSlots")
                        .HasColumnType("int");

                    b.HasKey("clinicId");

                    b.HasIndex("clinicSpec")
                        .IsUnique();

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("ClinicAppointmentsTaskImplementation.Models.Patient", b =>
                {
                    b.Property<int>("patientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("patientId"));

                    b.Property<int>("patientAge")
                        .HasColumnType("int");

                    b.Property<string>("patientGender")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("patientName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("patientId");

                    b.HasIndex("patientName")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("ClinicAppointmentsTaskImplementation.Models.Appointment", b =>
                {
                    b.HasOne("ClinicAppointmentsTaskImplementation.Models.Clinic", "Clinic")
                        .WithMany("ClinicAppointments")
                        .HasForeignKey("clinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClinicAppointmentsTaskImplementation.Models.Patient", "Patient")
                        .WithMany("PatientAppointments")
                        .HasForeignKey("patientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("ClinicAppointmentsTaskImplementation.Models.Clinic", b =>
                {
                    b.Navigation("ClinicAppointments");
                });

            modelBuilder.Entity("ClinicAppointmentsTaskImplementation.Models.Patient", b =>
                {
                    b.Navigation("PatientAppointments");
                });
#pragma warning restore 612, 618
        }
    }
}