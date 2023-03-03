using API_Movies.Context;
using API_Movies.Interfaces;
using API_Movies.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Movies.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly MovieContext _db;

        public MovieServices(MovieContext db)
        {
            _db = db;
        }

        public async Task<MovieModel> AddAsync(MovieModel model)
        {
            var result = new MovieModel();

            try
            {
                _db.Movies.Add(model);
                await _db.SaveChangesAsync();
                result = model;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }

        public async Task<MovieModel> ByIdAsync(int id)
        {
            var result = new MovieModel();

            try
            {
                if (_db.Movies != null)
                {
                    var movie = await _db.Movies.FindAsync(id);
                    if (movie != null) result = movie;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }

        public async Task<IEnumerable<MovieModel>> ListAsync()
        {
            var result = new List<MovieModel>();

            try
            {
                if (_db.Movies != null)
                {
                    var movies = await _db.Movies.ToListAsync();
                    if (movies != null) result = movies;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var result = false;

            try
            {
                if (_db.Movies != null)
                {
                    var movie = await _db.Movies.FindAsync(id);
                    if (movie == null) throw new NotImplementedException();

                    _db.Movies.Remove(movie);
                    await _db.SaveChangesAsync();
                    result = true;
                }
            }
            catch (Exception ex) 
            {
                var error = ex.Message;
            }
            return result;
        }

        public async Task<bool> ApplyPatchAsync<TEntity>(TEntity entityName, List<PatchModel> models) where TEntity : class
        {
            var result = false;

            try
            {
                if (_db.Movies != null)
                {
                    foreach (var pm in models)
                    {
                        if (pm.PropertyType.ToLower() == "string")
                            pm.PropertyValue = pm.PropertyValue.ToString();
                        if (pm.PropertyType.ToLower() == "double")
                            pm.PropertyValue = double.Parse(pm.PropertyValue.ToString());
                    }

                    var nameValuePairProperties = models.ToDictionary(a => a.PropertyName, a => a.PropertyValue);
                    var curr = _db.Entry(entityName);
                    curr.CurrentValues.SetValues(nameValuePairProperties);
                    curr.State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
            return result;
        }
    }
}
