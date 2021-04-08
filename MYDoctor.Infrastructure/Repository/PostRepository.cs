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
        private readonly ICategoryHelper _categoryHelper;

        public PostRepository(ApplicationDbContext context, IMedicinHelper medicinHelper, IPostHelper postHelper, IRelativeCategoryHelper relativeCategoryHelper, IDiseaseHelper diseaseHelper, IDoctorHelper doctorHelper, ICategoryHelper categoryHelper) : base(context)
        {
            _doctorHelper = doctorHelper;
            _diseaseHelper = diseaseHelper;
            _postHelper = postHelper;
            _relativeCategoryHelper = relativeCategoryHelper;
            _medicinHelper = medicinHelper;
            _categoryHelper = categoryHelper;
        }
        public async Task<PostViewModel> GetPostAsync(int id, int numberRelated)
        {
            var post = await GetFirstAsync(p => p.Id == id, p => p.Category,p=>p.Category.Doctors,p=>p.Category.Medicins,p=>p.Category.Diseases,p => p.User);
            var categoryId = post.CategoryId;
            var result = new PostViewModel(post, numberRelated)
                .WithMedicin(this._medicinHelper, categoryId)
                .WithRelativeCategory(this._relativeCategoryHelper, categoryId)
                .WithDisease(this._diseaseHelper, categoryId)
                .WithDoctors(this._doctorHelper, categoryId)
                .WithPosts(this._postHelper, categoryId)
                .WithCategories(this._categoryHelper, categoryId).Build();
            return result;
        }

    }
}
