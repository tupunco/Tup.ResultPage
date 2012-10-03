using System;
using Microsoft.Phone.Controls;

namespace Tup.ResultPage.Utils
{
    /// <summary>
    /// 模拟登录相关
    /// </summary>
    public static class UserDataServices
    {
        private static bool s_IsSigned = false;
        /// <summary>
        /// 
        /// </summary>
        public static bool IsSigned
        {
            get
            {
                return s_IsSigned;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static void FakeUserLogin()
        {
            FakeUserLogin(true);
        }
        /// <summary>
        /// 模拟登录
        /// </summary>
        public static void FakeUserLogin(bool isSigned)
        {
            s_IsSigned = isSigned;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        public static void GoUserLoginPage(this PhoneApplicationPage page)
        {
            ThrowHelper.ThrowIfNull(page, "page");

            page.NavigationService.Navigate(new System.Uri("/UserLoginPage.xaml", UriKind.Relative));
        }
    }
    /// <summary>
    /// 未登录异常
    /// </summary>
    public class NotSignedException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public NotSignedException() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public NotSignedException(string msg) : base(msg) { }
    }
}
