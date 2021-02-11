using System;
using Project_Instadev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project_Instadev.Controllers
{
    // localhost:5001/User
    [Route("Register")]
    public class UserController : Controller
    {
        User userModels = new User();

        // localhost:5001/User/Register
        public IActionResult Register()
        {
            ViewBag.Users = userModels.ReadAllItems();
            return View();
        }

        // localhost:5001/User/New
        [Route("New")]
        public IActionResult Registration(IFormCollection registrationForm)
        { // o IActionResult e o IFormCollection fazem parte de bibliotecas do AspNetCore
            User newUser = new User();
            newUser.Email = registrationForm["Email"];
            newUser.CompleteName = registrationForm["CompleteName"];
            newUser.UserName = registrationForm["UserName"];
            newUser.Password = registrationForm["Password"];
            newUser.Photo = "default.png";

            newUser.IdUser = userModels.IdGenerator(); // o IdUser do usuário será igual ao método IdGenerator dentro do userModels || assim, será gerado toda vez que o método de Register for executado 

            userModels.Create(newUser);
            ViewBag.Users = userModels.ReadAllItems();
            

            // localhost:5001/User/Register
            return LocalRedirect("~/");
        }

    }
}