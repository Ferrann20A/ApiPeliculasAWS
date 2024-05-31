using ApiPeliculasAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculasAWS.Data
{
    public class PeliculasContext: DbContext
    {
        public PeliculasContext(DbContextOptions<PeliculasContext> options)
            : base(options) { }

        public DbSet<Pelicula> Peliculas { get; set; }
    }
}
