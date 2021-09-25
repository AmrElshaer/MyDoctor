using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MYDoctor.Core.Application.ViewModel
{
    public  class RelativeBeatyandhealthyViewModel : ViewModel<RelativeofBeatyandhealthy>
    {
  
        public RelativeBeatyandhealthyViewModel(RelativeofBeatyandhealthy relativeofBeatyandhealthy,int numberTake,int? categoryId)
            :base(numberTake,relativeofBeatyandhealthy, categoryId)
        {
          
        }
        public override  ViewModel<RelativeofBeatyandhealthy> WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper)
        {
           this.RelativeCategories= relativeCategoryHelper.GetRelativesCategory(this.Model.BeatyandHealthy.RelativeofBeatyandhealthies,
               this.NumberTake, this.CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<RelativeofBeatyandhealthy> WithMedicin(IMedicinHelper medicinHelper)
        {
            this.Medicins = medicinHelper.GetRelativesMedicins(this.Model.BeatyandHealthy.Medicins,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<RelativeofBeatyandhealthy> WithDisease(IDiseaseHelper diseaseHelper)
        {
            this.Diseases = diseaseHelper.GetRelativesDiseases(this.Model.BeatyandHealthy.Diseases,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<RelativeofBeatyandhealthy> WithPosts(IPostHelper postHelper)
        {
            this.Posts = postHelper.GetRelativesPosts(this.Model.BeatyandHealthy.Posts,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<RelativeofBeatyandhealthy> WithDoctors(IDoctorHelper doctorHelper)
        {
            this.Doctors = doctorHelper.GetRelativesDoctors(this.Model.BeatyandHealthy.Doctors,
                this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
        public override ViewModel<RelativeofBeatyandhealthy> WithCategories(ICategoryHelper  categoryHelper)
        {
            this.Categories = categoryHelper.GetRelativesCategories(this.NumberTake, CategoryId).GetAwaiter().GetResult();
            return this;
        }
    }
}
