
namespace Enterprise.Component.EntityFramework
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// True specification
    /// </summary>
    /// <typeparam name="TValueObject">Type of entity in this specification</typeparam>
    public sealed class TrueSpecification<TEntity>
        : Specification<TEntity>
        where TEntity : class
    {
        #region Specification overrides

        /// <summary>
        /// <see cref=" FX.Domain.Core.Specifications.Specification{TValueObject}"/>
        /// </summary>
        /// <returns><see cref=" FX.Domain.Core.Specifications.Specification{TValueObject}"/></returns>
        public override System.Linq.Expressions.Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            bool result = true;

            Expression<Func<TEntity, bool>> trueExpression = t => result;
            return trueExpression;
        }

        #endregion
    }
}
