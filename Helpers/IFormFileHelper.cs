namespace SistemasWeb01.Helpers
{
    public interface IFormFileHelper
    {
        //upload photo to images folder
        Task<string> UploadFile(IFormFile file);
        //delete photo from images folder
        void DeleteFile(string imageName);
    }
}
