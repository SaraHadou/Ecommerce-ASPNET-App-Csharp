using System.ComponentModel.DataAnnotations;
using eMovies.Data.Base;

namespace eMovies.Models
{
    public class Producer : IEntityBase
	{
        [Key]
        public int Id { get; set; }

		[Display(Name = "Profile Picture")]
		public string ProfilePictureURL { get; set; }
		
        [Display(Name = "Full Name")]
		public string FullName { get; set; }

		[Display(Name = "Biography")]
		public string Bio { get; set; }

        //------------------ Relations ------------------//
        // Movie
        public List<Movie>? Movies{ get; set; }

    }
}
