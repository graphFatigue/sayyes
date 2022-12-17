using sayyes.Domain.Entity;
using sayyes.Domain.ViewModels.Writing;
using sayyes.Service.Implementations;
using sayyes.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sayyes.Controllers
{
    public class WritingController : Controller
    {
        private readonly IWritingService _writingService;

        public WritingController(IWritingService writingService)
        {
            _writingService = writingService;
        }

        [HttpGet]
        public IActionResult GetWritings(int albumId, int authorId)
        {
            var response = _writingService.GetWritings(albumId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Description == "Found 0 elements")
            {
                Writing writing = new Writing();// { AuthorId = authorId, AlbumId = albumId };!!!!!!!!!!!!!!!
                List<Writing> songs = new List<Writing> { writing };
                return View(songs);
            }
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }


        [HttpGet]
        public async Task<IActionResult> GetWriting(int id)
        {
            var response = await _writingService.GetWriting(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, int alId)
        {
            var response = await _writingService.DeleteWriting(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetSongs", "Song", new { albumId = alId });
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(int id, int albumId, int authorId)
        {
            if (id == 0)
            {
                return View();
            }

            var response = await _writingService.GetWriting(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]

        public async Task<IActionResult> Save(WritingViewModel model, int albumId, int authorId)
        {
            //model.AuthorId = authorId;
            //model.AlbumId = albumId;
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {

                    await _writingService.Create(model);
                }
                else
                {
                    await _writingService.Edit(model.Id, model);
                }
                //return RedirectToAction("GetSongs", "Song", new { albumId = model.AlbumId });!!!!!!!!
            }
            return View();
        }


        //public async Task<IActionResult> AddToFavorite(int id, int alId)
        //{
        //    var response = await _writingService.AddToFavorite(id);
        //    //string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //        return RedirectToAction("GetSongs", "Song", new { albumId = alId });
        //    }
        //    return RedirectToAction("Error");
        //}

        //public async Task<IActionResult> DeleteFromFavorite(int id, int alId)
        //{
        //    var response = await _writingService.DeleteFromFavorite(id);
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //        return RedirectToAction("GetSongs", "Song", new { albumId = alId });
        //    }
        //    return RedirectToAction("Error");
        //}

        //[HttpGet]
        //public IActionResult FavoriteSongs()
        //{
        //    var response = _writingService.GetFavoriteSongs();
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK && response.Description == "Found 0 elements")
        //    {
        //        SongViewModel song = new SongViewModel() { };
        //        List<SongViewModel> songs = new List<SongViewModel> { };
        //        return View(songs);
        //    }
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //        return View(response.Data);
        //    }
        //    return View("Error", $"{response.Description}");
        //}

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Search(WritingViewModel model)
        //{
        //    var response = _writingService.Search(model);
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //        return View("SearchResult", response.Data);
        //    }
        //    return View("Error", $"{response.Description}");
        //}

        //[HttpGet]
        //public async Task<IActionResult> AlbumsIncludeSong()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AlbumsIncludeSong(WritingViewModel model)
        //{
        //    var response = _writingService.AlbumsIncludeSong(model);
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //        return View("GetAlbums", response.Data);
        //    }
        //    return View("Error", $"{response.Description}");
        //}
    }
}
