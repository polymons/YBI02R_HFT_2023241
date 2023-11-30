using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBI02R_HFT_2023241.Models
{
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActorId { get; set; }

        [Required]
        [StringLength(240)]
        public string ActorName { get; set; }

        public virtual ICollection<Serie> Series { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public Actor()
        {

        }

        public Actor(string line)
        {
            string[] split = line.Split('#');
            ActorId = int.Parse(split[0]);
            ActorName = split[1];
        }
    }
}
