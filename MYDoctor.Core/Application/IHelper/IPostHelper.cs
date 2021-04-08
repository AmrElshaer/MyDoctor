using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IHelper
{
    public  interface IPostHelper
    {
        Task<IEnumerable<Post>> GetRelativesPosts(ICollection<Post> posts, int numberRelated,int categoryId);
    }
}
