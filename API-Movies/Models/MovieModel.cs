using API_Movies.Models.BusinessServices;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace API_Movies.Models
{
    [Validator(typeof(MovieModelValidator))]
    public class MovieModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public double Rating { get; set; }
        public string? Image { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }

    public class PatchModel
    {
        public string PropertyType { get; set; }
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
    }
}
