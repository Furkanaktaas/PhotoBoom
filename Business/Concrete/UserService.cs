using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        private IUserDal _userDal;
        public UserService(IUserDal userDal)
        {
           _userDal = userDal;
        }

        public User GetUserById(int userId)
        {
            return _userDal.Get(u => u.UserId == userId);
        }

    }
}
