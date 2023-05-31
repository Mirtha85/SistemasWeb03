using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.IRepository
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> AllCategories { get; }
        Categoria? GetCategory(int id);

        void CreateCategory(Categoria categoria);

        void EditCategory(Categoria categoria);

        void DeleteCategory(Categoria categoria);
    }
}
