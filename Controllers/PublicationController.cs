using Project_Instadev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Project_Instadev.Controllers
{
    [Route("Feed")]
    public class PublicationController : Controller
    {
        Publication pubModels = new Publication();

        public IActionResult Feed()
        {
            User user = new User();
            ViewBag.Publications = pubModels.ReadAllItens();
              
            return View();
        }

        [Route("Publicar")]
        public IActionResult Publicar(IFormCollection form)
        {
          
            Publication newPub = new Publication();
          
            newPub.IdPublication = pubModels.idGPublication();
            newPub.Subtitle = form["Subtitle"];
            newPub.Image = form["Image"];


            // Inicio uploud
            if (form.Files.Count > 0)
            {
                //Se sim,
                //Armazenamos o arquivo na variável file
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Posts");

                // Verificamos se a pasta Equipes não existe
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                //localhost:5001           +        + Equipes + equipe.jpg        
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    // Salvamos o arquivo no caminho especificado
                    file.CopyTo(stream);
                }
                newPub.Image = file.FileName;
            }


            // Uploud termino
            newPub.Subtitle = form["Subtitle"];
           


            // Chamamos o método Create para salvar
            // a novaEquipe no CSV
            pubModels.Create(newPub);
            
           
            return LocalRedirect("~/Feed");
        }

        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            pubModels.Delete(id);
            ViewBag.Publications = pubModels.ReadAllItens();
            return LocalRedirect("~/Feed");
        }
    }
}