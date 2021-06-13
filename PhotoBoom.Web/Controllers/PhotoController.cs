using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoBoom.Web.Models;
using System;
using System.IO;
using System.Linq;

namespace PhotoBoom.Web.Controllers
{
    public class PhotoController : Controller
    {
        const string SessionName = "_Name";
        const string SessionId = "_Id";

        private IPhotoService _photoService;
        private IUserService _userService;
        public PhotoController(IPhotoService photoService, IUserService userService)
        {
            _photoService = photoService;
            _userService = userService;
        }

        [HttpGet("{userId:int?}")]
        public IActionResult Index(int userId = 1)
        {
            var user = _userService.GetUserById(userId);
            if (user != null)
            {
                HttpContext.Session.SetString(SessionName, user.Name);
                HttpContext.Session.SetInt32(SessionId, user.UserId);
            }
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32(SessionId));
            var photos = _photoService.GetPhotoListByUserId(userId);
            var photoList = photos.Select(x => new PhotoModel
            {
                Name = x.Name,
                PhotoId = x.PhotoId,
                Tag = x.Tag,
                Title = x.Title,
                UserName = x.User.Name
            }).ToList();
            return View(photoList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PhotoModel photoModel)
        {
            if (photoModel.File != null)
            {
                var result = IsPhoto(photoModel.File);
                if (result)
                {
                    var imageName = SavePhoto(photoModel);

                    var userId = Convert.ToInt32(HttpContext.Session.GetInt32(SessionId));
                    var photo = new Photo
                    {
                        UserId = userId,
                        Tag = photoModel.Tag,
                        Title = photoModel.Title,
                        Name = imageName,
                        MimeType = photoModel.File.ContentType
                    };
                    _photoService.AddPhoto(photo);
                    return RedirectToAction("List");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detail(int photoId)
        {
            var photo = _photoService.GetPhoto(photoId);
            if (photo != null)
            {
                var tags = ReplaceTag(photo.Tag);
                var model = new PhotoModel
                {
                    Name = photo.Name,
                    PhotoId = photo.PhotoId,
                    Tag = tags,
                    Title = photo.Title,
                };
                return View(model);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int photoId)
        {
            var photo = _photoService.GetPhoto(photoId);
            if (photo != null)
            {
                DeletePhoto(photo.Name);
                _photoService.DeletePhoto(photo);
            }
            return RedirectToAction("List");
        }

        private bool IsPhoto(IFormFile file)
        {
            if (file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/png")
                return true;
            return false;
        }

        private string SavePhoto(PhotoModel photoModel)
        {
            string photoName = Guid.NewGuid().ToString() + Path.GetExtension(photoModel.File.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", photoName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                photoModel.File.CopyTo(stream);
            }
            return photoName;
        }

        private void DeletePhoto(string photoName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", photoName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        private string ReplaceTag(string tags)
        {
            var tag = tags.Split(',');
            var tagString = string.Empty;
            for (int i = 0; i < tag.Count(); i++)
            {
                var newTag = tag[i].Replace(tag[i], "#" + tag[i] + ", ");
                tagString = tagString + newTag;
            }
            return tagString;
        }
    }
}
