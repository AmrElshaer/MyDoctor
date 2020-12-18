using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Repository
{
    internal  class LikeRepository:BaseRepository<Like>,ILikeRepository
    {
        public LikeRepository(ApplicationDbContext context):base(context)
        {

        }

        public async Task<LikeDisLikeNumViewModel> AddLike(int postId, int userId)
        {
            // check that user not add dislike
            var disLike =await _context.DisLikes.FirstOrDefaultAsync(a=>a.UserProfileId==userId&&a.PostId==postId);
            var like = await GetFirstAsync(l => l.UserProfileId == userId && l.PostId == postId);
            var post = await _context.Posts.Include(p => p.Likes).Include(p=>p.DisLikes).FirstOrDefaultAsync(a => a.Id == postId);
            if (disLike != null)
            { 
                 _context.DisLikes.Remove(disLike);
                await _context.SaveChangesAsync();
            }
            
            if (like==null)
                  await InsertAsync(new Like() {UserProfileId=userId,PostId=postId });
            
            return new LikeDisLikeNumViewModel() {LikeNumber=post.Likes.Count(),DisLikeNumber=post.DisLikes.Count() };
        }
    }
}
