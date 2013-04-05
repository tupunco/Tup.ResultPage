using System;
using System.Collections.Generic;
using System.Windows.Navigation;

namespace Tup.ResultPage.Controls
{
    /// <summary>
    /// 可接收 页面返回值/带返回值 的 PhoneApplicationPage
    /// </summary>
    public class PhoneResultReceiveApplicationPage : PhoneResultApplicationPage
    {
        /// <summary>
        /// 处理页面导航出事件-打开页面挂载
        /// </summary>
        /// <param name="e"></param>
        /// <remarks>
        /// OnNavigatedFrom(NavigationEventArgs e) 内调用
        /// </remarks>
        protected void HookPageNavigatedFrom<TPageResult>(NavigationEventArgs navArgs, Action<ResultPageEventArgs<IDictionary<string, int>>> completed)
          where TPageResult : class, IResultPage<int>
        {
            if (navArgs.NavigationMode != NavigationMode.New || navArgs.Content == null || !(navArgs.Content is TPageResult))
                return;

            var page = navArgs.Content as TPageResult;

            this.CompletedAction = completed;

            page.Completed -= this.Page_Completed;
            page.Completed += this.Page_Completed;
        }

        private Action<ResultPageEventArgs<IDictionary<string, int>>> CompletedAction = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Completed(object sender, ResultPageEventArgs<IDictionary<string, int>> e)
        {
            ((IResultPage<int>)sender).Completed -= this.Page_Completed;

            if (this.CompletedAction != null)
                this.CompletedAction(e);
        }
    }
}
