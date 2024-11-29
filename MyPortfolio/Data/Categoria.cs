namespace MyPortfolio.Data
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        // Relazione con la tabella SottoCategoria
        public ICollection<SottoCategoria> SottoCategorie { get; set; }
    }
}
