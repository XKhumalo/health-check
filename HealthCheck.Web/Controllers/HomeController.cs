using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HealthCheck.Web.Models;
using HealthCheck.API.Controllers;
using HealthCheck.Model;
using HealthCheck.API.Services;

namespace HealthCheck.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AnswerService answerService;

        public IActionResult Index()
        {
            var answer = new Answer()
            {
                CategoryChosen = Model.Enums.AnswerOption.Amber,
                SessionId = "1"
            };

            answerService.Create(answer);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
