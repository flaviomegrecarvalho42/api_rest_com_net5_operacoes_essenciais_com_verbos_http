using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filmeModel = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filmeModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(RecuperarFilmePorId), new { filmeModel.Id}, filmeModel);
        }

        [HttpGet]
        public IActionResult RecuperarFilme()
        {
            return Ok(_context.Filmes);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperarFilmePorId(int id)
        {
            Filme filmeModel = _context.Filmes.FirstOrDefault(f => f.Id == id);

            if (filmeModel == null)
            {
                return NotFound();
            }

            ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filmeModel);

            return Ok(filmeDto);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filmeModel = _context.Filmes.FirstOrDefault(f => f.Id == id);

            if (filmeModel == null)
            {
                return NotFound();
            }

            _mapper.Map(filmeDto, filmeModel);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(f => f.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
