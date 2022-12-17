using Microsoft.EntityFrameworkCore;
using sayyes.DAL.Interfaces;
using sayyes.Domain.Entity;
using sayyes.Domain.Enum;
using sayyes.Domain.Response;
using sayyes.Domain.ViewModels.Publisher;
using sayyes.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Service.Implementations
{
    public class PublisherService : IPublisherService
    {
        private readonly IBaseRepository<Publisher> _publisherRepository;
        private readonly IBaseRepository<BookPublisher> _bookPublisherRepository;


        public PublisherService(IBaseRepository<Publisher> publisherRepository, IBaseRepository<BookPublisher> bookPublisherRepository)
        {
            _publisherRepository = publisherRepository;
            _bookPublisherRepository = bookPublisherRepository;
        }

        public IBaseResponse<List<Publisher>> GetPublishers()
        {
            try
            {
                var publishers = _publisherRepository.GetAll().ToList();
                if (!publishers.Any())
                {
                    return new BaseResponse<List<Publisher>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Publisher>>()
                {
                    Data = publishers,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {

                return new BaseResponse<List<Publisher>>()
                {
                    Description = $"[GetArtists] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }





        public async Task<IBaseResponse<PublisherViewModel>> GetPublisher(int id)
        {
            try
            {
                var publisher = await _publisherRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (publisher == null)
                {
                    return new BaseResponse<PublisherViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new PublisherViewModel()
                {
                    Id = id,
                    NameOfThePublishingHouse = publisher.NameOfThePublishingHouse,
                    FullNameOfTheDirector = publisher.FullNameOfTheDirector,   
                    YearOfFoundation = publisher.YearOfFoundation,
                    Country = publisher.Country,
                };

                return new BaseResponse<PublisherViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PublisherViewModel>()
                {
                    Description = $"[GetArtist] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Publisher>> Create(PublisherViewModel model)
        {
            try
            {
                var publisher = new Publisher()
                {
                    NameOfThePublishingHouse = model.NameOfThePublishingHouse,
                    FullNameOfTheDirector = model.FullNameOfTheDirector,
                    YearOfFoundation = model.YearOfFoundation,
                    Country = model.Country,
                };
                await _publisherRepository.Create(publisher);

                return new BaseResponse<Publisher>()
                {
                    StatusCode = StatusCode.OK,
                    Data = publisher
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Publisher>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<bool>> DeletePublisher(int id)
        {
            try
            {
                var artist = await _publisherRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (artist == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _publisherRepository.Delete(artist);

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
                    Description = $"[DeleteArtist] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<Publisher>> Edit(int id, PublisherViewModel model)
        {
            try
            {
                var publisher = await _publisherRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (publisher == null)
                {
                    return new BaseResponse<Publisher>()
                    {
                        Description = "Artist not found",
                        StatusCode = StatusCode.ArtistNotFound
                    };
                }
                publisher.YearOfFoundation = model.YearOfFoundation;
                publisher.NameOfThePublishingHouse = model.NameOfThePublishingHouse;
                publisher.Country = model.Country;
                publisher.FullNameOfTheDirector = model.FullNameOfTheDirector;
                await _publisherRepository.Update(publisher);
                return new BaseResponse<Publisher>()
                {
                    Data = publisher,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Publisher>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public IBaseResponse<List<Publisher>> Search(PublisherViewModel model)
        {
            try
            {
                model.Country = model?.Country is null ? "" : model.Country;
               // model.Name = model?.Name is null ? "" : model.Name;
                var artists = _publisherRepository.GetAll().ToList();
                var selectedPublishers = from p in artists
                                      where //p.Name.ToLower().Contains(model.Name.ToLower())
                                      //&& 
                                      p.Country.ToLower().Contains(model.Country.ToLower())
                                     // && p.Group.CompareTo(model.Group) == 0
                                      select p;
                List<Publisher> publishers1 = new List<Publisher>();
                foreach (var artist in selectedPublishers)
                {
                    publishers1.Add(artist);
                }
                if (!publishers1.Any())
                {
                    return new BaseResponse<List<Publisher>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Publisher>>()
                {
                    Data = publishers1,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Publisher>>()
                {
                    Description = $"[GetArtists] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
