using System.ComponentModel.DataAnnotations;

namespace JuanDeDiosFrausto.Entidades
{
    public class Song
    {
        [Key]
        [Required]
        public string ISMN { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int AlbumYear { get; set; }
        public string Duration { get; set; }
        public string Image { get; set; }
    }
}
