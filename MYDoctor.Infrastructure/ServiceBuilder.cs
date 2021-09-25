using MYDoctor.Core.Application;
using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.IHelper;

namespace MYDoctor.Infrastructure
{
    /// <summary>
    /// concrete factory
    /// </summary>
    public class ServiceBuilder : IServiceBuilder
    {
        private readonly IDoctorHelper _doctorHelper;
        private readonly IDiseaseHelper _diseaseHelper;
        private readonly IPostHelper _postHelper;
        private readonly IMedicinHelper _medicinHelper;
        private readonly IRelativeCategoryHelper _relativeCategoryHelper;
        private readonly ICategoryHelper _categoryHelper;

        public ServiceBuilder(IMedicinHelper medicinHelper, IPostHelper postHelper, IRelativeCategoryHelper relativeCategoryHelper
            , IDiseaseHelper diseaseHelper, IDoctorHelper doctorHelper, ICategoryHelper categoryHelper)
        {
            _doctorHelper = doctorHelper;
            _diseaseHelper = diseaseHelper;
            _postHelper = postHelper;
            _relativeCategoryHelper = relativeCategoryHelper;
            _medicinHelper = medicinHelper;
            _categoryHelper = categoryHelper;
        }

        public BaseViewModel<T> BuildViewModel<T>(ViewModel<T> viewModel) where T : class
        {
            return viewModel
               .WithMedicin(this._medicinHelper)
               .WithRelativeCategory(this._relativeCategoryHelper)
               .WithDisease(this._diseaseHelper)
               .WithDoctors(this._doctorHelper)
               .WithPosts(this._postHelper)
               .WithCategories(this._categoryHelper).Build();
        }
    }
}