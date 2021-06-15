using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace musica.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }
        [Display(Name = "Nombre")]
        public string SongName { get; set; }
        [Display(Name = "Artista")]
        public string SongArtist { get; set; }
        [Display(Name = "Productor/a")]
        public string SongProducer { get; set; }
    }
}
