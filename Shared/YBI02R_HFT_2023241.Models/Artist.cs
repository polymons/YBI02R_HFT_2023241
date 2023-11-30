﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace YBI02R_HFT_2023241.Models
{
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArtistID { get; set; }

        [Required]
        public string Name { get; set; }

        public int StudioID { get; set; }

        public virtual Publisher Studio { get; set; }

        [JsonIgnore]
        public virtual ICollection<Song> Songs { get; set; }

        [Range(18,100)]
        public int Age { get; set; }

        public Artist()
        {

        }
        public Artist(int id, string name, int studioID, int age)
        {
            ArtistID = id;
            Name = name;
            StudioID = studioID;
            Songs = new HashSet<Song>();
            Age = age;
        }
        public Artist(string name, int studioID, int age)
        {
            Age = age;
            Name = name;
            StudioID = studioID;
        }
    }
}