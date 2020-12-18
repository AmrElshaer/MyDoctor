using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System.Linq;
using System.Threading.Tasks;
using MYDoctor.Core.Application.IHelper;

namespace MYDoctor.Infrastructure.Repository
{
    internal class PostRepository:BaseRepository<Post>,IPostRepository
    {
        private readonly IDoctorHelper _doctorHelper;
        private readonly IDiseaseHelper _diseaseHelper;
        private readonly IPostHelper _postHelper;
        private readonly IMedicinHelper _medicinHelper;
        private readonly IRelativeCategoryHelper _relativeCategoryHelper;
        public PostRepository(ApplicationDbContext context, IMedicinHelper medicinHelper, IPostHelper postHelper, IRelativeCategoryHelper relativeCategoryHelper, IDiseaseHelper diseaseHelper, IDoctorHelper doctorHelper) :base(context)
        {
            _doctorHelper = doctorHelper;
            _diseaseHelper = diseaseHelper;
            _postHelper = postHelper;
            _relativeCategoryHelper = relativeCategoryHelper;
            _medicinHelper = medicinHelper;
        }
        public async Task<PostViewModel> GetPostAsync(int id, int numberRelated)
        {
            var post = await GetFirstAsync(p => p.Id == id, p => p.Category,p=>p.Category.Doctors,p=>p.Category.Medicins,p=>p.Category.Diseases,p => p.User);

            var result = new PostViewModel()
            {
                Post = post,
                Categories = await _context.BeatyandHealthy.Where(a => a.Id != post.CategoryId).OrderByDescending(a => a.Id).Take(numberRelated).ToListAsync(),
                Doctors = await _doctorHelper.GetRelativesDoctors(post.Category.Doctors, numberRelated, d => d.CategoryId != post.CategoryId),
                Medicins = await _medicinHelper.GetRelativesMedicins(post.Category.Medicins, numberRelated, d => d.BeatyandHealthyId != post.CategoryId),
                Diseases = await _diseaseHelper.GetRelativesDiseases(post.Category.Diseases, numberRelated, d => d.BeatyandHealthyId != post.CategoryId),
                RelativeCategories = await _relativeCategoryHelper.GetRelativesCategory(post.Category.RelativeofBeatyandhealthies, numberRelated, d => d.BeatyandHealthId != post.CategoryId),
                Posts = await _postHelper.GetRelativesPosts(post.Category.Posts, numberRelated, p => p.CategoryId != post.CategoryId)
            };
            return result;
        }

    }
}
