using Tup.ResultPage.Controls;
using Tup.ResultPage.Utils;
using System.Collections.Generic;

namespace Tup.ResultPage
{
    /// <summary>
    /// 模拟登录页面
    /// </summary>
    public partial class UserLoginPage : PhoneResultApplicationPage
    {
        public UserLoginPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (UserDataServices.IsSigned)
                TextBlockLoginInfo.Text = "已登录";
            else
                TextBlockLoginInfo.Text = "未登录";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSignin_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserDataServices.FakeUserLogin();

            DeferredAction(() =>
                            {
                                SetResult(new Dictionary<string, int>() { { "Type", 1 }, { "ResResult", 0 } });
                            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSignOut_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserDataServices.FakeUserLogin(false);
        }
    }
}