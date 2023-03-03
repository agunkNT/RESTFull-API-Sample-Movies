using API_Movies.Interfaces;
using Xunit;

namespace Test_xUnit_Movies
{
    public class UnitTest1
    {
        private readonly IMovieServices _movie;

        public UnitTest1(IMovieServices movie)
        {
            _movie = movie;
        }

        [Fact]
        public void IsListEmpty()
        {
            var result = true;
            var check = _movie.ListAsync();
            if (check.Result.Count() == 0) result = false;
            Assert.False(result, "Error: ListAsync");
        }
    }
}