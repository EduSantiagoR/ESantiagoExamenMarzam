using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Pedido pedido = new ML.Pedido();
            pedido.Pedidos = new List<ML.Pedido>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5095/api/");
                var responseTask = client.GetAsync("Pedido");
                responseTask.Wait();
                
                var resultService = responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<List<ML.Pedido>>();
                    readTask.Wait();
                    foreach(var pedidoResult in readTask.Result)
                    {
                        pedido.Pedidos.Add(pedidoResult);
                    }
                }
            }
            return View(pedido);
        }
        public IActionResult Detalles(int idPedido)
        {
            ML.MedicamentoPedido detalles = new ML.MedicamentoPedido();
            detalles.Pedidos = new List<ML.MedicamentoPedido>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5095/api/");
                var responseTask = client.GetAsync($"Pedido/{idPedido}");
                responseTask.Wait();

                var resultService = responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<List<ML.MedicamentoPedido>>();
                    readTask.Wait();
                    foreach(var medicamento in readTask.Result)
                    {
                        detalles.Pedidos.Add(medicamento);
                    }
                }
            }
            return View(detalles);
        }
        public IActionResult Form()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            medicamento.Medicamentos = new List<ML.Medicamento>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5095/api/");
                var resonseTask = client.GetAsync("Medicamento");
                resonseTask.Wait();

                var resultService = resonseTask.Result;
                if(resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<List<ML.Medicamento>>();
                    readTask.Wait();
                    foreach(var medicamentoResult in readTask.Result)
                    {
                        medicamento.Medicamentos.Add(medicamentoResult);
                    }
                }
            }
            return View(medicamento);
        }
        [HttpPost]
        public IActionResult Form(string nombre, List<int> cantidades, List<int> medicamentos)
        {
            if(cantidades.Count == medicamentos.Count)
            {
                bool correct = BL.Pedido.Add(nombre);
                int idPedido = BL.Pedido.GetIdPedido(nombre);
                int index = 0;
                foreach(int idMedicamento in medicamentos)
                {
                    using(var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5095/api/");

                        ML.Pedido pedido = new ML.Pedido();
                        pedido.IdMedicamento = idMedicamento;
                        pedido.Cantidad = cantidades[index];

                        var responseTask = client.PostAsJsonAsync($"Pedido/{idPedido}",pedido);
                        responseTask.Wait();

                        var resultService = responseTask.Result;
                        if (resultService.IsSuccessStatusCode)
                        {

                        }
                    }
                    index++;
                }
                bool actualizacionTotal = BL.Pedido.UpdateTotal(idPedido);
                if (actualizacionTotal)
                {
                    ViewBag.Mensaje = "Pedido genedado correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = "Error al generar el pedido.";
                }
            }
            else
            {
                ViewBag.Mensaje = "Por favor, seleccione la misma cantidad de mendicamentos con su cantidad.";
            }
            return PartialView("Modal");
        }
        public IActionResult Delete(int idPedido)
        {
            bool correct = false;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5095/api/");
                var responseTask = client.DeleteAsync($"Pedido/{idPedido}");
                responseTask.Wait();

                var resultService = responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<bool>();
                    readTask.Wait();
                    correct = readTask.Result;
                }
            }
            if (correct)
            {
                ViewBag.Mensaje = "Eliminado correctamente.";
            }
            else
            {
                ViewBag.Mensaje = "Error al eliminar el pedido.";
            }
            return PartialView("Modal");
        }
    }
}
