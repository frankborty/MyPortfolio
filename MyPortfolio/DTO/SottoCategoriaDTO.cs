using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyPortfolio.DTO
{
    public class SottoCategoriaDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }= -1;
        public int CategoryId { get; set; } = -1;
    }
}
