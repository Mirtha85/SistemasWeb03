using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.IRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
