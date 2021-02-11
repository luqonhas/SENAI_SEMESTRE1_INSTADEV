using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_Instadev.Models;
using Microsoft.AspNetCore.Http;

namespace Project_Instadev.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        User userModel = new User();
        [TempData]

        public string Mensagem { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logando(IFormCollection form)
        {
            // Lemos todos os arquivos do CSV
            List<string> csv = userModel.ReadAllLinesCSV("Database/register.csv");

            // Verificamos se as informações passadas existe na lista de string
            var logado =
             csv.Find(
            x =>
            x.Split(";")[1] == form["Email"] &&
            x.Split(";")[4] == form["Senha"]
             );


            // Redirecionamos o usuário logado caso encontrado
            if (logado != null)
            {
                HttpContext.Session.SetString("E-mail", logado.Split(";")[1]);
                HttpContext.Session.SetString("IdUser", logado.Split(";")[0]);
                return LocalRedirect("~/Feed");
            }

            Mensagem = "Dados incorretos, tente novamente...";
            return LocalRedirect("~/");

        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_UserName");
            return LocalRedirect("~/");
        }
    }
}
