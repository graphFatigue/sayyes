using sayyes.DAL.Interfaces;
using sayyes.Domain.Entity;
using sayyes.Domain.Enum;
using sayyes.Domain.Response;
using sayyes.Domain.ViewModels.Author;
using sayyes.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Service.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IBaseRepository<Author> _authorRepository;
        private readonly IBaseRepository<BookAuthor> _bookAuthorRepository;
        //private readonly IBaseRepository<WritingAuthor> _writingAuthorRepository;


        public AuthorService(IBaseRepository<Author> authorRepository, IBaseRepository<BookAuthor> bookAuthorRepository)//, IBaseRepository<WritingAuthor> writingAuthorRepository)
        {
            _authorRepository = authorRepository;
            _bookAuthorRepository = bookAuthorRepository;
           // _writingAuthorRepository = writingAuthorRepository;
        }

        public IBaseResponse<List<Author>> GetAuthors()
        {
            try
            {
                var authors = _authorRepository.GetAll().ToList();
                if (!authors.Any())
                {
                    return new BaseResponse<List<Author>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Author>>()
                {
                    Data = authors,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {

                return new BaseResponse<List<Author>>()
                {
                    Description = $"[GetArtists] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }





        public async Task<IBaseResponse<AuthorViewModel>> GetAuthor(int id)
        {
            try
            {
                var author = await _authorRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (author == null)
                {
                    return new BaseResponse<AuthorViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new AuthorViewModel()
                {
                    Id = id,
                    Name = author.Name,
                    Surname = author.Surname,
                    Patronymic = author.Patronymic,
                    Sex = author.Sex,
                    Biography = author.Biography,
                    DateOfBirth = author.DateOfBirth,
                    DateOfDeath = author.DateOfDeath,
                    Nationality = author.Nationality,
                    Avatar = author.Avatar,
                };

                return new BaseResponse<AuthorViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AuthorViewModel>()
                {
                    Description = $"[GetArtist] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Author>> Create(AuthorViewModel model)
        {
            try
            {
                var author = new Author()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    Sex = model.Sex,
                    Biography = model.Biography,
                    DateOfBirth = model.DateOfBirth,
                    DateOfDeath = model.DateOfDeath,
                    Nationality = model.Nationality,
                    Avatar=model.Avatar,
                };
                await _authorRepository.Create(author);

                return new BaseResponse<Author>()
                {
                    StatusCode = StatusCode.OK,
                    Data = author
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Author>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<bool>> DeleteAuthor(int id)
        {
            try
            {
                var author = await _authorRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (author == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _authorRepository.Delete(author);

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


        public async Task<IBaseResponse<Author>> Edit(int id, AuthorViewModel model)
        {
            try
            {
                var author = await _authorRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (author == null)
                {
                    return new BaseResponse<Author>()
                    {
                        Description = "Artist not found",
                        StatusCode = StatusCode.ArtistNotFound
                    };
                }
                author.Name = model.Name;
                author.Surname = model.Surname;
                author.Patronymic = model.Patronymic;
                author.Sex = model.Sex;
                author.Biography = model.Biography;
                author.DateOfBirth = model.DateOfBirth;
                author.DateOfDeath = model.DateOfDeath;
                author.Nationality = model.Nationality;
                author.Avatar = model.Avatar;
                await _authorRepository.Update(author);
                return new BaseResponse<Author>()
                {
                    Data = author,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Author>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public IBaseResponse<List<Author>> Search(AuthorViewModel model)
        {
            try
            {
               // model.Country = model?.Country is null ? "" : model.Country;
                model.Name = model?.Name is null ? "" : model.Name;
                var authors = _authorRepository.GetAll().ToList();
                var selectedArtists = from p in authors
                                      where p.Name.ToLower().Contains(model.Name.ToLower())
                                     // && p.Country.ToLower().Contains(model.Country.ToLower())
                                      //&& p.Group.CompareTo(model.Group) == 0
                                      select p;
                List<Author> authors1 = new List<Author>();
                foreach (var artist in selectedArtists)
                {
                    authors1.Add(artist);
                }
                if (!authors1.Any())
                {
                    return new BaseResponse<List<Author>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<Author>>()
                {
                    Data = authors1,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Author>>()
                {
                    Description = $"[GetArtists] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
