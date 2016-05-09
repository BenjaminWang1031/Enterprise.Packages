using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Enterprise.Core.Interface.Data;


namespace Enterprise.Core.Interface.Data.Specification
{
    public abstract class Specification<TEntityRoot>
     : ISpecification<TEntityRoot>
     where TEntityRoot : class
    {
        #region ISpecification<TValueObject> Members


        public abstract Expression<Func<TEntityRoot, bool>> SatisfiedBy();

        public ISpecification<TEntityRoot> And(ISpecification<TEntityRoot> other)
        {
            return new AndSpecification<TEntityRoot>(this, other);
        }

        public ISpecification<TEntityRoot> Or(ISpecification<TEntityRoot> other)
        {
            return new OrSpecification<TEntityRoot>(this, other);
        }

        public ISpecification<TEntityRoot> Not()
        {
            return new NotSpecification<TEntityRoot>(this);
        }

        #endregion


        public static ISpecification<TEntityRoot> Create(Expression<Func<TEntityRoot, bool>> expression)
        {
            return new DirectSpecification<TEntityRoot>(expression);
        }

        #region Override Operators

        /// <summary>
        ///  And operator
        /// </summary>
        /// <param name="leftSideSpecification">left operand in this AND operation</param>
        /// <param name="rightSideSpecification">right operand in this AND operation</param>
        /// <returns>New specification</returns>
        public static ISpecification<TEntityRoot> operator &(Specification<TEntityRoot> leftSideSpecification, Specification<TEntityRoot> rightSideSpecification)
        {
            return new AndSpecification<TEntityRoot>(leftSideSpecification, rightSideSpecification);
        }

        ///// <summary>
        ///// Or operator
        ///// </summary>
        ///// <param name="leftSideSpecification">left operand in this OR operation</param>
        ///// <param name="rightSideSpecification">left operand in this OR operation</param>
        ///// <returns>New specification </returns>
        public static ISpecification<TEntityRoot> operator |(Specification<TEntityRoot> leftSideSpecification, Specification<TEntityRoot> rightSideSpecification)
        {
            return new OrSpecification<TEntityRoot>(leftSideSpecification, rightSideSpecification);
        }

        ///// <summary>
        ///// Not specification
        ///// </summary>
        ///// <param name="specification">Specification to negate</param>
        ///// <returns>New specification</returns>
        public static ISpecification<TEntityRoot> operator !(Specification<TEntityRoot> specification)
        {
            return new NotSpecification<TEntityRoot>(specification);
        }

        /// <summary>
        /// Override operator false, only for support AND OR operators
        /// </summary>
        /// <param name="specification">Specification instance</param>
        /// <returns>See False operator in C#</returns>
        public static bool operator false(Specification<TEntityRoot> specification)
        {
            return false;
        }

        /// <summary>
        /// Override operator True, only for support AND OR operators
        /// </summary>
        /// <param name="specification">Specification instance</param>
        /// <returns>See True operator in C#</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "specification")]
        public static bool operator true(Specification<TEntityRoot> specification)
        {
            return false;
        }

        #endregion
    }
}
