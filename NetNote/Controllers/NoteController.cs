
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetNote.Models;
using NetNote.ViewModels;

namespace NetNote.Controllers
{

    public class NoteController : Controller
    {
        private INoteRepository _noteRepository;

        private INoteTypeRepository _noteTypeRepository;

        public NoteController(INoteRepository noteRepository, INoteTypeRepository noteTypeRepository)
        {
            _noteRepository = noteRepository;
            _noteTypeRepository = noteTypeRepository;
        }

        public async Task<IActionResult> Index(int pageindex = 1)
        {
            var pageSize = 10;
            var page = await _noteRepository.PageListAsync(pageindex, pageSize);

            return View(page);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NoteModel noteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = noteModel.CreateEntity();

            await _noteRepository.AddAsync(note);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Add()
        {
            var types = await _noteTypeRepository.GetListAsync();
            ViewBag.Types = types.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Id.ToString()
            });
            return View();
        }

    }
}
