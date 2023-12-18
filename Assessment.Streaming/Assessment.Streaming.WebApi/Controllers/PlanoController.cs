using Assessment.Streaming.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Streaming.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private PlanoService service { get; set; }

        public PlanoController()
        {
            this.service = new PlanoService();
        }

        [HttpGet("{id}")]
        public IActionResult GetPlano(Guid id)
        {
            var result = this.service.ObterPlano(id);

            if (result == null)
                return NotFound();

            return Ok(result);

        }
    }
}
