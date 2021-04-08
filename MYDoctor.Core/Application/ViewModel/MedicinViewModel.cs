using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
using System.Collections.Generic;
namespace MYDoctor.Core.Application.ViewModel
{
    public class MedicinViewModel:BaseViewModel
    {
        public Medicin Medicin { get; private set; }
        public MedicinSearch MedicinSearch { get; set; }
        private readonly int _numberTake;
        public MedicinViewModel(IEnumerable<Medicin> medicins, MedicinSearch medicinSearch)
        {
            this.Medicins = medicins;
            this.MedicinSearch = medicinSearch;
        }
       
        public MedicinViewModel(Medicin medicin, int numberTake)
        {
            this.Medicin = medicin;
            this._numberTake = numberTake;
        }
        public MedicinViewModel WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper, int categoryId)
        {
            this.RelativeCategories = relativeCategoryHelper.GetRelativesCategory(this.Medicin.BeatyandHealthy.RelativeofBeatyandhealthies,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public MedicinViewModel WithMedicin(IMedicinHelper medicinHelper, int categoryId)
        {
            this.Medicins = medicinHelper.GetRelativesMedicins(this.Medicin.BeatyandHealthy.Medicins,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public MedicinViewModel WithDisease(IDiseaseHelper diseaseHelper, int categoryId)
        {
            this.Diseases = diseaseHelper.GetRelativesDiseases(this.Medicin.BeatyandHealthy.Diseases,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public MedicinViewModel WithPosts(IPostHelper postHelper, int categoryId)
        {
            this.Posts = postHelper.GetRelativesPosts(this.Medicin.BeatyandHealthy.Posts,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public MedicinViewModel WithDoctors(IDoctorHelper doctorHelper, int categoryId)
        {
            this.Doctors = doctorHelper.GetRelativesDoctors(this.Medicin.BeatyandHealthy.Doctors,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public MedicinViewModel WithCategories(ICategoryHelper categoryHelper, int categoryId)
        {
            this.Categories = categoryHelper.GetRelativesCategories(this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public MedicinViewModel Build() => this;
    }
}
