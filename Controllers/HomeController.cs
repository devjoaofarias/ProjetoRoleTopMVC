using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RoleTopMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace RoleTopMVC.Controllers
{
    public class HomeController : AbstractController
    {
        public IActionResult Index()
        {   return View(new BaseViewModel()
            {
                NomeView = "Home",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
                
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

         
    }
}
