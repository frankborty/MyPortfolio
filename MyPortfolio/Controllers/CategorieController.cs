using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.DTO;

namespace MyPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly DataDbContext _dbContext;

        public CategorieController(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST: api/Categoria/aggiungi
        [HttpPost("aggiungi")]
        public async Task<IActionResult> AggiungiCategorie([FromBody] List<string> categorie)
        {
            if (categorie == null || categorie.Count == 0)
            {
                return BadRequest("La lista delle categorie non può essere vuota.");
            }

            List<Categoria> categorieToAdd = new List<Categoria>();
            // Aggiungi le categorie al contesto
            foreach(string categoryName in categorie)
            {
                categorieToAdd.Add(new Categoria() { Nome = categoryName });
            }
            _dbContext.Categorie.AddRange(categorieToAdd);

            try
            {
                // Salva le modifiche nel database
                await _dbContext.SaveChangesAsync();
                return Ok(new { message = "Categorie aggiunte con successo!" });
            }
            catch (DbUpdateException ex)
            {
                // Gestione degli errori in caso di fallimento del salvataggio
                return StatusCode(500, $"Errore durante l'aggiornamento del database: {ex.Message}");
            }
        }

        // POST: api/Categoria/{id}/aggiungi-sottocategorie
        [HttpPost("{categoriaId}/aggiungi-sottocategorie")]
        public async Task<IActionResult> AggiungiSottocategorie(int categoriaId, [FromBody] List<string> sottocategorie)
        {
            if (sottocategorie == null || sottocategorie.Count == 0)
            {
                return BadRequest("La lista delle sottocategorie non può essere vuota.");
            }

            // Trova la categoria specificata
            var categoria = await _dbContext.Categorie.FindAsync(categoriaId);

            if (categoria == null)
            {
                return NotFound("Categoria non trovata.");
            }

            // Crea le sottocategorie e le associa alla categoria
            foreach (var nome in sottocategorie)
            {
                var sottocategoria = new SottoCategoria
                {
                    Nome = nome,
                    CategoriaId = categoriaId  // Associa la sottocategoria alla categoria
                };

                _dbContext.SottoCategorie.Add(sottocategoria);
            }

            try
            {
                // Salva le modifiche nel database
                await _dbContext.SaveChangesAsync();
                return Ok(new { message = "Sottocategorie aggiunte con successo!" });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Errore durante l'aggiornamento del database: {ex.Message}");
            }
        }

        // GET: api/Categoria/{id}/sottocategorie
        [HttpGet("{categoriaId}/sottocategorie")]
        public async Task<IActionResult> GetSottocategorie(int categoriaId)
        {
            // Trova la categoria con l'Id specificato
            var categoria = await _dbContext.Categorie
                                            .Include(c => c.SottoCategorie)  // Include le sottocategorie
                                            .FirstOrDefaultAsync(c => c.Id == categoriaId);

            if (categoria == null)
            {
                return NotFound("Categoria non trovata.");
            }

            List<SottoCategoriaDTO> sottocategorieDto = categoria.SottoCategorie
                                        .Select(s => new SottoCategoriaDTO
                                        {
                                            Id = s.Id,
                                            Name = s.Nome,
                                            CategoryId = categoria.Id
                                        })
                                        .ToList();

            // Restituisce la lista delle sottocategorie associate alla categoria
            return Ok(sottocategorieDto);
        }
    }
}
