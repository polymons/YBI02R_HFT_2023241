using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace YBI02R_HFT_2023241.Models
{
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudioID { get; set; }

        [StringLength(2)]
        public string Country { get; set; }

        [StringLength(240)]
        public string City { get; set; }

        [Required]
        [StringLength(240)]
        public string StudioName { get; set; }

        //[NotMapped]
        [JsonIgnore]
        public virtual ICollection<Artist> Artists { get; set; }

        public Publisher()
        {
            Artists = new HashSet<Artist>(); //Stores a collection of unique elements
        }
        public Publisher(string country, string studioName, string studioCity, int studioID)
        {
            Country = country;
            StudioName = studioName;
            City = studioCity;
            StudioID = studioID;
        }
        public Publisher(string country, string studioName, int studioID)
        {
            Country = country;
            StudioName = studioName;
            StudioID = studioID;
        }
        public Publisher(string country, string studioName, string studioCity)
        {
            Country = country;
            StudioName = studioName;
            City = studioCity;
        }

        //public override string ToString()
        //{
        //    return $"{Country}, {StudioName}, {StudioID}";
        //}
    }
}
