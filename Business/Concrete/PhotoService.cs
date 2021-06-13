using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class PhotoService : IPhotoService
    {
        private IPhotoDal _photoDal;
        public PhotoService(IPhotoDal photoDal)
        {
            _photoDal = photoDal;
        }
        public List<Photo> GetPhotoListByUserId(int userId)
        {
            if (userId < 1)
                return null;
            return _photoDal.GetListWithUser(p => p.UserId == userId).ToList();
        }

        public Photo GetPhoto(int photoId)
        {
            if (photoId < 1)
                return null;
            return _photoDal.Get(p => p.PhotoId == photoId);
        }

        public void AddPhoto(Photo photo)
        {
            if (photo != null)
                _photoDal.Add(photo);
        }

        public void DeletePhoto(Photo photo)
        {
            if (photo != null)
                _photoDal.Delete(photo);
        }

    }
}
