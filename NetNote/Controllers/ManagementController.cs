using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetNote.Models;
using NetNote.ViewModels;

namespace NetNote.Controllers
{
    public class ManagementController : Controller
    {
        private INoteTypeRepository _noteTypeRepository;

        public ManagementController(INoteTypeRepository noteTypeRepository)
        {
            _noteTypeRepository = noteTypeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var noteTypes = await _noteTypeRepository.GetListAsync();

            return View(noteTypes);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var type = new NoteType() { Name = name };

            await _noteTypeRepository.AddAsync(type);

            return RedirectToAction("Index");
        }
    }
}
