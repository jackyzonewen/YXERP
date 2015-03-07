using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSalesTool
{
    public class Encrypt
    {
        /// <summary>
        ///  通过用户名加密密码
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static string GetEncryptPwd(string pwd, string userName)
        {
            userName = userName.ToUpper();
            for (int i = 0; i < userName.Length; i++)
            {
                if (i % 3 == 1)
                {
                    pwd = userName[i] + pwd;
                }
            }
            for (int i = 0; i < userName.Length; i++)
            {
                if (i % 2 == 0)
                {
                    pwd += userName[i];
                }
            }
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");//System.Web.Configuration.FormsAuthPasswordFormat;
        }
    }
}
