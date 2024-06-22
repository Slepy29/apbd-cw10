using CodeFirst.DTOs;


namespace CodeFirst.Services
{
    public interface IPrescriptionService
    {
        Task<bool> AddNewPrescriptionAsync(PrescriptionRequestDTO request);
        Task<PatientDetailsDTO> GetPatientDetailsAsync(int patientId);
    }
}