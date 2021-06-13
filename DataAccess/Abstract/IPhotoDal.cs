using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IPhotoDal : IEntityRepository<Photo>
    {
        IList<Photo> GetListWithUser(Expression<Func<Photo, bool>> filter = null);
    }
}
