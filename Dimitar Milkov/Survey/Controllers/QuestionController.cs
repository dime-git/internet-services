using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Net.Mime;
using System.Threading.Tasks;
using Survey.Models.Models.Question;
using Survey.Services.Abstractions;

namespace Survey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _service;

        public QuestionController(IQuestionService service)
        {
            _service = service;
        }

        [HttpGet("Questions/{id:int}", Name = nameof(GetQuestion))]
        public async Task<IActionResult> GetQuestion([FromRoute] int id)
        {
            var question = await _service.GetById(id);
            return question != null ? Ok(question) : NoContent();
        }

        [HttpGet("Questions/WithOpts", Name = nameof(GetQuestionsWithOpts))]
        public async Task<IActionResult> GetQuestionsWithOpts()
        {
            var questions = await _service.GetFull();
            return questions != null && questions.Any() ? Ok(questions) : NoContent();
        }

        [HttpGet("Questions")]
        public async Task<IActionResult> Get()
        {
            var questions = await _service.Get();
            return questions != null && questions.Any() ? Ok(questions) : NoContent();
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] QuestionCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await _service.Insert(model);

                if (item != null)
                {
                    return CreatedAtRoute(nameof(GetQuestion), item, item.Id);
                }

                return Conflict();
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] QuestionUpdateModel model)
        {

            if (ModelState.IsValid)
            {
                model.Id = id;
                var item = await _service.Update(model);

                return item != null ? Ok(item) : NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            if (ModelState.IsValid)
            {
                Ok(await _service.Delete(id));
            }

            return BadRequest();
        }
    }
}
