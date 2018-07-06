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

        // GET: api/Doctor/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
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

        // PUT: api/Doctor/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
