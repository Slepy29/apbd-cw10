using CodeFirst.DTOs;
using CodeFirst.Models;
using CodeFirst.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        public async Task<bool> AddNewPrescriptionAsync(PrescriptionRequestDTO request)
        {
            if (request.DueDate < request.Date)
            {
                return false;
            }

            var patient = await GetOrCreatePatientAsync(request);
            var prescription = CreatePrescription(request, patient);

            if (!await AddPrescriptionMedicamentsAsync(request.Medicaments, prescription))
            {
                return false;
            }

            try
            {
                await _prescriptionRepository.AddPrescriptionAsync(prescription);
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine($"Error adding prescription: {e.Message}");
                return false;
            }

            return true;
        }

        private async Task<Patient> GetOrCreatePatientAsync(PrescriptionRequestDTO request)
        {
            return await _prescriptionRepository.GetPatientWithDetailsAsync(request.IdPatient) ?? new Patient
            {
                FirstName = request.PatientFirstName,
                LastName = request.PatientLastName,
                Birthdate = request.PatientBirthDate
            };
        }

        private static Prescription CreatePrescription(PrescriptionRequestDTO request, Patient patient)
        {
            return new Prescription
            {
                Date = request.Date,
                DueDate = request.DueDate,
                Patient = patient,
                IdDoctor = request.IdDoctor,
                PrescriptionMedicaments = new List<PrescriptionMedicament>()
            };
        }

        private async Task<bool> AddPrescriptionMedicamentsAsync(List<PrescriptionMedicamentDTO> medicaments, Prescription prescription)
        {
            foreach (var med in medicaments)
            {
                if (!await _prescriptionRepository.MedicamentExistsAsync(med.IdMedicament))
                {
                    return false;
                }

                prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
                {
                    IdMedicament = med.IdMedicament,
                    Dose = med.Dose,
                    Details = med.Description
                });
            }
            return true;
        }

        public async Task<PatientDetailsDTO> GetPatientDetailsAsync(int patientId)
        {
            var patient = await _prescriptionRepository.GetPatientWithDetailsAsync(patientId);

            if (patient == null)
            {
                return null;
            }

            return MapPatientDetails(patient);
        }

        private static PatientDetailsDTO MapPatientDetails(Patient patient)
        {
            return new PatientDetailsDTO
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthdate = patient.Birthdate,
                Prescriptions = patient.Prescriptions.OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionDetailsDTO
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Medicaments = pr.PrescriptionMedicaments
                            .Select(pm => new MedicamentDetailsDTO
                            {
                                IdMedicament = pm.IdMedicament,
                                Name = pm.Medicament.Name,
                                Dose = pm.Dose,
                                Description = pm.Medicament.Description
                            }).ToList(),
                        Doctor = new DoctorDetailsDTO
                        {
                            IdDoctor = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName = pr.Doctor.LastName,
                            Email = pr.Doctor.Email
                        }
                    }).ToList()
            };
        }
    }
}
