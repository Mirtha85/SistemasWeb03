using SistemasWeb01.Models;
using SistemasWeb01.Repository.IRepository;

namespace SistemasWeb01.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly BethesdaPieShopDbContext _bethesdaPieShopDbContext;
        public CategoriaRepository(BethesdaPieShopDbContext bethesdaPieShopDbContext)
        {
            _bethesdaPieShopDbContext = bethesdaPieShopDbContext;
        }
        public IEnumerable<Categoria> AllCategories => _bethesdaPieShopDbContext.Categorias.ToList();

        public void CreateCategory(Categoria categoria)
        {
            _bethesdaPieShopDbContext.Categorias.Add(categoria);
            _bethesdaPieShopDbContext.SaveChanges();
        }

        public void DeleteCategory(Categoria categoria)
        {
            _bethesdaPieShopDbContext.Categorias.Remove(categoria);
            _bethesdaPieShopDbContext.SaveChanges();
        }

        public Categoria? GetCategory(int id)
        {
            Categoria? categoria = _bethesdaPieShopDbContext.Categorias
                .FirstOrDefault(c => c.Id == id);
            return categoria;
        }

        public void EditCategory(Categoria categoria)
        {
            _bethesdaPieShopDbContext.Categorias.Update(categoria);
            _bethesdaPieShopDbContext.SaveChanges();
        }
    }
}
