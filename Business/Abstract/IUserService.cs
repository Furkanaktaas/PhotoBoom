using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        User GetUserById(int userId);
    }
}
