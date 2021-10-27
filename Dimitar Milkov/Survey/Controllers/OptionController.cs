using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Survey.Models.Models.Option;
using Survey.Services.Abstractions;

namespace Survey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IOptionService _service;

        public OptionController(IOptionService service)
        {
            _service = service;
        }

        [HttpGet("Options/{id:int}", Name = nameof(GetOption))]
        public async Task<IActionResult> GetOption([FromRoute] int id)
        {
            var option = await _service.GetById(id);
            return option != null ? Ok(option) : NoContent();
        }

        [HttpGet("Options")]
        public async Task<IActionResult> Get()
        {
            var options = await _service.Get();
            return options != null && options.Any() ? Ok(options) : NoContent();
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] OptionCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await _service.Insert(model);

                if (item != null)
                {
                    return CreatedAtRoute(nameof(GetOption), item, item.Id);
                }

                return Conflict();
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] OptionUpdateModel model)
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
