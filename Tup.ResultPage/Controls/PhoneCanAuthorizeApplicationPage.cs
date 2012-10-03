using System;
using System.Collections.Generic;
using System.Windows.Navigation;
using Tup.ResultPage.Utils;

namespace Tup.ResultPage.Controls
{
    /// <summary>
    /// 附带 登录 功能的页面
    /// </summary>
    public class PhoneCanAuthorizeApplicationPage : PhoneResultReceiveApplicationPage
    {
        /// <summary>
        /// 尝试用户登录, 用户没有登录 跳转到登录页面
        /// </summary>
        /// <param name="signedAction">登录后动作(PLAQ 执行)</param>
        /// <param name="backAction">登录页面返回动作(不判断是否登录成功)(非 PLAQ 执行)</param>
        /// <returns>需要用户登录 返回 false 同时引导用户到登录页面, 不需要登录 返回 true</returns>
        protected bool TryUserLogin(Action<IDictionary<string, int>> signedAction = null,
                                    Action<ResultPageEventArgs<IDictionary<string, int>>> backAction = null)
        {
            return TryUserLogin(false, signedAction, backAction);
        }
        /// <summary>
        /// 尝试用户登录, 用户没有登录 跳转到登录页面
        /// </summary>
        /// <param name="isEnforce">强制执行登录操作(如果已经登录也执行登录操作)</param>
        /// <param name="signedAction">登录后动作(PLAQ 执行)</param>
        /// <param name="backAction">登录页面返回动作(不判断是否登录成功)(非 PLAQ 执行)</param>
        /// <returns>需要用户登录 返回 false 同时引导用户到登录页面, 不需要登录 返回 true</returns>
        protected bool TryUserLogin(bool isEnforce, Action<IDictionary<string, int>> signedAction = null,
                                    Action<ResultPageEventArgs<IDictionary<string, int>>> backAction = null)
        {
            if (!isEnforce && UserDataServices.IsSigned)
                return true;

            this.SignedAction = signedAction;
            this.BackAction = backAction;

            SubmitToPLAQ(() =>
            {
                this.IsHitTestVisible = false;

                DeferredAction(() =>
                {
                    this.IsHitTestVisible = true;

                    this.GoUserLoginPage();
                });
            });

            return false;
        }
        /// <summary>
        /// 导航出页面事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //打开登录页面挂载
            HookPageNavigatedFrom<UserLoginPage>(e, res =>
            {
                if (this.BackAction != null)
                {
                    this.BackAction(res);
                    this.BackAction = null;
                }

                if (res.Mode == ResultPageMode.Command && res.Result != null && res.Result.Count > 0)
                {
                    if (this.SignedAction != null && UserDataServices.IsSigned)
                    {
                        SubmitToPLAQ(() =>
                        {
                            this.SignedAction(res.Result);
                            this.SignedAction = null;
                        });
                    }
                }
            });

            base.OnNavigatedFrom(e);
        }
        /// <summary>
        /// 登录后动作
        /// </summary>
        private Action<IDictionary<string, int>> SignedAction = null;
        /// <summary>
        /// 登录页面返回动作(不判断是否登录成功)
        /// </summary>
        private Action<ResultPageEventArgs<IDictionary<string, int>>> BackAction = null;
    }
}
