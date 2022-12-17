using sayyes.Domain.Entity;
using sayyes.Domain.ViewModels.Book;
using sayyes.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sayyes.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetBooks(int authorId)
        {
            var response = _bookService.GetBooks(authorId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Description == "Found 0 elements")
            {
                Book book = new Book(); //{ AuthorId = authorId };!!!!!!!!!!!!!!!
                List<Book> books = new List<Book> { book };
                return View(books);
            }
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return View("Error", $"{response.Description}");
        }


        [HttpGet]
        public async Task<IActionResult> GetBook(int id)
        {
            var response = await _bookService.GetBook(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, int aId)
        {
            var response = await _bookService.DeleteBook(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetAlbums", "Album", new { authorId = aId });
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(int id, int authorId)
        {
            if (id == 0)
            {
                return View();
            }

            var response = await _bookService.GetBook(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]

        public async Task<IActionResult> Save(BookViewModel model, int authorId)
        {
            //ModelState.Remove("DateCreate");
            //model.AuthorId = authorId;
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {

                    await _bookService.Create(model);//, imageData);
                }
                else
                {
                    await _bookService.Edit(model.Id, model);
                }
                //return RedirectToAction("GetAlbums", "Album", new { authorId = model.AuthorId });
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(BookViewModel model)
        {
            var response = _bookService.Search(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("SearchResult", response.Data);
            }
            return View("Error", $"{response.Description}");
        }

    }
}
