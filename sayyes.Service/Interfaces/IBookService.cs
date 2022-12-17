using sayyes.Domain.Entity;
using sayyes.Domain.Response;
using sayyes.Domain.ViewModels.Author;
using sayyes.Domain.ViewModels.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Service.Interfaces
{
    public interface IBookService
    {
        IBaseResponse<List<Book>> GetBooks(int authorId);

        Task<IBaseResponse<BookViewModel>> GetBook(int id);

        Task<IBaseResponse<Book>> Create(BookViewModel model);

        Task<IBaseResponse<bool>> DeleteBook(int id);

        IBaseResponse<List<Book>> Search(BookViewModel model);

        Task<IBaseResponse<Book>> Edit(int id, BookViewModel model);

    }
}
