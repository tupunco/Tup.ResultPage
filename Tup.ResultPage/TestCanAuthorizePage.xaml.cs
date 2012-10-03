using System.Windows;

using Tup.ResultPage.Controls;
using Tup.ResultPage.Utils;

namespace Tup.ResultPage
{
    /// <summary>
    /// 不强制登录页面测试
    /// </summary>
    public partial class TestCanAuthorizePage : PhoneCanAuthorizeApplicationPage
    {
        public TestCanAuthorizePage()
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
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("不需要登录访问功能-Msg:1");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (!this.TryUserLogin(res => { Button2_Click(sender, e); }))
                return;

            MessageBox.Show("需要登录访问功能-Msg:2");
        }
    }
}