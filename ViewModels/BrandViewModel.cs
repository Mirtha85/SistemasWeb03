namespace SistemasWeb01.ViewModels
{
    public class BrandViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ThumbnailImage { get; set; }
        public IFormFile? formFile { get; set; }
    }
}
