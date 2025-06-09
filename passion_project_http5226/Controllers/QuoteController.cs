using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using passion_project_http5226.Data;
using passion_project_http5226.Interfaces;
using passion_project_http5226.Models;

namespace passion_project_http5226.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class QuoteController : Controller
    {
        private readonly IQuoteServices _QuoteServices;

        public QuoteController(IQuoteServices QuoteServices)
        {
            _QuoteServices = QuoteServices;
        }
        /// <summary>
        /// Returns a list of Quotes, each represented by an QuoteDto
        /// </summary>
        /// <returns>
        /// 200 OK
        /// </returns>
        /// <example>
        /// GET: api/Quote/QuoteList ->
        ///[{"quote_id":1,"content":"Even if we’re not together, I’ll always be on your side.","actor":"Hyun Bin","episode":16,"drama_id":0,"title":null},{"quote_id":2,"content":"Fate brought you to me.","actor":"Son Ye-jin","episode":10,"drama_id":0,"title":null},{"quote_id":3,"content":"Every moment I spent with you shined.","actor":"Gong Yoo","episode":16,"drama_id":0,"title":null},{"quote_id":4,"content":"I want to be your last love.","actor":"Kim Go-eun","episode":12,"drama_id":0,"title":null},{"quote_id":5,"content":"I don’t have to win. I just have to not lose.","actor":"Park Seo-joon","episode":8,"drama_id":0,"title":null},{"quote_id":6,"content":"Pain is meant to be felt.","actor":"Lee Sun-kyun","episode":5,"drama_id":0,"title":null},{"quote_id":7,"content":"You’ll always have a home to return to.","actor":"Ryu Jun-yeol","episode":20,"drama_id":0,"title":null},{"quote_id":8,"content":"Evil is punished by evil.","actor":"Song Joong-ki","episode":15,"drama_id":0,"title":null},{"quote_id":9,"content":"Even in death, I’ll protect you.","actor":"IU","episode":10,"drama_id":0,"title":null},{"quote_id":10,"content":"It’s okay to not be okay.","actor":"Kim Soo-hyun","episode":16,"drama_id":0,"title":null},{"quote_id":11,"content":"Dreams are not something you wait for.","actor":"Nam Joo-hyuk","episode":3,"drama_id":0,"title":null},{"quote_id":12,"content":"You’ll regret rejecting me.","actor":"Kim Se-jeong","episode":2,"drama_id":0,"title":null},{"quote_id":13,"content":"Healing is mutual.","actor":"Shin Min-a","episode":14,"drama_id":0,"title":null},{"quote_id":14,"content":"Not everything that exists can be explained.","actor":"Lee Min-ho","episode":9,"drama_id":0,"title":null},{"quote_id":15,"content":"I’m different, not less.","actor":"Park Eun-bin","episode":6,"drama_id":0,"title":null}]
        /// </example>
        [HttpGet("QuoteList")]
        public async Task<ActionResult<IEnumerable<QuoteDto>>> QuoteList()
        {

            // returns a list of Quotes dtos
            IEnumerable<QuoteDto> QuoteDtos = await _QuoteServices.ListQuotes();
            // return 200 OK with QuoteDtos
            return Ok(QuoteDtos);
        }
        /// <summary>
        /// Returns a single Quote details specified by its { id }
        /// </summary>
        /// <param name = "id" > The Quote id</param>
        /// <returns>
        /// 200 OK
        /// { QuoteDto}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/OrderItems/Find/4 -> 
        ///{"quote_id":4,"content":"I want to be your last love.","actor":"Kim Go-eun","episode":12,"drama_id":0,"title":null}
        /// </example>
        [HttpGet(template: "FindQuote/{id}")]
        public async Task<ActionResult<QuoteDto>> FindQuote(int id)
        {
            var aQuote = await _QuoteServices.FindQuote(id);

            // if the Quote could not be located, return 404 Not Found
            if (aQuote == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(aQuote);
            }
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        ///// <summary>
        ///// Updates a Quote
        ///// </summary>
        ///// <param name="id">The ID of the Quote to update</param>
        ///// <param name="QuoteDto">The required information to update the Quote</param>
        ///// <returns>
        ///// 400 Bad Request
        ///// or
        ///// 404 Not Found
        ///// or
        ///// 204 No Content
        ///// </returns>
        ///// <example>
        ///// PUT: api/Category/UpdateQuote/17
        ///// Request Headers: Content-Type: application/json
        ///// Request Body: {QuoteDto}
        ///// ->
        ///// Response Code: Quote with id " +id+ " Updated Successfully
        ///// </example>
        //[HttpPut(template: "UpdateQuote/{id}")]
        //public async Task<ActionResult> UpdateQuote(int id, [FromBody] QuoteDto QuoteDto)
        //{
        //    // {id} in URL must match Quote_id in POST Body
        //    if (id != QuoteDto.quote_id)
        //    {
        //        //400 Bad Request
        //        return BadRequest();
        //    }

        //    ServiceResponse response = await _QuoteServices.UpdateQuote(QuoteDto);

        //    if (response.Status == ServiceResponse.ServiceStatus.NotFound)
        //    {
        //        return NotFound(response.Messages);
        //    }
        //    else if (response.Status == ServiceResponse.ServiceStatus.Error)
        //    {
        //        return StatusCode(500, response.Messages);
        //    }

        //    //Status = Updated
        //    return Ok("Quote with id " + id + " Updated Successfully");

        //}
        ///// <summary>
        ///// Adds a Quote
        ///// </summary>
        ///// <param name="QuoteDto">The required information to add the Quote</param>
        ///// <returns>
        ///// 201 Created
        ///// Location: api/Quote/FindQuote/{QuoteId}
        ///// {QuoteDto}
        ///// or
        ///// 404 Not Found
        ///// </returns>
        ///// <example>
        ///// POST: api/Quote/AddQuote
        ///// Request Headers: Content-Type: application/json
        ///// Request Body: {QuoteDto}
        ///// ->
        ///// Response Code: 201 Created
        ///// Response Headers: Location: api/Quote/FindQuote/{QuoteId}
        ///// </example>
        //[HttpPost(template: "AddQuote")]
        //public async Task<ActionResult<Quote>> AddQuote([FromBody] QuoteDto QuoteDto)
        //{
        //    ServiceResponse response = await _QuoteServices.AddQuote(QuoteDto);

        //    if (response.Status == ServiceResponse.ServiceStatus.NotFound)
        //    {
        //        return NotFound(response.Messages);
        //    }
        //    else if (response.Status == ServiceResponse.ServiceStatus.Error)
        //    {
        //        return StatusCode(500, response.Messages);
        //    }

        //    // returns 201 Created with Location
        //    return Created($"api/Quote/FindQuote/{response.CreatedId}", QuoteDto);
        //}

        ///// <summary>
        ///// Deletes the Quote
        ///// </summary>
        ///// <param name="id">The id of the Quote to delete</param>
        ///// <returns>
        ///// 204 No Content
        ///// or
        ///// 404 Not Found
        ///// </returns>
        ///// <example>
        ///// DELETE: api/Quote/DeleteQuote/7
        ///// ->
        ///// Response Code: Quote with id " +id+ " Deleted Successfully
        ///// </example>
        //[HttpDelete("DeleteQuote/{id}")]
        //public async Task<ActionResult> DeleteQuote(int id)
        //{
        //    ServiceResponse response = await _QuoteServices.DeleteQuote(id);

        //    if (response.Status == ServiceResponse.ServiceStatus.NotFound)
        //    {
        //        return NotFound();
        //    }
        //    else if (response.Status == ServiceResponse.ServiceStatus.Error)
        //    {
        //        return StatusCode(500, response.Messages);
        //    }

        //    return Ok("Quote with id " + id + " Deleted Successfully");

        //}
    }
}
