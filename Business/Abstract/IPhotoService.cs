using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IPhotoService
    {
        List<Photo> GetPhotoListByUserId(int userId);
        Photo GetPhoto(int photoId);
        void AddPhoto(Photo photo);
        void DeletePhoto(Photo photo);

    }
}
