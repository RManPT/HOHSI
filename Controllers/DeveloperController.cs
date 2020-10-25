using HOHSI.Data;
using HOHSI.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HOHSI.Controllers
{
    public class DeveloperController: Controller
    {
        private readonly HOHSIContext _context;

        public DeveloperController(HOHSIContext context)
        {
            _context = context;
        }

        public ViewResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Empty()
        {
            _context.EmptyDB();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Reseed()
        {
            _context.EnsureDBSeeded();
            return RedirectToAction("Index", "Home");
        }

    }
}
