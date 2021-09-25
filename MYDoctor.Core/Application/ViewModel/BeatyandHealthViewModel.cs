using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;

namespace MYDoctor.Core.Application.ViewModel
{
    public class BeatyandHealthViewModel: ViewModel<BeatyandHealthy>
    {
        

        public BeatyandHealthViewModel(BeatyandHealthy beatyandHealthy, int numberTake,int categoryId)
            :base(numberTake,beatyandHealthy,categoryId)
        {
            
        }
        public override ViewModel<BeatyandHealthy>  WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper)
        {
            this.RelativeCategories = relativeCategoryHelper.GetRelativesCategory(this.Model.RelativeofBeatyandhealthies,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<BeatyandHealthy> WithMedicin(IMedicinHelper medicinHelper)
        {
            this.Medicins = medicinHelper.GetRelativesMedicins(this.Model.Medicins,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<BeatyandHealthy> WithDisease(IDiseaseHelper diseaseHelper)
        {
            this.Diseases = diseaseHelper.GetRelativesDiseases(this.Model.Diseases,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<BeatyandHealthy> WithPosts(IPostHelper postHelper)
        {
            this.Posts = postHelper.GetRelativesPosts(this.Model.Posts,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<BeatyandHealthy> WithDoctors(IDoctorHelper doctorHelper)
        {
            this.Doctors = doctorHelper.GetRelativesDoctors(this.Model.Doctors,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<BeatyandHealthy> WithCategories(ICategoryHelper categoryHelper)
        {
            this.Categories = categoryHelper.GetRelativesCategories(this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
    }
}
