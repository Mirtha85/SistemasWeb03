using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Interfaces;
using SistemasWeb01.ViewModels;
using static System.Collections.Specialized.BitVector32;

namespace SistemasWeb01.Repository.Implementations
{
    public class PictureRepository : IPictureRepository
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public PictureRepository(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }

        public IEnumerable<Picture> AllImages => _shoppingDbContext.Pictures.Include(p => p.Product).ToList();

        public void CreatePicture(Picture picture)
        {
            try
            {
                _shoppingDbContext.Pictures.Add(picture);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeletePicture(Picture picture)
        {
            _shoppingDbContext.Pictures.Remove(picture);
            _shoppingDbContext.SaveChanges();
        }

        public void EditPicture(ProductImageViewModel productImageViewModel)
        {
            try
            {
                Picture picture = new()
                {
                    Id = productImageViewModel.Id,
                    PictureName = productImageViewModel.PictureName,
                    ProductId = productImageViewModel.ProductId
                };
                _shoppingDbContext.Pictures.Update(picture);
                _shoppingDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Picture? GetPictureById(int id)
        {
            return _shoppingDbContext.Pictures
                .Include(p => p.Product)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Picture> GetPicturesByProductId(int productId)
        {
            return  _shoppingDbContext.Pictures
                 .Where(p => p.ProductId == productId)
                 .ToList();
        }
    }
}
