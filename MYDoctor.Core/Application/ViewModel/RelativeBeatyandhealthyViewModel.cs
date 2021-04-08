using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MYDoctor.Core.Application.ViewModel
{
    public  class RelativeBeatyandhealthyViewModel : BaseViewModel
    {
        private readonly int _numberTake;
        public RelativeofBeatyandhealthy  RelativeofBeatyandhealthy { get;private set; }
        public RelativeBeatyandhealthyViewModel(RelativeofBeatyandhealthy relativeofBeatyandhealthy,int numberTake)
        {
            this.RelativeofBeatyandhealthy = relativeofBeatyandhealthy;
            this._numberTake = numberTake;
        }
        public  RelativeBeatyandhealthyViewModel WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper, int categoryId)
        {
           this.RelativeCategories= relativeCategoryHelper.GetRelativesCategory(this.RelativeofBeatyandhealthy.BeatyandHealthy.RelativeofBeatyandhealthies,
               this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public RelativeBeatyandhealthyViewModel WithMedicin(IMedicinHelper medicinHelper, int categoryId)
        {
            this.Medicins = medicinHelper.GetRelativesMedicins(this.RelativeofBeatyandhealthy.BeatyandHealthy.Medicins,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public RelativeBeatyandhealthyViewModel WithDisease(IDiseaseHelper diseaseHelper, int categoryId)
        {
            this.Diseases = diseaseHelper.GetRelativesDiseases(this.RelativeofBeatyandhealthy.BeatyandHealthy.Diseases,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public RelativeBeatyandhealthyViewModel WithPosts(IPostHelper postHelper,int categoryId)
        {
            this.Posts = postHelper.GetRelativesPosts(this.RelativeofBeatyandhealthy.BeatyandHealthy.Posts,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public RelativeBeatyandhealthyViewModel WithDoctors(IDoctorHelper doctorHelper,int categoryId)
        {
            this.Doctors = doctorHelper.GetRelativesDoctors(this.RelativeofBeatyandhealthy.BeatyandHealthy.Doctors,
                this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public RelativeBeatyandhealthyViewModel WithCategories(ICategoryHelper  categoryHelper, int categoryId)
        {
            this.Categories = categoryHelper.GetRelativesCategories(this._numberTake, categoryId).GetAwaiter().GetResult();
            return this;
        }
        public RelativeBeatyandhealthyViewModel Build() => this;
    }
}
