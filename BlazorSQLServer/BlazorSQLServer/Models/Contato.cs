using System.ComponentModel.DataAnnotations;

namespace BlazorSQLServer.Models
{
    public class Contato
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;
    }
}