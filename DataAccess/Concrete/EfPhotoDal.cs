using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete
{
    public class EfPhotoDal : EfRepositoryBase<Photo, PhotoBoomContext>, IPhotoDal
    {
        public IList<Photo> GetListWithUser(Expression<Func<Photo, bool>> filter = null)
        {
            using (var context = new PhotoBoomContext())
            {
                return filter == null
                    ? context.Set<Photo>().Include(x=> x.User).ToList()
                    : context.Set<Photo>().Where(filter).Include(x => x.User).ToList();
            }
        }

    }
}
