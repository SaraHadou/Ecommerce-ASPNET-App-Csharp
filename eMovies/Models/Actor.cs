using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace eMovies.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }

        public string ProfilePictureURL { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }

        //------------------ Relations ------------------//
        // Actor_Movie
        public List<Actor_Movie> Actors_Movies { get; set; }


    }   
}
    