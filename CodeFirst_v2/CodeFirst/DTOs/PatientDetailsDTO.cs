namespace CodeFirst.DTOs;

public class PatientDetailsDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionDetailsDTO> Prescriptions { get; set; }
}