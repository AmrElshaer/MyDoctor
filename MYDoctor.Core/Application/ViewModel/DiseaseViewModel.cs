using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
namespace MYDoctor.Core.Application.ViewModel
{
    public class DiseaseViewModel:BaseViewModel
    {
        public Disease Disease { get;private set; }
        private readonly int _numberTake;
        public DiseaseViewModel(Disease disease, int numberTake)
        {
            this.Disease = disease;
            this._numberTake = numberTake;
        }
        public DiseaseViewModel WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper, int categoryId)
        {
            this.RelativeCategories = relativeCategoryHelper.GetRelativesCategory(this.Disease.BeatyandHealthy.RelativeofBeatyandhealthies,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public DiseaseViewModel WithMedicin(IMedicinHelper medicinHelper, int categoryId)
        {
            this.Medicins = medicinHelper.GetRelativesMedicins(this.Disease.BeatyandHealthy.Medicins,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public DiseaseViewModel WithDisease(IDiseaseHelper diseaseHelper, int categoryId)
        {
            this.Diseases = diseaseHelper.GetRelativesDiseases(this.Disease.BeatyandHealthy.Diseases,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public DiseaseViewModel WithPosts(IPostHelper postHelper, int categoryId)
        {
            this.Posts = postHelper.GetRelativesPosts(this.Disease.BeatyandHealthy.Posts,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public DiseaseViewModel WithDoctors(IDoctorHelper doctorHelper, int categoryId)
        {
            this.Doctors = doctorHelper.GetRelativesDoctors(this.Disease.BeatyandHealthy.Doctors,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public DiseaseViewModel WithCategories(ICategoryHelper categoryHelper, int categoryId)
        {
            this.Categories = categoryHelper.GetRelativesCategories(this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public DiseaseViewModel Build() => this;
    }
}
