using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            List<ML.Pedido> pedidos = BL.Pedido.Get();
            if(pedidos != null)
            {
                return Ok(pedidos);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{idPedido}")]
        public IActionResult GetDetalles(int idPedido)
        {
            ML.MedicamentoPedido detalles = new ML.MedicamentoPedido();
            detalles.Pedidos = BL.Pedido.GetDetalles(idPedido);
            if(detalles.Pedidos != null)
            {
                return Ok(detalles.Pedidos);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("{idPedido}")]
        public IActionResult Add(int idPedido, [FromBody]ML.Pedido pedido)
        {
            bool result = BL.MedicamentoPedido.Add(idPedido, pedido.IdMedicamento.Value, pedido.Cantidad.Value);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("{idPedido}")]
        public IActionResult Delete(int idPedido)
        {
            bool result = BL.Pedido.Delete(idPedido);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
