// CMD.Patient.API/Controllers/PatientController.cs
using Microsoft.AspNetCore.Mvc;
using CMD.Application.Interfaces;
using CMD.Application.DTOs;
using CMD.Domain.Enums;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CMD.API.Models;

namespace CMD.Patient.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<PatientDto>> AddPatient(PatientDto patientDto)
        {
            try
            {
                var result = await _patientService.AddPatientAsync(patientDto);
                return CreatedAtAction(nameof(GetPatientById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<PatientDto>> EditPatient(int id, PatientDto patientDto)
        {
            try
            {
                var result = await _patientService.EditPatientAsync(id, patientDto);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor,User")]
        public async Task<ActionResult<PatientDto>> GetPatientById(int id)
        {
            try
            {
                var result = await _patientService.GetPatientByIdAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PaginatedResult<PatientDto>>> GetAllPatients([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (patients, totalCount) = await _patientService.GetAllPatientsAsync(page, pageSize);
            var paginatedResult = new PaginatedResult<PatientDto>
            {
                Items = patients,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
            return Ok(paginatedResult);
        }

        [HttpPatch("{id}/activate")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> ActivatePatient(int id)
        {
            try
            {
                await _patientService.ActivatePatientAsync(id);
                return Ok("Status set to Active");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/inactivate")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> InactivatePatient(int id)
        {
            try
            {
                await _patientService.InactivatePatientAsync(id);
                return Ok("Status set to Inactive");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/primary-clinic")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> SelectPrimaryClinic(int id, [FromBody] Clinic clinic)
        {
            try
            {
                await _patientService.SelectPrimaryClinicAsync(id, clinic);
                return Ok("Primary Clinic is Set");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/primary-doctor")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> SelectPrimaryDoctor(int id, [FromBody] int doctorId)
        {
            try
            {
                await _patientService.SelectPrimaryDoctorAsync(id, doctorId);
                return Ok("Primary Doctor is Selected");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}