using Microsoft.AspNetCore.Http;

namespace AMAPG4.Models.Catalog
{
    public class FileUpload
    {
        public IFormFile file {  get; set; }
        public string NewProductViewModel {  get; set; }
    }
}
