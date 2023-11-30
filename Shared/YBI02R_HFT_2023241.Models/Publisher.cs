﻿using System.Collections.Generic;
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


        public string City { get; set; }

        [Required]
        public string StudioName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Artist> Artists { get; set; }

        public Publisher()
        {

        }
        public Publisher(string country, string studioName, string studioCity, int studioID)
        {
            Country = country;
            StudioName = studioName;
            City = studioCity;
            StudioID = studioID;
            Artists = new HashSet<Artist>();
        }
        public Publisher(string country, string studioName, string studioCity)
        {
            Country = country;
            StudioName = studioName;
            City = studioCity;
        }
    }
}
