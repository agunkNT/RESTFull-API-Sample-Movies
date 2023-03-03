using API_Movies.Models;

namespace API_Movies.Interfaces
{
    public interface IMovieServices
    {
        Task<IEnumerable<MovieModel>> ListAsync();
        Task<MovieModel> ByIdAsync(int id);
        Task<MovieModel> AddAsync(MovieModel model);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> ApplyPatchAsync<TEntity>(TEntity entityName, List<PatchModel> models) where TEntity : class;
    }
}
