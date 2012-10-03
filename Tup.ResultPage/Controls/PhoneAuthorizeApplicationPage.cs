using System.ComponentModel;
using System.Windows.Navigation;
using Tup.ResultPage.Utils;

namespace Tup.ResultPage.Controls
{
    /// <summary>
    /// 需要登录访问的页面
    /// </summary>
    /// <remarks>
    /// 登录后数据处理部分请重写 <see cref="PhoneAuthorizeApplicationPage.OnSigned"/>
    /// </remarks>
    public class PhoneAuthorizeApplicationPage : PhoneCanAuthorizeApplicationPage
    {
        private bool m_IsUserLoginPageBack = false;
        /// <summary>
        /// 导航进入 判断用户登录情况
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (DesignerProperties.IsInDesignTool)
                return;

            if (!UserDataServices.IsSigned)
            {
                //如果是登录页面返回, 但是登录不成功, 退出本页面
                if (e.NavigationMode == NavigationMode.Back && m_IsUserLoginPageBack)
                {
                    SubmitToPLAQ(() =>
                    {
                        #region GoBack
                        this.IsHitTestVisible = false;

                        DeferredAction(() =>
                        {
                            this.IsHitTestVisible = true;

                            if (NavigationService.CanGoBack)
                                NavigationService.GoBack();
                            else
                                throw new NotSignedException("!UserDataServices.IsSigned");
                        });
                        #endregion
                    });

                    m_IsUserLoginPageBack = false;
                    return;
                }

                ////网络未连接
                //if (!IsNetworkConnected())
                //    return;

                TryUserLogin(
                    res => { OnSigned(); },
                    res =>
                    {
                        m_IsUserLoginPageBack = true;
                    });
            }
        }
        /// <summary>
        /// 登录后事件
        /// </summary>
        protected virtual void OnSigned()
        {
        }
    }
}
