namespace CodeFirst.DTOs;


public class PrescriptionRequestDTO
{
    public int IdPatient { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public DateTime PatientBirthDate { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
    public List<PrescriptionMedicamentDTO> Medicaments { get; set; }
}