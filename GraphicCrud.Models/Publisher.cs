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
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublisherId { get; set; }

        [Required]
        [StringLength(240)]
        public string PublisherName { get; set; }

        //Virtual properties for lazy loading
        [JsonIgnore]
        public virtual ICollection<Developer> Developers { get; private set; }
        [JsonIgnore]
        public virtual ICollection<VideoGame> Games { get; private set; }

        public Publisher()
        {
            Developers = new HashSet<Developer>();
            Games = new HashSet<VideoGame>();
        }

        public Publisher(int publisherId, string pubName)
        {
            this.PublisherId = publisherId;
            this.PublisherName = pubName;
            Developers = new HashSet<Developer>();
            Games = new HashSet<VideoGame>();
        }

        public Publisher(string init)
        {
            string[] splittedSource = init.Split(';');
            this.PublisherId = int.Parse(splittedSource[0]);
            this.PublisherName = splittedSource[1];
            Developers = new HashSet<Developer>();
            Games = new HashSet<VideoGame>();
        }
        public Publisher(string init, char sep)
        {
            string[] splittedSource = init.Split(sep);
            this.PublisherId = int.Parse(splittedSource[0]);
            this.PublisherName = splittedSource[1];
            Developers = new HashSet<Developer>();
            Games = new HashSet<VideoGame>();
        }

        public override bool Equals(object obj)
        {
            Publisher publisher = obj as Publisher;
            if (publisher == null) return false;
            else
            {
                return publisher.PublisherId == this.PublisherId && publisher.PublisherName == this.PublisherName;
            }
        }
        public override int GetHashCode()
        {
            return PublisherId;
        }
    }
}
