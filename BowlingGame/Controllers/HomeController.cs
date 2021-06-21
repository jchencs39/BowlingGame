using BowlingGame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BowlingGame.Respository;

namespace BowlingGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBowlingGameService _bowlingGame;
        
        public HomeController(ILogger<HomeController> logger, IBowlingGameService bowlingGame)
        {
            _logger = logger;
            _bowlingGame = bowlingGame;         
        }
        [HttpGet]
        public IActionResult Index()
        {
            BowlingGameModel model = new BowlingGameModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(BowlingGameModel model, string submitButton)
        {    
            if (submitButton == "Play Random")
                model = _bowlingGame.PlayRadom();
            else if (submitButton == "Play Strike")
                model = _bowlingGame.PlayStrike();
            else if (submitButton == "Play Spare")
                model = _bowlingGame.PlaySpare();
            else if (submitButton == "Play Tenth")
                model = _bowlingGame.PlayTenth();
            else if (submitButton == "Play Perfect")
                model = _bowlingGame.PlayPerfect();
            return View(model);
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
