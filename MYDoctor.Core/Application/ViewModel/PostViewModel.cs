using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
namespace MYDoctor.Core.Application.ViewModel
{
    public class PostViewModel:BaseViewModel
    {
        public Post Post { get; private set; }
        private readonly int _numberTake;
        public PostViewModel(Post post, int numberTake)
        {
            this.Post = post;
            this._numberTake = numberTake;
        }
        public PostViewModel WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper, int categoryId)
        {
            this.RelativeCategories = relativeCategoryHelper.GetRelativesCategory(this.Post.Category.RelativeofBeatyandhealthies,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public PostViewModel WithMedicin(IMedicinHelper medicinHelper, int categoryId)
        {
            this.Medicins = medicinHelper.GetRelativesMedicins(this.Post.Category.Medicins,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public PostViewModel WithDisease(IDiseaseHelper diseaseHelper, int categoryId)
        {
            this.Diseases = diseaseHelper.GetRelativesDiseases(this.Post.Category.Diseases,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public PostViewModel WithPosts(IPostHelper postHelper, int categoryId)
        {
            this.Posts = postHelper.GetRelativesPosts(this.Post.Category.Posts,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public PostViewModel WithDoctors(IDoctorHelper doctorHelper, int categoryId)
        {
            this.Doctors = doctorHelper.GetRelativesDoctors(this.Post.Category.Doctors,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public PostViewModel WithCategories(ICategoryHelper categoryHelper, int categoryId)
        {
            this.Categories = categoryHelper.GetRelativesCategories(this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public PostViewModel Build() => this;
    }
}
