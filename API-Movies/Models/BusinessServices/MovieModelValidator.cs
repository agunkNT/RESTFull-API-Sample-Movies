using FluentValidation;

namespace API_Movies.Models.BusinessServices
{
    public class MovieModelValidator : AbstractValidator<MovieModel>
    {
        /// <summary>  
        /// Validator rules for MovieModel
        /// </summary>  
        public MovieModelValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("The Movie ID must be at greather than 0.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("The Movie Title cannot be blank.")
                .Length(0, 100)
                .WithMessage("The Movie Title cannot be more than 100 characters.");

            RuleFor(x => x.Rating)
                .GreaterThan(0)
                .WithMessage("The Movie Rating must be at greather than 0.");
        }
    }
}
