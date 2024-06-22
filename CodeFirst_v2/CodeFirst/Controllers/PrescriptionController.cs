using CodeFirst.DTOs;
using CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    
    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    
    [HttpPost("add")]
    public async Task<IActionResult> AddNewPrescription([FromBody] PrescriptionRequestDTO request)
    {
        if (request == null || request.Medicaments == null || request.Medicaments.Count > 10)
        {
            return BadRequest("The request cannot be null, must include medicaments, and can contain a maximum of 10 medicaments.");
        }

        var result = await _prescriptionService.AddNewPrescriptionAsync(request);

        if (!result)
        {
            return BadRequest("Error adding prescription.");
        }

        return Ok("New prescription added successfully.");
    }

    [HttpGet("patient/{idPatient}")]
    public async Task<IActionResult> GetPatientDetails(int idPatient)
    {
        var patientDetails = await _prescriptionService.GetPatientDetailsAsync(idPatient);

        if (patientDetails == null)
        {
            return NotFound("Patient details not found.");
        }

        return Ok(patientDetails);
    }
}