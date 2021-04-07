using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MYDoctor.Infrastructure.Helper
{
    //specification design pattern
    public abstract class Specification<T>
    {
        public static readonly Specification<T> All = new IdentitySpecification<T>();
        // for inmemory
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }
        // for sql linq
        public abstract Expression<Func<T, bool>> ToExpression();
        public Specification<T> And(Specification<T> specification)
        {
            if (All==this)
            {
                return specification;
            }
            if (All == specification)
            {
                return this;
            }
            return new AndSpecification<T>(this,specification);
        }
        public Specification<T> Or(Specification<T> specification)
        {
            if (All==this||specification==All)
            {
                return All;
            }
            return new OrSpecification<T>(this, specification);
        }
        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }
    public class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }
    internal sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> left;
        private readonly Specification<T> right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            this.left = left;
            this.right = right;
        }
        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = this.left.ToExpression();
            var rightExpression = this.right.ToExpression();
            var andExpression = Expression.AndAlso(leftExpression.Body,rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }
    internal sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> left;
        private readonly Specification<T> right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            this.left = left;
            this.right = right;
        }
        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = this.left.ToExpression();
            var rightExpression = this.right.ToExpression();
            var andExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }
    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> specification;
        public NotSpecification(Specification<T> specification)
        {
            this.specification = specification;
           
        }
        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression = this.specification.ToExpression();
            var notExpression = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }
}
