using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HOHSI.Data;
using HOHSI.Models;
using Microsoft.Extensions.Configuration;
using HOHSI.Models.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace HOHSI.Controllers.Auxiliary
{
    public class FilesToDeleteController : Controller
    {
        private readonly HOHSIContext _context;
        private readonly IConfiguration _configuration;
        private readonly IFilesToDeleteRepository _filesToDeleteRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FilesToDeleteController(IConfiguration configuration, HOHSIContext context, IFilesToDeleteRepository filesToDeleteRepository, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _configuration = configuration;
            _filesToDeleteRepository = filesToDeleteRepository;
            _hostEnvironment = hostEnvironment;
        }

        // GET: FilesToDelete
        public async Task<IActionResult> Index()
        {
            return View(await _filesToDeleteRepository.GetAll());
        }

        // GET: FilesToDelete/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filesToDelete = await _filesToDeleteRepository.GetById((int)id);
            if (filesToDelete == null)
            {
                return NotFound();
            }

            return View(filesToDelete);
        }


        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileToDelete = await _filesToDeleteRepository.GetById((int)id);

            if (System.IO.File.Exists(fileToDelete.filePath))
            {
                while (true)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(fileToDelete.filePath,
                        FileMode.Open, FileAccess.ReadWrite,
                        FileShare.Delete, 100, true))
                        {
                            fs.ReadByte();

                            // If we got this far the file is ready
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        System.Threading.Thread.Sleep(200);
                    }
                }
                System.IO.File.Delete(fileToDelete.filePath);
                await _filesToDeleteRepository.Delete(fileToDelete);
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FilesToDeleteExists(int id)
        {
            return await _filesToDeleteRepository.Any(e => e.fileId == id);
        }
    }
}
