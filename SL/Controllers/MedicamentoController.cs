using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            medicamento.Medicamentos = BL.Medicamento.GetAll();
            if(medicamento.Medicamentos != null)
            {
                return Ok(medicamento.Medicamentos);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
