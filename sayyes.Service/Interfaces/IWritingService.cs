using sayyes.Domain.Entity;
using sayyes.Domain.Response;
using sayyes.Domain.ViewModels.Publisher;
using sayyes.Domain.ViewModels.Writing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Service.Interfaces
{
    public interface IWritingService
    {
        IBaseResponse<List<Writing>> GetWritings(int aId);

        Task<IBaseResponse<WritingViewModel>> GetWriting(int id);

        Task<IBaseResponse<Writing>> Create(WritingViewModel model);

        Task<IBaseResponse<bool>> DeleteWriting(int id);

       // IBaseResponse<List<Writing>> Search(WritingViewModel model);

        Task<IBaseResponse<Writing>> Edit(int id, WritingViewModel model);

    }
}
