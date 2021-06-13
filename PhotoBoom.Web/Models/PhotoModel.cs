using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PhotoBoom.Web.Models
{
    public class PhotoModel
    {
        public int PhotoId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Fotoğraf alanı boş geçilemez")]
        public IFormFile File { get; set; }
    }
}
