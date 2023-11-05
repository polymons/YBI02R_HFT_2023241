using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBI02R_HFT_2023241.Models
{
    public class Song
    {
        [MaxLength(1000)]
        [Required]
        public string Title { get; set; }
        public string Genre { get; set; }
        public virtual Artist Artist { get; set; }
        [ForeignKey(nameof(Artist))]
        public int ArtistID { get; set; }
        [Range(0,1500)]
        public int Length { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SongID { get; set; }

        public Song(string title, string genre, int length, int songID, int artistID)
        {
            Title = title;
            Genre = genre;
            Length = length;
            SongID = songID;
            ArtistID = artistID;
        }

        public Song()
        {

        }

        public Song(string title, string genre, int artistID)
        {
            Title = title;
            Genre = genre;
            ArtistID = artistID;
        }
    }
}
