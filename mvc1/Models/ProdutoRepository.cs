 namespace mvc1.Models
{
    public class ProdutoRepository : IRepository
    {
        private readonly AppDbContext context;
      public ProdutoRepository(AppDbContext ctx)
        {
            this.context = ctx;
        }

        public IEnumerable<Produto> Produtos => context.Produtos;
    }
}