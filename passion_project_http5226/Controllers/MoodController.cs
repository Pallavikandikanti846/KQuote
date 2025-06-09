using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using passion_project_http5226.Data;
using passion_project_http5226.Interfaces;
using passion_project_http5226.Models;

namespace passion_project_http5226.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MoodController : Controller
    {
        private readonly IMoodServices _MoodServices;

        public MoodController(IMoodServices MoodServices)
        {
            _MoodServices = MoodServices;
        }
        /// <summary>
        /// Returns a list of Moods, each represented by an MoodDto
        /// </summary>
        /// <returns>
        /// 200 OK
        /// </returns>
        /// <example>
        /// GET: api/Mood/MoodList ->
        ///
        /// </example>
        [HttpGet("MoodList")]
        public async Task<ActionResult<IEnumerable<MoodDto>>> MoodList()
        {

            // returns a list of Moods dtos
            IEnumerable<MoodDto> MoodDtos = await _MoodServices.ListMoods();
            // return 200 OK with MoodDtos
            return Ok(MoodDtos);
        }
        /// <summary>
        /// Returns a single Mood details specified by its {id}
        /// </summary>
        /// <param name="id">The Mood id</param>
        /// <returns>
        /// 200 OK
        /// {MoodDto}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/OrderItems/Find/5 -> 
        ///
        /// </example>
        [HttpGet(template: "FindMood/{id}")]
        public async Task<ActionResult<MoodDto>> FindMood(int id)
        {
            var aMood = await _MoodServices.FindMood(id);

            // if the Mood could not be located, return 404 Not Found
            if (aMood == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(aMood);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Updates a Mood
        /// </summary>
        /// <param name="id">The ID of the Mood to update</param>
        /// <param name="MoodDto">The required information to update the Mood</param>
        /// <returns>
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// or
        /// 204 No Content
        /// </returns>
        /// <example>
        /// PUT: api/Category/UpdateMood/17
        /// Request Headers: Content-Type: application/json
        /// Request Body: {MoodDto}
        /// ->
        /// Response Code: Mood with id " +id+ " Updated Successfully
        /// </example>
        [HttpPut(template: "UpdateMood/{id}")]
        public async Task<ActionResult> UpdateMood(int id, [FromBody] MoodDto MoodDto)
        {
            // {id} in URL must match Mood_id in POST Body
            if (id != MoodDto.mood_id)
            {
                //400 Bad Request
                return BadRequest();
            }

            ServiceResponse response = await _MoodServices.UpdateMood(MoodDto);

            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            //Status = Updated
            return Ok("Mood with id " + id + " Updated Successfully");

        }
        /// <summary>
        /// Adds a Mood
        /// </summary>
        /// <param name="MoodDto">The required information to add the Mood</param>
        /// <returns>
        /// 201 Created
        /// Location: api/Mood/FindMood/{MoodId}
        /// {MoodDto}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// POST: api/Mood/AddMood
        /// Request Headers: Content-Type: application/json
        /// Request Body: {MoodDto}
        /// ->
        /// Response Code: 201 Created
        /// Response Headers: Location: api/Mood/FindMood/{MoodId}
        /// </example>
        [HttpPost(template: "AddMood")]
        public async Task<ActionResult<Mood>> AddMood([FromBody] MoodDto MoodDto)
        {
            ServiceResponse response = await _MoodServices.AddMood(MoodDto);

            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            // returns 201 Created with Location
            return Created($"api/Mood/FindMood/{response.CreatedId}", MoodDto);
        }

        /// <summary>
        /// Deletes the Mood
        /// </summary>
        /// <param name="id">The id of the Mood to delete</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// DELETE: api/Mood/DeleteMood/7
        /// ->
        /// Response Code: Mood with id " +id+ " Deleted Successfully
        /// </example>
        [HttpDelete("DeleteMood/{id}")]
        public async Task<ActionResult> DeleteMood(int id)
        {
            ServiceResponse response = await _MoodServices.DeleteMood(id);

            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound();
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            return Ok("Mood with id " + id + " Deleted Successfully");

        }
    }
}
