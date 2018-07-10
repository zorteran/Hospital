using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Core.Interfaces;
using Hospital.Data.Exceptions;
using Hospital.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Doctor")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }


        // GET: api/Doctor
        /// <summary>
        /// Returns all doctors
        /// </summary>
        /// <param name="limit">Optional numeric paremeter. Limits returned collection.</param>
        /// <returns>Array of doctors</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int? limit)
        {

            try
            {
                return Ok(await _doctorService.GetAllDoctors(limit));
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
        /// Returns doctor
        /// </summary>
        /// <param name="id">Doctor's id</param>
        /// <returns>Doctor</returns>
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                return Ok(await _doctorService.GetDoctor(id));
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
        /// Adds Doctor to database
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Doctor
        ///     {
        ///        "$doctype": "doctor",
        ///        "firstName": "Karol",
        ///        "lastName": "Wielki",
        ///        "professions": [
        ///             "Chirurg",
        ///             "Internista"
        ///         ]
        ///     }
        ///
        /// </remarks>
        /// <param name="doc">Doctor</param>
        // POST: api/Doctor
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]Doctor doc)
        {
            try
            {
                return Ok(await _doctorService.AddDoctor(doc));
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
        /// Updates Doctor
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Doctor
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
        /// <param name="doc">Doctor</param>
        /// <returns>Updated doctor</returns>
        // PUT: api/Doctor/5
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put([FromBody]Doctor doc)
        {
            try
            {
                return Ok(await _doctorService.UpdateDoctor(doc));
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
        /// Deletes doctor
        /// </summary>
        /// <param name="id">Doctor's id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctor(id);
                await _doctorService.DeleteDoctor(doctor);
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
