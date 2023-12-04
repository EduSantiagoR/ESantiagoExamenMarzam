using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            byte[] passwordBytes = Encriptar(UTF8Encoding.UTF8.GetBytes(password));
            ML.Usuario usuario = new ML.Usuario(username,passwordBytes);
            bool result = BL.Usuario.Login(usuario);
            if (result)
            {
                return RedirectToAction("GetAll","Pedido");
            }
            else
            {
                ViewBag.Mensaje = "Nombre de usuario o contraseña incorrectos";
                return PartialView("Modal");
            }
        }
        public static byte[] Encriptar(byte[] data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] datosEncriptados = sha256.ComputeHash(data);
                return datosEncriptados;
            }
        }
    }

}
