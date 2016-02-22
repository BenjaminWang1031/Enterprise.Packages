
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Enterprise.Component.Common.Encrypt
{
    public static class MD5Helper
    {
        /// <summary>
        /// Get Encrypt String
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string GetEncryptString(string msg)
        {
            UnicodeEncoding en = new UnicodeEncoding();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] ueByte = en.GetBytes(msg);
            byte[] md5Byte = md5.ComputeHash(ueByte);
            return BitConverter.ToString(md5Byte); 
        }

        /// <summary>
        /// constant parms
        /// </summary>
        private static char[] constant =   
        {   
            '0','1','2','3','4','5','6','7','8','9',   
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
        };

        /// <summary>
        /// Get Random 
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        private static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }

        /// <summary>
        /// Reset User Password
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static UserPwdModal ResetUserPassword()
        {
            var modal = new UserPwdModal();
            modal.PasswordText = GenerateRandom(6);
            modal.EncryptPasswordText = GetEncryptString(modal.PasswordText);
            return modal;
        }   
    }

    /// <summary>
    /// User Init Pwd Modal
    /// </summary>
    public class UserPwdModal
    {
        public virtual string PasswordText { get; set; }
        public virtual string EncryptPasswordText { get; set; }
    }
}
