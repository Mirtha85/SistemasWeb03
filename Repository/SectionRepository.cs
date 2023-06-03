using SistemasWeb01.Models;
using SistemasWeb01.Repository.IRepository;

namespace SistemasWeb01.Repository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly BethesdaPieShopDbContext _bethesdaPieShopDbContext;
        public SectionRepository(BethesdaPieShopDbContext bethesdaPieShopDbContext)
        {
            _bethesdaPieShopDbContext = bethesdaPieShopDbContext;
        }
        public IEnumerable<Section> AllSections => _bethesdaPieShopDbContext.Sections.ToList();

        public void CreateSection(Section section)
        {
            try
            {
                _bethesdaPieShopDbContext.Sections.Add(section);
                _bethesdaPieShopDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteSection(Section section)
        {
            _bethesdaPieShopDbContext.Sections.Remove(section);
            _bethesdaPieShopDbContext.SaveChanges();
        }

        public Section? GetSection(int id)
        {
            Section? section = _bethesdaPieShopDbContext.Sections
                .FirstOrDefault(s => s.Id == id);
            return section;
        }

        public void EditSection(Section section)
        {
            try
            {
                _bethesdaPieShopDbContext.Sections.Update(section);
                _bethesdaPieShopDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
