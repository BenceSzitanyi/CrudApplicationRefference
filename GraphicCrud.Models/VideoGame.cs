using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraphicCrud.Models
{
    public class VideoGame
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VideoGameId { get; set; }
        [StringLength(240)]
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        [StringLength(20)]
        public string Platform { get; set; }
        public double MetacriticsScore { get; set; }
        public int PublisherId { get; set; }
        public int DeveloperId { get; set; }

        //Virtial properties for lazy loading
        public virtual Developer Developer { get; set; }
        public virtual Publisher Publisher { get; set; }

        public VideoGame()
        {

        }

        public VideoGame(int videogameid, string title, DateTime release, string platform, double meta, int publisher, int developer)
        {
            this.VideoGameId = videogameid;
            this.Title = title;
            this.ReleaseDate = release;
            this.MetacriticsScore = meta;
            this.Platform = platform;
            this.PublisherId = publisher;
            this.DeveloperId = developer;
        }

        public VideoGame(string init, char sep = ';')
        {
            string[] splittedSource = init.Split(sep);
            this.VideoGameId = int.Parse(splittedSource[0]);
            this.Title = splittedSource[1];
            this.ReleaseDate = DateTime.Parse(splittedSource[2]);
            this.MetacriticsScore = double.Parse(splittedSource[3]);
            this.Platform = splittedSource[4];
            this.PublisherId = int.Parse(splittedSource[5]);
            this.DeveloperId = int.Parse(splittedSource[6]);
        }

        //Method overrides
        public override bool Equals(object obj)
        {
            VideoGame y = obj as VideoGame;
            if (y == null)
            {
                return false;
            }
            return (this.VideoGameId == y.VideoGameId)
                && (this.Title == y.Title)
                && (this.ReleaseDate == y.ReleaseDate);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.VideoGameId, this.MetacriticsScore, this.PublisherId);
        }
    }
}
