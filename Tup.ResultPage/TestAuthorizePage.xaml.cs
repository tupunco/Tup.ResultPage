using Tup.ResultPage.Controls;
using Tup.ResultPage.Utils;

namespace Tup.ResultPage
{
    /// <summary>
    /// 强制登录页面测试
    /// </summary>
    public partial class TestAuthorizePage : PhoneAuthorizeApplicationPage
    {
        public TestAuthorizePage()
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
    }
}