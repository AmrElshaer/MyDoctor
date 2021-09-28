using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
using System.Collections.Generic;
namespace MYDoctor.Core.Application.ViewModel
{
    public class MedicinViewModel:ViewModel<Medicin>
    {
        
        public MedicinSearch MedicinSearch { get; set; }
        public MedicinViewModel(IEnumerable<Medicin> medicins, MedicinSearch medicinSearch)
            :base(0,null,null)
        {
            this.Medicins = medicins;
            this.MedicinSearch = medicinSearch;
        }
       
        public MedicinViewModel(Medicin medicin, int numberTake):base(numberTake,medicin, medicin?.BeatyandHealthyId)
        {
        }
        public override ViewModel<Medicin> WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper)
        {
            this.RelativeCategories = relativeCategoryHelper.GetRelativesCategory(Model.BeatyandHealthy.RelativeofBeatyandhealthies,
                NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Medicin> WithMedicin(IMedicinHelper medicinHelper)
        {
            this.Medicins = medicinHelper.GetRelativesMedicins(Model.BeatyandHealthy.Medicins,
                NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Medicin> WithDisease(IDiseaseHelper diseaseHelper)
        {
            this.Diseases = diseaseHelper.GetRelativesDiseases(Model.BeatyandHealthy.Diseases,
                NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Medicin> WithPosts(IPostHelper postHelper)
        {
            this.Posts = postHelper.GetRelativesPosts(Model.BeatyandHealthy.Posts,
                NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Medicin> WithDoctors(IDoctorHelper doctorHelper)
        {
            this.Doctors = doctorHelper.GetRelativesDoctors(Model.BeatyandHealthy.Doctors,
                NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<Medicin> WithCategories(ICategoryHelper categoryHelper)
        {
            this.Categories = categoryHelper.GetRelativesCategories(NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
    }
}
