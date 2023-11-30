using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YBI02R_HFT_2023241.Models
{
    public class Serie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerieId { get; set; }

        [StringLength(240)]
        public string Title { get; set; }

        [Range(0, 10000)]
        public double Income { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        public DateTime Release { get; set; }

        public int DirectorId { get; set; }

        public virtual Director Director { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        public virtual ICollection<Role> Roles { get; set; }


        public Serie()
        {

        }

        public Serie(string line)
        {
            string[] split = line.Split('#');
            SerieId = int.Parse(split[0]);
            Title = split[1];
            Income = double.Parse(split[2]);
            DirectorId = int.Parse(split[3]);
            Release = DateTime.Parse(split[4].Replace('*', '.'));
            Rating = double.Parse(split[5]);
        }

    }
}
