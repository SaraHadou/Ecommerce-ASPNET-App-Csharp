using System.ComponentModel.DataAnnotations;
using eMovies.Data.Base;

namespace eMovies.Models
{
    public class Cinema : IEntityBase
	{
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        public string Logo { get; set; }

		[Display(Name = "Cinema Name")]
		public string Name { get; set; }

		[Display(Name = "Description")]
		public string Description { get; set; }

        //------------------ Relations ------------------//
        // Movie
        public List<Movie>? Movies { get; set; }

    }
}
