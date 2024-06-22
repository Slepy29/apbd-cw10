using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Data;

public class ApplicationContext : DbContext
{


    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });
        

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "John1", LastName = "Doe1", Email = "e1@example.com" },
            new Doctor { IdDoctor = 2, FirstName = "John2", LastName = "Doe2", Email = "e2@example.com" },
            new Doctor { IdDoctor = 3, FirstName = "John3", LastName = "Doe3", Email = "e3@example.com" }
        );

        modelBuilder.Entity<Patient>().HasData(
            new Patient { IdPatient = 1, FirstName = "John1", LastName = "Doe1", Birthdate = new DateTime(2000, 1, 1) },
            new Patient { IdPatient = 2, FirstName = "John2", LastName = "Doe2", Birthdate = new DateTime(2000, 1, 1) },
            new Patient { IdPatient = 3, FirstName = "John3", LastName = "Doe3", Birthdate = new DateTime(2000, 1, 1) }
        );

        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { IdMedicament = 1, Name = "Medicament1", Description = "Description1", Type = "Type1" },
            new Medicament { IdMedicament = 2, Name = "Medicament2", Description = "Description2", Type = "Type2" },
            new Medicament { IdMedicament = 3, Name = "Medicament3", Description = "Description3", Type = "Type3" }
        );
        
        
    }
    

}