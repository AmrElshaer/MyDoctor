using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Repository
{
    internal class DisLikeRepository:BaseRepository<DisLike>,IDisLikeRepository
    {
        public DisLikeRepository(ApplicationDbContext context) : base(context)
        {
            
        }
        public async Task<LikeDisLikeNumViewModel> AddDisLike(int postId, int userId)
        {
            var like = await _context.Likes.FirstOrDefaultAsync(a => a.UserProfileId == userId && a.PostId == postId);
            var disLike = await GetFirstAsync(l => l.UserProfileId == userId && l.PostId == postId);
            var post = await _context.Posts.Include(p => p.DisLikes).FirstOrDefaultAsync(a => a.Id == postId);
            if (like != null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }

            if (disLike == null)
                await InsertAsync(new DisLike() { UserProfileId = userId, PostId = postId });

            return new LikeDisLikeNumViewModel() { LikeNumber = post.Likes.Count(), DisLikeNumber = post.DisLikes.Count() };
        }
    }
}
