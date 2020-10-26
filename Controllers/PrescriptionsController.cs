using HOHSI.Data;
using HOHSI.Models;
using HOHSI.Models.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly HOHSIContext _context;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PrescriptionsController(HOHSIContext context, IPrescriptionRepository prescriptionRepository, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _prescriptionRepository = prescriptionRepository;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Prescriptions
        public async Task<IActionResult> Index()
        {
            return View(await _prescriptionRepository.GetAll());
        }

        // GET: Prescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prescription = await _prescriptionRepository.GetById((int)id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // GET: Prescriptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrescriptionId,Date,PatientId,PrescriptorId")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                prescription.Date = DateTime.UtcNow;
                await _prescriptionRepository.Create(prescription);
                return RedirectToAction(nameof(Index));
            }
            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _prescriptionRepository.GetById((int)id);
            if (prescription == null)
            {
                return NotFound();
            }
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrescriptionId,Date,PatientId,PrescriptorId")] Prescription prescription)
        {
            if (id != prescription.PrescriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _prescriptionRepository.Update(prescription);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!PrescriptionExists(prescription.PrescriptionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ViewBag.ErrorTitle = $"'{prescription.PrescriptionId}' is inaccessible at the moment.";
                        ViewBag.ErrorMessage = "This is a database concurrency error";
                        ViewBag.ErrorDetails = e.Message;
                        return View("Error");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions
                .FirstOrDefaultAsync(m => m.PrescriptionId == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescription = await _prescriptionRepository.GetById(id);
            await _prescriptionRepository.Delete(prescription);
            return RedirectToAction(nameof(Index));
        }

        private bool PrescriptionExists(int id)
        {
            return _context.Prescriptions.Any(e => e.PrescriptionId == id);
        }
    }
}