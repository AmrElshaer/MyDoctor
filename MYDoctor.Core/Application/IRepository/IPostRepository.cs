using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IRepository
{
    public interface IPostRepository:IRepository<Post>
    {
        Task<PostViewModel> GetPostAsync(int id, int numberRelated);
    }
}
