using SistemasWeb01.Models;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface IPictureRepository
    {

        IEnumerable<Picture> AllImages { get; }
        IEnumerable<Picture> GetPicturesByProductId (int productId);
        Picture? GetPictureById(int id);

        //save image name in database
        //void CreatePicture(PictureViewModel imageViewModel); esto se hara en el producto viewmodel
        void CreatePicture(Picture picture);

        void EditPicture(ProductImageViewModel productImageViewModel);

        void DeletePicture(Picture picture);
    }
}
