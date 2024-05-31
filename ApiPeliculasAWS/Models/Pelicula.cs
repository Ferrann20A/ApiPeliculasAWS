using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculasAWS.Models
{
    [Table("pelismysqlv2")]
    public class Pelicula
    {
        [Key]
        [Column("IdPelicula")]
        public int IdPelicula { get; set; }
        [Column("Genero")]
        public string Genero { get; set; }
        [Column("Titulo")]
        public string Titulo { get; set; }
        [Column("Argumento")]
        public string Argumento { get; set; }
        [Column("Foto")]
        public string Foto { get; set; }
        [Column("Actores")]
        public string Actores { get; set; }
        [Column("Precio")]
        public int Precio { get; set; }
        [Column("Youtube")]
        public string Youtube { get; set; }
    }
}
