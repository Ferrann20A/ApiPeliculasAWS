using ApiPeliculasAWS.Models;
using ApiPeliculasAWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculasAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private RepositoryPeliculas repo;

        public PeliculasController(RepositoryPeliculas repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pelicula>>> Get()
        {
            return await this.repo.GetPeliculasAsync();
        }

        [HttpGet("{idpelicula}")]
        public async Task<ActionResult<Pelicula>> Find(int idpelicula)
        {
            return await this.repo.FindPeliculaAsync(idpelicula);
        }

        [HttpGet]
        [Route("[action]/{actor}")]
        public async Task<ActionResult<List<Pelicula>>> PeliculasActores(string actor)
        {
            return await this.repo.GetPeliculasActoresAsync(actor);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Pelicula peli)
        {
            await this.repo.CreatePeliculaAsync(peli.Genero, peli.Titulo, peli.Argumento, peli.Foto,
                peli.Actores, peli.Precio, peli.Youtube);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Pelicula peli)
        {
            await this.repo.UpdatePeliculaAsyc(peli.IdPelicula, peli.Genero, peli.Titulo, peli.Argumento,
                peli.Foto, peli.Actores, peli.Precio, peli.Youtube);
            return Ok();
        }

        [HttpDelete("{idpelicula}")]
        public async Task<ActionResult> Delete(int idpelicula)
        {
            await this.repo.DeletePeliculaAsync(idpelicula);
            return Ok();
        }
    }
}
