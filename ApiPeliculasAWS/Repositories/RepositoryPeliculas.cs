using ApiPeliculasAWS.Data;
using ApiPeliculasAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculasAWS.Repositories
{
    public class RepositoryPeliculas
    {
        private PeliculasContext context;

        public RepositoryPeliculas(PeliculasContext context)
        {
            this.context = context;
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            return await this.context.Peliculas.ToListAsync();
        }

        public async Task<List<Pelicula>> GetPeliculasActoresAsync(string actor)
        {
            var consulta = from datos in this.context.Peliculas
                           where datos.Actores.Contains(actor)
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<Pelicula> FindPeliculaAsync(int id)
        {
            return await this.context.Peliculas.FirstOrDefaultAsync(x => x.IdPelicula == id);
        }

        private async Task<int> GetMaxIdPeliAsync()
        {
            return await this.context.Peliculas.MaxAsync(x => x.IdPelicula) + 1;
        }

        public async Task CreatePeliculaAsync(string genero, string titulo, string argumento,
            string foto, string actores, int precio, string youtube)
        {
            Pelicula peli = new Pelicula
            {
                IdPelicula = await this.GetMaxIdPeliAsync(),
                Genero = genero,
                Titulo = titulo,
                Argumento = argumento,
                Foto = foto,
                Actores = actores,
                Precio = precio,
                Youtube = youtube
            };
            await this.context.Peliculas.AddAsync(peli);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdatePeliculaAsyc(int idpelicula, string genero, string titulo, string argumento,
            string foto, string actores, int precio, string youtube)
        {
            Pelicula peli = await this.FindPeliculaAsync(idpelicula);
            peli.Genero = genero;
            peli.Titulo = titulo;
            peli.Argumento = argumento;
            peli.Foto = foto;
            peli.Actores = actores;
            peli.Precio = precio;
            peli.Youtube = youtube;
            await this.context.SaveChangesAsync();
        }

        public async Task DeletePeliculaAsync(int id)
        {
            Pelicula peli = await this.FindPeliculaAsync(id);
            this.context.Peliculas.Remove(peli);
            await this.context.SaveChangesAsync();
        }
    }
}
