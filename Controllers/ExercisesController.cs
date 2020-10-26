using HOHSI.Data;
using HOHSI.Models;
using HOHSI.Models.Interfaces;
using HOHSI.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly HOHSIContext _context;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ExercisesController(HOHSIContext context, IExerciseRepository exerciseRepository, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _exerciseRepository = exerciseRepository;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Exercises.ToListAsync());
            return View(await _exerciseRepository.GetAll());
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _exerciseRepository.GetById((int)id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                //Save image to wwwroot/image
                if (vm != null)
                {
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "img/uploads/");
                    if (vm.Image == null) uniqueFileName = "/img/no-image.jpg";
                    else
                    {
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + vm.Image.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        await vm.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
                        uniqueFileName = "img/uploads/" + uniqueFileName;
                    }
                    Exercise ex = new Exercise
                    {
                        Name = vm.Name,
                        Description = vm.Description,
                        ImageName = uniqueFileName
                    };
                    await _exerciseRepository.Create(ex);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _exerciseRepository.GetById((int)id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseId,Name,Description")] Exercise exercise)
        {
            if (id != exercise.ExerciseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _exerciseRepository.Update(exercise);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!ExerciseExists(exercise.ExerciseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ViewBag.ErrorTitle = $"'{exercise.Name}' is inaccessible at the moment.";
                        ViewBag.ErrorMessage = "This is a database concurrency error";
                        ViewBag.ErrorDetails = e.Message;
                        return View("Error");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            /*  if (id == null)
              {
                  return NotFound();
              }

              var exercise = await _context.Exercises
                  .FirstOrDefaultAsync(m => m.ExerciseId == id);
              if (exercise == null)
              {
                  return NotFound();
              }

              return View(exercise);*/
            var exercise = await _context.Exercises.FindAsync(id);
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _exerciseRepository.GetById((int)id);
            await _exerciseRepository.Delete(exercise);
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }
    }
}