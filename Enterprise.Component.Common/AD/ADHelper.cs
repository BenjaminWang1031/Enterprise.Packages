using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Enterprise.Core.Interface.Log;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace Enterprise.Component.Common
{
    public class ADHelper
    {
        public static UserInfo GetCurrentUserInfo(string DomainName, string DomainContainer)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                return null;
            var PrincipalCtxt = new PrincipalContext(ContextType.Domain, DomainName, DomainContainer);
            var UsrPrincipal = UserPrincipal.FindByIdentity(PrincipalCtxt, IdentityType.SamAccountName, HttpContext.Current.User.Identity.Name);
            if (UsrPrincipal != null)
            {
                var mUserInfo = new UserInfo();
                mUserInfo.UserId = UsrPrincipal.SamAccountName.Replace(".", "_");
                mUserInfo.UserName = String.Format("{0}.{1}", UsrPrincipal.GivenName, UsrPrincipal.Surname);
                mUserInfo.UserEmail = UsrPrincipal.EmailAddress;
                var mDe = (DirectoryEntry)UsrPrincipal.GetUnderlyingObject();
                if (mDe != null)
                {
                    mUserInfo.Country = Utility.TypeHelper.GetObjectValue(mDe.Properties["co"].Value);
                    mUserInfo.City = Utility.TypeHelper.GetObjectValue(mDe.Properties["l"].Value);
                    mUserInfo.Region = Utility.TypeHelper.GetObjectValue(mDe.Properties["st"].Value);
                    mUserInfo.Org = Utility.TypeHelper.GetObjectValue(mDe.Properties["department"].Value);
                }

                var strHostName = System.Net.Dns.GetHostName();
                mUserInfo.Ip = System.Net.Dns.GetHostEntry(strHostName).AddressList[1].ToString();
                return mUserInfo;
            }
            return null;
        }

        public static bool IsMemberOf(string GroupName, string DomainName, string DomainContainer)
        {
            if (string.IsNullOrEmpty(GroupName)) return false;
            if (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) return false;

            var PrincipalCtxt = new PrincipalContext(ContextType.Domain, DomainName, DomainContainer);
            var List = GroupName.Split(',');
            foreach (var Group in List)
            {
                var UsrPrincipal = UserPrincipal.FindByIdentity(PrincipalCtxt, IdentityType.SamAccountName, HttpContext.Current.User.Identity.Name);
                if (UsrPrincipal != null)
                {
                    var grpPrincipalContext = new PrincipalContext(ContextType.Domain, DomainName);
                    var groupPrincipal  = GroupPrincipal.FindByIdentity(grpPrincipalContext, Group);
                    if (UsrPrincipal.IsMemberOf(groupPrincipal)) return true;
                }
            }
            return false;
        }

    }
}
