using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
namespace MYDoctor.Core.Application.ViewModel
{
    public class DiseaseViewModel:ViewModel<Disease>
    {
        

        public DiseaseViewModel(int categoryId,Disease disease, int numberTake):base(numberTake,disease,categoryId)
        {
            
        }
        public override ViewModel<Disease> WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper)
        {
            this.RelativeCategories = relativeCategoryHelper.GetRelativesCategory(this.Model.BeatyandHealthy.RelativeofBeatyandhealthies,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Disease> WithMedicin(IMedicinHelper medicinHelper)
        {
            this.Medicins = medicinHelper.GetRelativesMedicins(this.Model.BeatyandHealthy.Medicins,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Disease> WithDisease(IDiseaseHelper diseaseHelper)
        {
            this.Diseases = diseaseHelper.GetRelativesDiseases(this.Model.BeatyandHealthy.Diseases,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Disease> WithPosts(IPostHelper postHelper)
        {
            this.Posts = postHelper.GetRelativesPosts(this.Model.BeatyandHealthy.Posts,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Disease> WithDoctors(IDoctorHelper doctorHelper)
        {
            this.Doctors = doctorHelper.GetRelativesDoctors(this.Model.BeatyandHealthy.Doctors,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Disease> WithCategories(ICategoryHelper categoryHelper)
        {
            this.Categories = categoryHelper.GetRelativesCategories(this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
    }
}
