namespace SistemasWeb01.Models
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
