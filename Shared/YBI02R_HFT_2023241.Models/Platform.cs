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
    public class Platform
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlatformId { get; set; }

        [StringLength(240)]
        public string Name { get; set; }

        [Range(-100000, 100000)]
        public double Income { get; set; }


        public virtual ICollection<Serie> Series { get; set; }

        public Platform()
        {
                
        }
    }
}
