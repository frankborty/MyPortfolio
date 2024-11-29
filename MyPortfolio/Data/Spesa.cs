namespace MyPortfolio.Data
{
    public class Spesa
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Costo { get; set; }
        public string Descrizione { get; set; } = string.Empty;

        // Chiave esterna per la relazione con Categoria (Tipo1)
        public int Tipo1Id { get; set; }

        // Relazione con Categoria (Tipo1)
        public Categoria Tipo1 { get; set; }

        // Chiave esterna per la relazione con SottoCategoria (Tipo2)
        public int Tipo2Id { get; set; }

        // Relazione con SottoCategoria (Tipo2)
        public SottoCategoria Tipo2 { get; set; }
    }
}
