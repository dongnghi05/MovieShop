using ApplicationCore.Contracts.Respositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastucture.Repositories;

namespace Infrastructure.Services;

public class CastService : ICastService
{
    private readonly ICastRepository _castRepository;


    public CastService(ICastRepository castRepository)
    {
        _castRepository = castRepository;
    }

    public async Task<CastDetailsModel> GetCastDetails(int id)
    {
        var cast = await _castRepository.GetById(id);
        var castDetails = new CastDetailsModel
        {
            Id = cast.Id,
            ProfilePath = cast.ProfilePath,
            Name = cast.Name,
            Gender = cast.Gender,
            TmdbUrl = cast.TmdbUrl

        };
        castDetails.movies = new List<MovieDetailsModel>();
        foreach (var i in cast.MovieCasts)
        {
            castDetails.movies.Add(new MovieDetailsModel
            {
                Id = i.Movie.Id, Budget = i.Movie.Budget, Title = i.Movie.Title
            });
        }
        return castDetails;
    }
}