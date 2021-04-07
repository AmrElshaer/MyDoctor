using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MYDoctor.Infrastructure.Helper
{
    //specification design pattern
    public abstract class Specification<T>
    {
        // for inmemory
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }
        // for sql linq
        public abstract Expression<Func<T, bool>> ToExpression();
    }
    
    
}
