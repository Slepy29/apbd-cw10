namespace CodeFirst.DTOs;

public class PrescriptionDetailsDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDetailsDTO> Medicaments { get; set; }
    public DoctorDetailsDTO Doctor { get; set; }
}