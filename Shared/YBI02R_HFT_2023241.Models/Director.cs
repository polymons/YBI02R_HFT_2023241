using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBI02R_HFT_2023241.Models
{
    public class Director
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DirectorId { get; set; }

        [Required]
        [StringLength(240)]
        public string DirectorName { get; set; }

        public virtual ICollection<Serie> Series { get; set; }

        public Director()
        {
            Series = new HashSet<Serie>();
        }

        public Director(string line)
        {
            string[] split = line.Split('#');
            DirectorId = int.Parse(split[0]);
            DirectorName = split[1];
            Series = new HashSet<Serie>();
        }
    }
}
