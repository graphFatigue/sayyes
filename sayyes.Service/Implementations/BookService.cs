
using sayyes.DAL.Interfaces;
using sayyes.DAL.Repositories;
using sayyes.Domain.Entity;
using sayyes.Domain.Enum;
using sayyes.Domain.Response;
using sayyes.Domain.ViewModels.Book;
using sayyes.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Service.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly IBaseRepository<BookAuthor> _bookAuthorRepository;
        private readonly IBaseRepository<BookPublisher> _bookPublisherRepository;
        private readonly IBaseRepository<BookWriting> _bookWritingRepository;
        //private readonly IBaseRepository<Book> _artistRepository;

        public BookService(IBaseRepository<Book> bookRepository, IBaseRepository<BookAuthor> bookAuthorRepository, IBaseRepository<BookPublisher> bookPublisherRepository,
           IBaseRepository<BookWriting> bookWritingRepository )//, IBaseRepository<Artist> artistRepository)
        {
            _bookRepository = bookRepository;
            _bookAuthorRepository = bookAuthorRepository;
            _bookPublisherRepository = bookPublisherRepository;
            _bookWritingRepository = bookWritingRepository;
            
            //_artistRepository = artistRepository;
        }

        public IBaseResponse<List<Book>> GetBooks(int authorId)
        {
            try
            {
                var relations = _bookAuthorRepository.GetAll().ToList().Where(x => x.AuthorId == authorId);
                var selectedBooks = from p in relations
                                    where p.AuthorId == authorId
                                    //where p.Title.ToLower().Contains(model.Title.ToLower()) && p.Genre.Contains(model.Genre) && (p.YearRelease == model.YearRelease || model.YearRelease == 0)
                                    select p.BookId;

                List<Book> books = new List<Book>(3);
                var books1 = _bookRepository.GetAll().ToList();
                foreach (var book in selectedBooks)
                {
                    var b1 = books1.Where(x => x.Id == book);
                    var b2 = b1.ToList().First();
                    books.Add(b2);
                    //books.Concat(b1);//.ToList();
                    books.ToList();

                }

                //.Where(x => x.Id in books1);

                //foreach (var book in selectedBooks)
                //{
                //    books1.Add(book);

                //}
                // var books = _bookRepository.GetAll().ToList().Where(x => x.Id in authorId)//AllAsync();//;
                //books = books.Where(x => x.AuthorId == authorId).ToList();
                //.Where(x => x.AuthorId == authorId);//AllAsync();//;
                //books = books.Where(x => x.AuthorId == authorId).ToList();
                if (!books.Any())
                {
                    return new BaseResponse<List<Book>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }

                books = books.OrderByDescending(o => o.YearOfPublication).ToList();
                return new BaseResponse<List<Book>>()
                {
                    Data = (List<Book>)books,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {

                return new BaseResponse<List<Book>>()
                {
                    Description = $"[GetBooks] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<BookViewModel>> GetBook(int id)
        {
            try
            {
                var book = await _bookRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (book == null)
                {
                    return new BaseResponse<BookViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new BookViewModel()
                {
                    Id = id,
                    //AuthorId = book.AuthorId,
                    Title = book.Title,
                    YearOfPublication = book.YearOfPublication,
                    Translator = book.Translator,
                    OriginalLanguage = book.OriginalLanguage,
                    Language = book.Language,
                    OriginalTitle = book.OriginalTitle,
                    BriefDescription = book.BriefDescription,
                    Pages = book.Pages,
                    Genre = book.Genre,
                    Avatar = book.Avatar,
                    //avatar??
                };

                return new BaseResponse<BookViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<BookViewModel>()
                {
                    Description = $"[GetBook] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Book>> Create(BookViewModel model)//, byte[] imageData)
        {
            try
            {
                var book = new Book()
                {
                    Title = model.Title,
                    YearOfPublication = model.YearOfPublication,
                    Translator = model.Translator,
                    OriginalLanguage = model.OriginalLanguage,
                    Language = model.Language,
                    OriginalTitle = model.OriginalTitle,
                    BriefDescription = model.BriefDescription,
                    Pages = model.Pages,
                    Genre = model.Genre,
                    Avatar = model.Avatar,
                };
                await _bookRepository.Create(book);

                return new BaseResponse<Book>()
                {
                    StatusCode = StatusCode.OK,
                    Data = book
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Book>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<bool>> DeleteBook(int id)
        {
            try
            {
                var book = await _bookRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (book == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _bookRepository.Delete(book);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteAlbum] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<Book>> Edit(int id, BookViewModel model)
        {
            try
            {
                var book = await _bookRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (book == null)
                {
                    return new BaseResponse<Book>()
                    {
                        Description = "Album not found",
                        StatusCode = StatusCode.ArtistNotFound
                    };
                }

                book.Title = model.Title;
                book.YearOfPublication = model.YearOfPublication;
                book.Translator = model.Translator;
                book.OriginalLanguage = model.OriginalLanguage;
                book.Language = model.Language;
                book.OriginalTitle = model.OriginalTitle;
                book.BriefDescription = model.BriefDescription;
                book.Pages = model.Pages;
                book.Genre = model.Genre;
                book.Avatar = model.Avatar;

                await _bookRepository.Update(book);


                return new BaseResponse<Book>()
                {
                    Data = book,
                    StatusCode = StatusCode.OK,
                };
                // TypeCar
            }
            catch (Exception ex)
            {
                return new BaseResponse<Book>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public IBaseResponse<List<Book>> Search(BookViewModel model)
        {
            try
            {
                model.Genre = model?.Genre is null ? "" : model.Genre;
                model.Title = model?.Title is null ? "" : model.Title;
                var albums = _bookRepository.GetAll().ToList();

                string genre = model?.Genre?.ToString();
                var selectedBooks = from p in albums
                                     //where p.Title.ToLower().Contains(model.Title.ToLower()) && p.Genre.Contains(model.Genre) && (p.YearRelease == model.YearRelease || model.YearRelease == 0)
                                     select p;

                List<Book> books1 = new List<Book>();

                foreach (var book in selectedBooks)
                {
                    books1.Add(book);

                }


                if (!books1.Any())
                {
                    return new BaseResponse<List<Book>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Book>>()
                {
                    Data = books1,
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse<List<Book>>()
                {
                    Description = $"[GetArtists] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



    }
}
