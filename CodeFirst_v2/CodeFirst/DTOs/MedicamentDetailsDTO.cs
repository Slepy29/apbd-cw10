namespace CodeFirst.DTOs;

public class MedicamentDetailsDTO
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; }
}