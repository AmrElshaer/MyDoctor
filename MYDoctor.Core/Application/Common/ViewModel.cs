using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.Common
{
    public abstract class ViewModel<T>:BaseViewModel<T> where T:class
    {
        protected int NumberTake { get; }
        protected int? CategoryId { get; }
        protected ViewModel(int numberTake, T model, int? categoryId)
        {
            this.NumberTake = numberTake;
            this.CategoryId = categoryId;
            this.Model = model;

        }
        public ViewModel<T> Build() { return this; }
        public abstract ViewModel<T> WithRelativeCategory(IRelativeCategoryHelper relativeCategoryHelper);
        public abstract ViewModel<T> WithMedicin(IMedicinHelper medicinHelper);
        public abstract ViewModel<T> WithDisease(IDiseaseHelper diseaseHelper);
        public abstract ViewModel<T> WithPosts(IPostHelper postHelper);
        public abstract ViewModel<T> WithDoctors(IDoctorHelper doctorHelper);
        public abstract ViewModel<T> WithCategories(ICategoryHelper categoryHelper);
    }
}
