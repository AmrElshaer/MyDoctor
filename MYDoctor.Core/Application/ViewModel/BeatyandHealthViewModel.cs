using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;

namespace MYDoctor.Core.Application.ViewModel
{
    public class BeatyandHealthViewModel:BaseViewModel
    {
        public BeatyandHealthy BeatyandHealthy { get;private set; }
        private readonly int _numberTake;
        public BeatyandHealthViewModel(BeatyandHealthy beatyandHealthy, int numberTake)
        {
            this.BeatyandHealthy = beatyandHealthy;
            this._numberTake = numberTake;
        }
        public BeatyandHealthViewModel WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper, int categoryId)
        {
            this.RelativeCategories = relativeCategoryHelper.GetRelativesCategory(this.BeatyandHealthy.RelativeofBeatyandhealthies,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public BeatyandHealthViewModel WithMedicin(IMedicinHelper medicinHelper, int categoryId)
        {
            this.Medicins = medicinHelper.GetRelativesMedicins(this.BeatyandHealthy.Medicins,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public BeatyandHealthViewModel WithDisease(IDiseaseHelper diseaseHelper, int categoryId)
        {
            this.Diseases = diseaseHelper.GetRelativesDiseases(this.BeatyandHealthy.Diseases,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public BeatyandHealthViewModel WithPosts(IPostHelper postHelper, int categoryId)
        {
            this.Posts = postHelper.GetRelativesPosts(this.BeatyandHealthy.Posts,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public BeatyandHealthViewModel WithDoctors(IDoctorHelper doctorHelper, int categoryId)
        {
            this.Doctors = doctorHelper.GetRelativesDoctors(this.BeatyandHealthy.Doctors,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public BeatyandHealthViewModel WithCategories(ICategoryHelper categoryHelper, int categoryId)
        {
            this.Categories = categoryHelper.GetRelativesCategories(this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public BeatyandHealthViewModel Build() => this;
    }
}
