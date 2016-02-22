using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Core.Interface.Log
{
    /// <summary>
    /// Log all visitors/users
    /// </summary>
    public interface ILogUserHelper:ILogHelper
    {
        /// <summary>
        /// Log current user
        /// </summary>
        /// <param name="MoudleInfo">MoudleInfo</param>
        /// <param name="CurrentVisitor">Current user</param>
        void LogUser(string MoudleInfo, UserInfo CurrentUserInfo);
    }
}
