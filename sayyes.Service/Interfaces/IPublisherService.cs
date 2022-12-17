using sayyes.Domain.Entity;
using sayyes.Domain.Response;
using sayyes.Domain.ViewModels.Book;
using sayyes.Domain.ViewModels.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Service.Interfaces
{
    public interface IPublisherService
    {
        IBaseResponse<List<Publisher>> GetPublishers();

        Task<IBaseResponse<PublisherViewModel>> GetPublisher(int id);

        Task<IBaseResponse<Publisher>> Create(PublisherViewModel model);

        Task<IBaseResponse<bool>> DeletePublisher(int id);

        IBaseResponse<List<Publisher>> Search(PublisherViewModel model);

        Task<IBaseResponse<Publisher>> Edit(int id, PublisherViewModel model);

    }
}
