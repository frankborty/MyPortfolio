using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Data
{
    public class SottoCategoria
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;

        // Chiave esterna per la relazione con Categoria
        public int CategoriaId { get; set; }

        // Relazione con Categoria
        public Categoria Categoria { get; set; }
    }
}
