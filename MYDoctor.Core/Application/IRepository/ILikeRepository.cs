using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IRepository
{
    public interface ILikeRepository : IRepository<Like>
    {
        Task<LikeDisLikeNumViewModel> AddLike(int postId, int userId);
    }
}
