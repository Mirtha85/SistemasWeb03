using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.IRepository
{
    public interface ISectionRepository
    {
        IEnumerable<Section> AllSections { get; }
        Section? GetSection(int id);

        void CreateSection(Section section);

        void EditSection(Section section);

        void DeleteSection(Section section);
    }
}
