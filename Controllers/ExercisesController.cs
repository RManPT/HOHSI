using HOHSI.Data;
using HOHSI.Models;
using HOHSI.Models.Interfaces;
using HOHSI.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly HOHSIContext _context;
        private readonly IConfiguration _configuration;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IFilesToDeleteRepository _filesToDeleteRepository;

        public ExercisesController(
            IConfiguration configuration,
            HOHSIContext context,
            IExerciseRepository exerciseRepository,
            IWebHostEnvironment hostEnvironment,
            IFilesToDeleteRepository filesToDeleteRepository)
        {
            _context = context;
            _configuration = configuration;
            _exerciseRepository = exerciseRepository;
            _hostEnvironment = hostEnvironment;
            _filesToDeleteRepository = filesToDeleteRepository;
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
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, _configuration.GetValue<string>("IMG_PATH_EXERCISES"));
                    if (vm.Image == null) uniqueFileName = _configuration.GetValue<string>("IMG_DEFAULT_EXERCISES");
                    else
                    {
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "." + vm.Image.FileName.Split(".").Last();
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        await vm.Image.CopyToAsync(new FileStream(filePath, FileMode.Create));
                        uniqueFileName = _configuration.GetValue<string>("IMG_PATH_EXERCISES") + uniqueFileName;
                    }
                    Exercise ex = new Exercise
                    {
                        Name = vm.Name,
                        Description = vm.Description,
                        ImageName = uniqueFileName
                    };
                    try
                    {
                        await _exerciseRepository.Create(ex);
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        ViewBag.ErrorTitle = $"There was an error creating this record.";
                        ViewBag.ErrorMessage = "Reload the page and try again";
                        ViewBag.ErrorDetails = e.Message;
                        return View("Error");
                    }
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
                    if (!await ExerciseExists(exercise.ExerciseId))
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
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>delete");
            await _exerciseRepository.GetById((int)id);
            throw new Exception("");
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _exerciseRepository.GetById((int)id);
            try
            {
                await _exerciseRepository.Delete(exercise);
            }
            catch (Exception e)
            {
                ViewBag.ErrorTitle = $"'Exercise {id}' is inaccessible at the moment.";
                ViewBag.ErrorMessage = "This is a database concurrency error";
                ViewBag.ErrorDetails = e.Message;
                return View("Error");
            }
            if (exercise.ImageName != _configuration.GetValue<string>("IMG_DEFAULT_EXERCISES"))
            {
                string filePath = Path.Combine(_hostEnvironment.WebRootPath, exercise.ImageName);
                await _filesToDeleteRepository.Create(new FilesToDelete { filePath = filePath });
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ExerciseExists(int id)
        {
            return await _exerciseRepository.Any(e => e.ExerciseId == id);
        }
    }
}