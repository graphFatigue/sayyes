using sayyes.Domain.Entity;
using sayyes.Domain.Response;
using sayyes.Domain.ViewModels.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Service.Interfaces
{
    public interface IAuthorService
    {
        IBaseResponse<List<Author>> GetAuthors();

        Task<IBaseResponse<AuthorViewModel>> GetAuthor(int id);

        Task<IBaseResponse<Author>> Create(AuthorViewModel model);

        Task<IBaseResponse<bool>> DeleteAuthor(int id);

        IBaseResponse<List<Author>> Search(AuthorViewModel model);

        Task<IBaseResponse<Author>> Edit(int id, AuthorViewModel model);

    }
}
