using sayyes.DAL.Interfaces;
using sayyes.Domain.Entity;
using sayyes.Domain.Enum;
using sayyes.Domain.Response;
using sayyes.Domain.ViewModels.Writing;
using sayyes.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sayyes.Service.Implementations
{
    public class WritingService : IWritingService
    {
        private readonly IBaseRepository<Writing> _writingRepository;
        private readonly IBaseRepository<WritingAuthor> _writingAuthorRepository;
        IBaseRepository<BookWriting> _writingBookRepository;
        //private readonly IBaseRepository<Artist> _artistRepository;
        //private readonly IBaseRepository<Album> _albumRepository;

        public WritingService(IBaseRepository<Writing> writingRepository, IBaseRepository<WritingAuthor> writingAuthorRepository, IBaseRepository<BookWriting> writingBookRepository)//, IBaseRepository<Artist> artistRepository, IBaseRepository<Album> albumRepository)
        {
            _writingRepository = writingRepository;
            _writingAuthorRepository = writingAuthorRepository;
            _writingBookRepository = writingBookRepository;
            //_artistRepository = artistRepository;
            //_albumRepository = albumRepository;
        }

        public IBaseResponse<List<Writing>> GetWritings(int albumId)
        {
            try
            {
                var songs = _writingRepository.GetAll().ToList();//.Where(x => x.AuthorId == authorId);//AllAsync();//;
                //songs = songs.Where(x => x.AlbumId == albumId).ToList();

                if (!songs.Any())
                {
                    return new BaseResponse<List<Writing>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }


                return new BaseResponse<List<Writing>>()
                {
                    Data = (List<Writing>)songs,
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {

                return new BaseResponse<List<Writing>>()
                {
                    Description = $"[GetSongs] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<WritingViewModel>> GetWriting(int id)
        {
            try
            {
                var writing = await _writingRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (writing == null)
                {
                    return new BaseResponse<WritingViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new WritingViewModel()
                {
                    Id = id,
                    Title = writing.Title,
                    YearOfPublication = writing.YearOfPublication,
                    OriginalLanguage = writing.OriginalLanguage,
                    OriginalTitle = writing.OriginalTitle,
                    BriefDescription = writing.BriefDescription,
                    Pages = writing.Pages,
                    Genre = writing.Genre,

                };

                return new BaseResponse<WritingViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<WritingViewModel>()
                {
                    Description = $"[GetSong] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Writing>> Create(WritingViewModel model)//, byte[] imageData)
        {
            try
            {
                var song = new Writing()
                {
                    Title = model.Title,
                    YearOfPublication = model.YearOfPublication,
                    OriginalLanguage = model.OriginalLanguage,
                    OriginalTitle = model.OriginalTitle,
                    BriefDescription = model.BriefDescription,
                    Pages = model.Pages,
                    Genre = model.Genre,
                };
                await _writingRepository.Create(song);

                return new BaseResponse<Writing>()
                {
                    StatusCode = StatusCode.OK,
                    Data = song
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Writing>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<bool>> DeleteWriting(int id)
        {
            try
            {
                var song = await _writingRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (song == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _writingRepository.Delete(song);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteSong] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<IBaseResponse<Writing>> Edit(int id, WritingViewModel model)
        {
            try
            {
                var writing = await _writingRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (writing == null)
                {
                    return new BaseResponse<Writing>()
                    {
                        Description = "Song not found",
                        StatusCode = StatusCode.ArtistNotFound
                    };
                }

                writing.Title = model.Title;
                writing.YearOfPublication = model.YearOfPublication;
                writing.OriginalLanguage = model.OriginalLanguage;
                writing.OriginalTitle = model.OriginalTitle;
                writing.BriefDescription = model.BriefDescription;
                writing.Pages = model.Pages;
                writing.Genre = model.Genre;


                await _writingRepository.Update(writing);


                return new BaseResponse<Writing>()
                {
                    Data = writing,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Writing>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        //public async Task<IBaseResponse<Writing>> AddToFavorite(int id)
        //{
        //    try
        //    {
        //        var song = await _writingRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        //        if (song == null)
        //        {
        //            return new BaseResponse<Song>()
        //            {
        //                Description = "Song not found",
        //                StatusCode = StatusCode.ArtistNotFound
        //            };
        //        }

        //        song.IsFavorite = true;

        //        await _writingRepository.Update(song);


        //        return new BaseResponse<Song>()
        //        {
        //            Data = song,
        //            StatusCode = StatusCode.OK,
        //        };

        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<Song>()
        //        {
        //            Description = $"[Edit] : {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}

        //public async Task<IBaseResponse<Song>> DeleteFromFavorite(int id)
        //{
        //    try
        //    {
        //        var song = await _writingRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        //        if (song == null)
        //        {
        //            return new BaseResponse<Song>()
        //            {
        //                Description = "Song not found",
        //                StatusCode = StatusCode.ArtistNotFound
        //            };
        //        }

        //        song.IsFavorite = false;

        //        await _writingRepository.Update(song);


        //        return new BaseResponse<Song>()
        //        {
        //            Data = song,
        //            StatusCode = StatusCode.OK,
        //        };

        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<Song>()
        //        {
        //            Description = $"[Edit] : {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}


        //public IBaseResponse<List<SongViewModel>> GetFavoriteSongs()
        //{
        //    try
        //    {
        //        var songs = _writingRepository.GetAll().ToList();//.Where(x => x.AuthorId == authorId);//AllAsync();//;
        //        songs = songs.Where(x => x.IsFavorite).ToList();//songs.Count
        //        List<SongViewModel> viewModel = new List<SongViewModel>();
        //        foreach (var song in songs)
        //        {
        //            var artist = _artistRepository.GetAll().FirstOrDefaultAsync(x => x.Id == song.AuthorId);

        //            viewModel.Add(new SongViewModel()
        //            {
        //                Id = song.Id,
        //                Title = song.Title,
        //                AlbumId = song.AlbumId,
        //                AuthorId = song.AuthorId,
        //                IsFavorite = song.IsFavorite,
        //                Length = song.Length,
        //                ArtistName = artist.Result.Name,
        //                Text = song.Text,
        //            });
        //        }
        //        if (!songs.Any())
        //        {
        //            return new BaseResponse<List<SongViewModel>>()
        //            {
        //                Description = "Found 0 elements",
        //                StatusCode = StatusCode.OK
        //            };
        //        }

        //        return new BaseResponse<List<SongViewModel>>()
        //        {
        //            Data = (List<SongViewModel>)viewModel,
        //            StatusCode = StatusCode.OK
        //        };
        //    }

        //    catch (Exception ex)
        //    {

        //        return new BaseResponse<List<SongViewModel>>()
        //        {
        //            Description = $"[GetSongs] : {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}



        public IBaseResponse<List<WritingViewModel>> Search(WritingViewModel model)
        {
            try
            {
                model.Title = model?.Title is null ? "" : model.Title;
                //model.ArtistName = model?.ArtistName is null ? "" : model.ArtistName;
                var songs = _writingRepository.GetAll().ToList();
                IEnumerable<Writing> selectedArtists = new List<Writing>();
                //if (model.ArtistName != "")
                //{
                //    var artistEntity = _artistRepository.GetAll().Where(x => x.Name.ToLower().Contains(model.ArtistName.ToLower()));
                //    int[] artistId = new int[artistEntity.Count()];
                //    int i = 0;
                //    foreach (var artist in artistEntity)
                //    {
                //        artistId[i] = artist.Id;
                //        i++;
                //    }
                //    if (artistId.Length > 0)
                //    {
                //        selectedArtists = from p in songs
                //                          where p.Title.ToLower().Contains(model.Title.ToLower()) && artistId.Contains(p.AuthorId)
                //                          select p;
                //    }

                //}
                //else
                {
                    selectedArtists = from p in songs
                                      where p.Title.ToLower().Contains(model.Title.ToLower())
                                      select p;
                }


                List<WritingViewModel> viewModel = new List<WritingViewModel>();
                foreach (var song in selectedArtists)
                {
                    //var artist = _artistRepository.GetAll().FirstOrDefaultAsync(x => x.Id == song.AuthorId);

                    //viewModel.Add(new SongViewModel()
                    //{
                    //    Id = song.Id,
                    //    Title = song.Title,
                    //    AlbumId = song.AlbumId,
                    //    AuthorId = song.AuthorId,
                    //    IsFavorite = song.IsFavorite,
                    //    Length = song.Length,
                    //    ArtistName = artist.Result.Name,
                    //    Text = song.Text
                    //});
                }


                if (!viewModel.Any())
                {
                    return new BaseResponse<List<WritingViewModel>>()
                    {
                        Description = "Found 0 elements",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<WritingViewModel>>()
                {
                    Data = viewModel,
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse<List<WritingViewModel>>()
                {
                    Description = $"[GetSongs] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        //public IBaseResponse<List<Writing>> BooksIncludeSong(SongViewModel model)
        //{
        //    try
        //    {
        //        var songs = _writingRepository.GetAll().ToList();//.Where(x => x.AuthorId == authorId);//AllAsync();//;
        //        songs = songs.Where(x => x.Title.ToLower().Contains(model.Title.ToLower())).ToList();//songs.Count
        //        int[] indexes = new int[] { };
        //        foreach (var song in songs)
        //        {
        //            if (!indexes.Contains(song.AlbumId))
        //            {
        //                Array.Resize(ref indexes, indexes.Length + 1);
        //                indexes[indexes.Length - 1] = song.AlbumId;
        //            }
        //        }
        //        var albums = _albumRepository.GetAll().ToList().Where(x => indexes.Contains(x.Id)).ToList();

        //        if (!albums.Any())
        //        {
        //            return new BaseResponse<List<Album>>()
        //            {
        //                Description = "Found 0 elements",
        //                StatusCode = StatusCode.OK
        //            };
        //        }

        //        return new BaseResponse<List<Album>>()
        //        {
        //            Data = (List<Album>)albums,
        //            StatusCode = StatusCode.OK
        //        };
        //    }

        //    catch (Exception ex)
        //    {

        //        return new BaseResponse<List<Album>>()
        //        {
        //            Description = $"[GetSongs] : {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}
    }
}
