using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
namespace MYDoctor.Core.Application.ViewModel
{
    public class PostViewModel:ViewModel<Post>
    {
        public PostViewModel(Post post, int numberTake,int categoryId):base(numberTake,post,categoryId)
        {
           
        }
        public override ViewModel<Post> WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper)
        {
            this.RelativeCategories = relativeCategoryHelper.GetRelativesCategory(Model.Category.RelativeofBeatyandhealthies,
                NumberTake,CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Post> WithMedicin(IMedicinHelper medicinHelper)
        {
            this.Medicins = medicinHelper.GetRelativesMedicins(Model.Category.Medicins,
               NumberTake,CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Post> WithDisease(IDiseaseHelper diseaseHelper)
        {
            this.Diseases = diseaseHelper.GetRelativesDiseases(Model.Category.Diseases,
               NumberTake,CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Post> WithPosts(IPostHelper postHelper)
        {
            this.Posts = postHelper.GetRelativesPosts(Model.Category.Posts,
               NumberTake,CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Post> WithDoctors(IDoctorHelper doctorHelper)
        {
            this.Doctors = doctorHelper.GetRelativesDoctors(Model.Category.Doctors,
               NumberTake,CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Post> WithCategories(ICategoryHelper categoryHelper)
        {
            this.Categories = categoryHelper.GetRelativesCategories(NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
       
    }
}
