using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    Task<List<MovieCardModel>> GetTop30GrossingMovies();
    Task<MovieDetailsModel> GetMovieDetails(int id);
    Task<PagedResultSet<MovieCardModel>> GetMoviesByGenrePagination( int genreId, int pageSize = 30, int pageNumber =1);
}