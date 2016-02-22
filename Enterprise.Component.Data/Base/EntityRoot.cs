using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Enterprise.Core.Interface.Data;

namespace Enterprise.Component.Nhiberate.Base
{
    /// <summary>
    /// EntityRoot
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class EntityRoot:IEntityRoot
    {

        public override int GetHashCode()
        {
            return Utility.GetHashCode(base.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public virtual bool Equals(IEntityRoot other)
        {
            if (object.ReferenceEquals(this, other))
                return true;
            if ((object)other == null)
                return false;
            return base.Equals(other);
        }

        public virtual string ComponentId { get; set; }
    }
}
