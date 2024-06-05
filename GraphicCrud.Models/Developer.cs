using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraphicCrud.Models
{
    public class Developer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeveloperId { get; set; }
        [Required]
        [StringLength(240)]
        public string DeveloperName { get; set; }
        public int PublisherId { get; set; }

        //Virtual properties for lazy loading
        [JsonIgnore]
        public virtual ICollection<VideoGame> Games { get; set; }
        public virtual Publisher Publisher { get; set; }


        public Developer()
        {
            Games = new HashSet<VideoGame>();
        }

        public Developer(int DevId, string name, int publisher)
        {
            this.DeveloperId = DevId;
            this.DeveloperName = name;
            this.PublisherId = publisher;
            Games = new HashSet<VideoGame>();
        }

        public Developer(string init)
        {
            string[] splittedSource = init.Split(';');
            this.DeveloperId = int.Parse(splittedSource[0]);
            this.DeveloperName = splittedSource[1];
            this.PublisherId = int.Parse(splittedSource[2]);
            Games = new HashSet<VideoGame>();
        }

        public Developer(string init, char sep)
        {
            string[] splittedSource = init.Split(sep);
            this.DeveloperId = int.Parse(splittedSource[0]);
            this.DeveloperName = splittedSource[1];
            this.PublisherId = int.Parse(splittedSource[2]);
            Games = new HashSet<VideoGame>();
        }
    }
}
