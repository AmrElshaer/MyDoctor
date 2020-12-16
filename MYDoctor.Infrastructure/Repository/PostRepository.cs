using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;

namespace MYDoctor.Infrastructure.Repository
{
    internal class PostRepository:BaseRepository<Post>,IPostRepository
    {
        private readonly IDoctorHelper _doctorHelper;

        public PostRepository(ApplicationDbContext context,IDoctorHelper doctorHelper):base(context)
        {
            _doctorHelper = doctorHelper;
        }
        public async Task<PostViewModel> GetPostAsync(int id, int numberRelated)
        {
            var post = await GetFirstAsync(p => p.Id == id, p => p.Category,p=>p.Category.Doctors,p=>p.Category.Medicins,p=>p.Category.Diseases,p => p.User);

            var result = new PostViewModel()
            {
                Post = post,
                Categories = await _context.BeatyandHealthy.Where(a => a.Id != post.CategoryId).OrderByDescending(a => a.Id).Take(numberRelated).ToListAsync(),
                Doctors = await _doctorHelper.GetRelativesDoctors(post.Category.Doctors, numberRelated, d => d.CategoryId != post.CategoryId),
                Medicins = await GetRelativesMedicins(post, numberRelated),
                Diseases = await GetRelativesDiseases(post, numberRelated),
                RelativeCategories = await GetRelativesCategories(post, numberRelated),
                Posts=await _context.Posts.Include(p=>p.Category).Include(p=>p.User).Where(p=>p.CategoryId==post.CategoryId).ToListAsync()
            };
            return result;
        }
        private async Task<IEnumerable<Medicin>> GetRelativesMedicins(Post post, int numberRelated)
        {
            var relativeMedicin = post.Category.Medicins;
            if ((relativeMedicin.Any(), relativeMedicin.Count() >= numberRelated) == (true, true))
                return relativeMedicin;
            var medicins = await _context.Medicin.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthyId != post.CategoryId).Take(numberRelated - relativeMedicin.Count()).ToListAsync();
            return relativeMedicin.AppendData(medicins);
        }
        private async Task<IEnumerable<Disease>> GetRelativesDiseases(Post post, int numberRelated)
        {
            var relativeDisease = post.Category.Diseases;
            if ((relativeDisease.Any(), relativeDisease.Count() >= numberRelated) == (true, true))
                return relativeDisease;
            var medicins = await _context.Disease.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthyId != post.CategoryId).Take(numberRelated - relativeDisease.Count()).ToListAsync();
            return relativeDisease.AppendData(medicins);
        }
        private async Task<IEnumerable<RelativeofBeatyandhealthy>> GetRelativesCategories(Post post, int numberRelated)
        {
            var relativeCategories = post.Category.RelativeofBeatyandhealthies;
            if ((relativeCategories.Any(), relativeCategories.Count() >= numberRelated) == (true, true))
                return relativeCategories;
            var medicins = await _context.RelativeofBeatyandhealthy.Include(d => d.BeatyandHealthy).Where(d => d.BeatyandHealthId != post.CategoryId).Take(numberRelated - relativeCategories.Count()).ToListAsync();
            return relativeCategories.AppendData(medicins);
        }

    }
}
