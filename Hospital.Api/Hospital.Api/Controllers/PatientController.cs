using System;
using System.Threading.Tasks;
using Hospital.Core.Interfaces;
using Hospital.Data.Exceptions;
using Hospital.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Patient")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }


        /// <summary>
        /// Returns all patients
        /// </summary>
        /// <param name="limit">Optional numeric paremeter. Limits returned collection.</param>
        /// <returns>Array of patients</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int? limit)
        {

            try
            {
                return Ok(await _patientService.GetAllPatients(limit));
            }
            catch (CouchDbException e)
            {

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }



        }

        /// <summary>
        /// Returns patient
        /// </summary>
        /// <param name="id">Patient's id</param>
        /// <returns>Patient</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                return Ok(await _patientService.GetPatient(id));
            }
            catch (CouchDbException e)
            {
                if (e.Message == "not_found")
                {
                    return NotFound();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Adds Patient to database
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Patient
        /// {
        ///    "firstName": "Jan",
        ///    "lastName": "Kowalski",
        ///    "address": [
        ///    "Warszawa",
        ///    "Bemowo",
        ///    "Kaliskiego 17"
        ///        ],
        ///    "nfzInsurance": true,
        ///    "nfzInsuranceValidDate": "2019-07-16T08:58:07.144Z"
        /// }
        ///
        /// </remarks>
        /// <param name="patient">Patient</param>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]Patient patient)
        {
            try
            {
                return Ok(await _patientService.AddPatient(patient));
            }
            catch (CouchDbException e)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates Patient
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Patient
        ///     {
        ///        "_id" : "id",
        ///        "_rev" : "rev",
        ///        "$doctype": "doctor",
        ///        "firstName": "Karol",
        ///        "lastName": "Wielki",
        ///        "professions": [
        ///             "Chirurg",
        ///             "Internista"
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="patient">Patient</param>
        /// <returns>Updated patient</returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put([FromBody]Patient patient)
        {
            try
            {
                return Ok(await _patientService.UpdatePatient(patient));
            }
            catch (CouchDbException e)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes patient
        /// </summary>
        /// <param name="id">Patients's id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var doctor = await _patientService.GetPatient(id);
                await _patientService.DeletePatient(doctor);
                return Ok();
            }
            catch (CouchDbException e)
            {
                if (e.Message == "not_found")
                {
                    return NotFound();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
