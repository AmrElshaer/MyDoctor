using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace MYDoctor.Infrastructure.Helper
{
    #region CategorySearch
    public sealed class CategorySearchHint : Specification<BeatyandHealthy>
    {
        private readonly SearchHintBase searchHintBase;
        public CategorySearchHint(SearchParamter searchParamter)
        {
            searchHintBase = new SearchHintBase(searchParamter);
        }



        public override Expression<Func<BeatyandHealthy, bool>> ToExpression()
        {
            return x => searchHintBase.ApplySearch(x.Category, x.CreateDate);
        }
    }
    #endregion
    #region CategoryRelativeSearchHint
    public sealed class CategoryRelativeSearchHint : Specification<RelativeofBeatyandhealthy>
    {
        private readonly SearchHintBase searchHintBase;
        public CategoryRelativeSearchHint(SearchParamter searchParamter)
        {
            searchHintBase = new SearchHintBase(searchParamter);
        }

        public override Expression<Func<RelativeofBeatyandhealthy, bool>> ToExpression()
        {
            var relatedId =searchHintBase.RelatedIdPredicate();
            return x => searchHintBase.ApplySearch(x.Address, x.CreateDate) && relatedId(x.BeatyandHealthy.Id);
        }
    }
    #endregion
    #region DiseaseSearchHint
    public sealed class DiseaseSearchHint : Specification<Disease>
    {
        private readonly SearchHintBase searchHintBase;
        public DiseaseSearchHint(SearchParamter searchParamter)
        {
            searchHintBase = new SearchHintBase(searchParamter);
        }



        public override Expression<Func<Disease, bool>> ToExpression()
        {
            return x => searchHintBase.ApplySearch(x.DiseaseName, x.CreateDate);
        }
    }
    #endregion
    #region DoctorSearchHint
    public sealed class DoctorSearchHint : Specification<Doctor>
    {
        private readonly SearchHintBase searchHintBase;

        public DoctorSearchHint(SearchParamter searchParamter)
        {
            searchHintBase = new SearchHintBase(searchParamter);
        }
        public override Expression<Func<Doctor, bool>> ToExpression()
        {
            var relatedId =searchHintBase.RelatedIdPredicate();
            var searchQuery = searchHintBase.SearchQuery();
            return x => searchQuery(x.Name) && relatedId(x.Category.Id);
        }
    }
    #endregion
    #region MedicinSearchHint
    public sealed class MedicinSearchHint : Specification<Medicin>
    {
        private readonly SearchHintBase searchHintBase;
        public MedicinSearchHint(SearchParamter searchParamter)
        {
            searchHintBase = new SearchHintBase(searchParamter);
        }



        public override Expression<Func<Medicin, bool>> ToExpression()
        {
            return x => searchHintBase.ApplySearch(x.Name, x.CreateDate);
        }
    } 
    #endregion
    internal class SearchHintBase
    {
        private readonly SearchParamter searchParamter;
        public SearchHintBase(SearchParamter searchParamter)
        {
           this.searchParamter = searchParamter;
        }

        
        public bool ApplySearch(string searchVal, DateTime? createDate)
        {
            var searchQuery = SearchQuery();
            var fromDate = FromPredicate();
            var toDate = ToPredicate();
            return searchQuery(searchVal) && fromDate(createDate) && toDate(createDate);
        }
        public Func<DateTime?, bool> FromPredicate()
        {
            return (createDate) => (!searchParamter.CreateFrom.HasValue || createDate >= searchParamter.CreateFrom);
        }

        public Func<string, bool> SearchQuery()
        {
            return x => (string.IsNullOrEmpty(this.searchParamter.SearchQuery) || x.ToLower().Contains(searchParamter.SearchQuery.ToLower()));
        }
        public Func<DateTime?, bool> ToPredicate()
        {
            return (createDate) => (!searchParamter.CreateTo.HasValue || createDate <= searchParamter.CreateTo);
        }
        public Func<int?, bool> RelatedIdPredicate()
        {
            return x => (!this.searchParamter.IdRelated.HasValue || x == searchParamter.IdRelated);
        }
    }
}