using sayyes.Domain.ViewModels.Author;
using sayyes.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace sayyes.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var response = _authorService.GetAuthors();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }


        [HttpGet]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var response = await _authorService.GetAuthor(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _authorService.DeleteAuthor(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetAuthors");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var response = await _authorService.GetAuthor(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]

        public async Task<IActionResult> Save(AuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _authorService.Create(model);
                }
                else
                {
                    await _authorService.Edit(model.Id, model);
                }
                return RedirectToAction("GetAuthors");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(AuthorViewModel model)
        {
            var response = _authorService.Search(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("SearchResult", response.Data);
            }
            return View("Error", $"{response.Description}");
        }


    }
}
