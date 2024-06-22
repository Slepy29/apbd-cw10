using CodeFirst.Models;

namespace CodeFirst.Repositories
{
    public interface IPrescriptionRepository
    {
        Task AddPrescriptionAsync(Prescription prescription);
        Task<Patient> GetPatientWithDetailsAsync(int patientId);
        Task<bool> MedicamentExistsAsync(int medicamentId);
    }
}