using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace Enterprise.Core.Interface.Data
{
    public interface ISpecification<EntityRoot>
        where EntityRoot : class
    {
        Expression<Func<EntityRoot, bool>> SatisfiedBy();

        ISpecification<EntityRoot> And(ISpecification<EntityRoot> other);

        ISpecification<EntityRoot> Or(ISpecification<EntityRoot> other);

        ISpecification<EntityRoot> Not();
    }
}
