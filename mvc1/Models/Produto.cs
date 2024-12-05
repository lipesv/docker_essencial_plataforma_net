namespace mvc1.Models
{
    public class Produto
    {
        public Produto() { }

        public Produto(int id, string nome, string categoria, decimal preco)
        {
            ProdutoId = id;
            Nome = nome;
            Categoria = categoria;
            Preco = preco;
        }

        public int ProdutoId { get; set; }
        public string? Nome { get; set; }
        public string? Categoria { get; set; }
        public decimal Preco { get; set; }
    }
}