using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eMovies.Data.Base;
using eMovies.Data.Enums;

namespace eMovies.Models
{
    public class Movie : IEntityBase
	{

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //------------------ Relations ------------------//
        // Actor_Movie
        public List<Actor_Movie> Actors_Movies { get; set; }

        // Cinema
        [ForeignKey("CinemaId")]
        public int CinemaId { get; set; }    
        public Cinema Cinema { get; set; }

        // Producer
        [ForeignKey("ProducerId")]
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }

    }
}