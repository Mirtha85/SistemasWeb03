namespace SistemasWeb01.Helpers
{
    public class FormFileHelper : IFormFileHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FormFileHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public void DeleteFile(string imageName)
        {

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                string folder = "images/";
                string imageName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
                string fullPath = $"{folder}{imageName}";
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, fullPath);
                using (var fileStream = new FileStream(serverFolder, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return imageName;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
