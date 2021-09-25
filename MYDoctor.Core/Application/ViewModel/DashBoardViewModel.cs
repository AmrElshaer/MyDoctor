using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
using System.Collections.Generic;

namespace MYDoctor.Core.Application.ViewModel
{
    public class DashBoardViewModel : ViewModel<BeatyandHealthy>
    {
        public DashBoardViewModel(int nTake):base(nTake,null,null)
        {

        }
        public override ViewModel<BeatyandHealthy> WithCategories(ICategoryHelper categoryHelper)
        {
            this.Categories = categoryHelper.GetRelativesCategories(NumberTake).GetAwaiter().GetResult();
            return this;
        }

        public override ViewModel<BeatyandHealthy> WithDisease(IDiseaseHelper diseaseHelper)
        {
            this.Diseases = diseaseHelper.GetRelativesDiseases(NumberTake).GetAwaiter().GetResult();
            return this;
        }

        public override ViewModel<BeatyandHealthy> WithDoctors(IDoctorHelper doctorHelper)
        {
            this.Doctors = doctorHelper.GetRelativesDoctors(NumberTake).GetAwaiter().GetResult();
            return this;
        }

        public override ViewModel<BeatyandHealthy> WithMedicin(IMedicinHelper medicinHelper)
        {
            this.Medicins = medicinHelper.GetRelativesMedicins(NumberTake).GetAwaiter().GetResult();
            return this;
        }

        public override ViewModel<BeatyandHealthy> WithPosts(IPostHelper postHelper)
        {
            this.Posts = postHelper.GetRelativesPosts(NumberTake).GetAwaiter().GetResult();
            return this;

        }

        public override ViewModel<BeatyandHealthy> WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper)
        {
            this.RelativeCategories = relativeCategoryHelper.GetRelativesCategory(NumberTake).GetAwaiter().GetResult();
            return this;
        }
    }
}
