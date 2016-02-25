using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace Enterprise.Core.Interface.Data
{
    /// <summary>
    /// @Dec: Base Interface of specification which is used for querying entities
    /// @Author: Benjamin Wang
    /// @Date:02/2016
    /// </summary>
    /// <typeparam name="EntityRoot"></typeparam>
    public interface ISpecification<EntityRoot>
        where EntityRoot : class
    {
        /// <summary>
        /// Meets
        /// </summary>
        /// <returns></returns>
        Expression<Func<EntityRoot, bool>> SatisfiedBy();

        /// <summary>
        /// And
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        ISpecification<EntityRoot> And(ISpecification<EntityRoot> other);

        /// <summary>
        /// Or
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        ISpecification<EntityRoot> Or(ISpecification<EntityRoot> other);

        /// <summary>
        /// Not
        /// </summary>
        /// <returns></returns>
        ISpecification<EntityRoot> Not();
    }
}
