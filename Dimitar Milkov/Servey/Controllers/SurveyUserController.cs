using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using midTerm.Models.Models.SurveyUser;
using midTerm.Services.Abstractions;

namespace midTerm.Controllers
{
    /// <summary>
    /// Survey User API Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyUserController 
        : ControllerBase
    {
        private readonly ISurveyUserService _service;


        /// <summary>
        /// Survey User API Controller
        /// </summary>
        /// <param name="service">surveyUser service</param>
        public SurveyUserController(ISurveyUserService service)
        {
            _service = service;
        }


        /// <summary>
        /// Get Survey Users
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/SurveyUsers
        ///
        /// </remarks>
        /// <returns>An List of Base Survey Users model items</returns>
        /// <response code="200">All went well</response>
        /// <response code="204">Item is not found</response>
        /// <response code="400">If the item is null</response>
        /// <response code="500">server side error</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.Get();
            return Ok(result);
        }


        /// <summary>
        /// Get Survey User by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/SurveyUsers/{id}
        ///
        /// </remarks>
        /// <param name="id">identifier of the item</param>
        /// <returns>An Extended Survey User model item</returns>
        /// <response code="200">All went well</response>
        /// <response code="204">Item is not found</response>
        /// <response code="400">If the item is null</response>
        /// <response code="500">server side error</response> 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        /// <summary>
        /// Create SurveyUser
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/SurveyUser
        ///     {
        ///         "FirstName":"string",     
        ///         "LastName":"string",     
        ///         "DoB": "2021-01-01T12:09:49.553Z",
        ///         "Gender":gender, 
        ///         "Country":"string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="model">model to create</param>
        /// <returns>created item</returns>
        /// <response code="201">Returns the the created item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="405">Method not allowed</response>    
        /// <response code="409">If the item is not created</response>   
        /// <response code="500">server side error</response> 
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] SurveyUserCreate model)
        {
            if (ModelState.IsValid)
            {
                var user = await _service.Insert(model);
                return user != null
                    ? (IActionResult)CreatedAtRoute(nameof(GetById), user, user.Id)
                    : Conflict();
            }
            return BadRequest();
        }


        /// <summary>
        /// Update Survey User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/SurveyUser
        ///     {
        ///         "Id": 0,
        ///         "FirstName":"string",     
        ///         "LastName":"string",     
        ///         "DoB": "2021-01-01T12:09:49.553Z",
        ///         "Gender":gender, 
        ///         "Country":"string"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">identifier of the item</param>
        /// <param name="model">model to update</param>
        /// <returns>updated item</returns>
        /// <response code="201">Returns the the updated item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="405">Method not allowed</response>    
        /// <response code="409">If the item is not created</response>   
        /// <response code="500">server side error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SurveyUserUpdate model)
        {
            if (ModelState.IsValid)
            {
                model.Id = id;
                var result = await _service.Update(model);

                return result != null
                    ? (IActionResult)Ok(result)
                    : NoContent();
            }
            return BadRequest();
        }


        /// <summary>
        /// Deletes a Survey User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/SurveyUser/{id:int}
        /// 
        /// </remarks>
        /// <param name="id">identifier of the Survey User</param>
        /// <response code="200">true if deleted</response>
        /// <response code="400">If the item is not deleted</response>   
        /// <response code="405">Method not allowed</response>    
        /// <response code="500">server side error</response> 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.Delete(id));
            }
            return BadRequest();
        }
    }
}
