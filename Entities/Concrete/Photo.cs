using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Photo : IEntity
    {
        [Key]
        public int PhotoId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string MimeType { get; set; }
    }
}
