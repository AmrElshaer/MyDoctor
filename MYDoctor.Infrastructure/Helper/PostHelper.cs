using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYDoctor.Core.Application.Common;
using MYDoctor.Infrastructure.Identity;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MYDoctor.Infrastructure.Helper
{
    internal  class PostHelper:IPostHelper
    {
        private readonly ApplicationDbContext _context;

        public PostHelper(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Post>> GetRelativesPosts(ICollection<Post> posts, int numberRelated, int? categoryId)
        {
            if ((posts.Any(), posts.Count() >= numberRelated) == (true, true))
                return posts;
            var pos = await _context.Posts.Include(d => d.Category).Include(p => p.User).Where(a=>a.CategoryId!=categoryId).OrderByDescending(c => c.Id).Take(numberRelated - posts.Count()).ToListAsync();
            return posts.AppendData(pos);
        }
        public async Task<IEnumerable<Post>> GetRelativesPosts(int numberRelated)
        {
           return await _context.Posts.Include(p => p.Likes).Include(p => p.DisLikes).Include(p => p.Category)
                .Include(p => p.User).OrderByDescending(a => a.Id).Take(numberRelated).ToListAsync();
        }
    }
}
