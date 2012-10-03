using System;
using System.Collections.Generic;

using Tup.ResultPage.Utils;

namespace Tup.ResultPage.Controls
{
    /// <summary>
    /// 带返回值 的 PhoneApplicationPage 
    /// </summary>
    public class PhoneResultApplicationPage : PhoneBaseApplicationPage, IResultPage<int>
    {
        #region IResultPage<int> 成员
        /// <summary>
        /// 页面 返回键 动作
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (!e.Cancel)
                RaiseCompleted(ResultPageMode.Back, null);

            base.OnBackKeyPress(e);
        }
        /// <summary>
        /// 页面返回值
        /// </summary>
        public IDictionary<string, int> Result
        {
            get;
            private set;
        }
        /// <summary>
        /// 页面返回成功 事件
        /// </summary>
        public event EventHandler<ResultPageEventArgs<IDictionary<string, int>>> Completed;
        /// <summary>
        /// 触发 页面返回成功 事件
        /// </summary>
        /// <param name="resultMode"></param>
        /// <param name="result"></param>
        protected void RaiseCompleted(ResultPageMode resultMode, IDictionary<string, int> result)
        {
            if (this.Completed == null)
                return;

            this.Completed(this, new ResultPageEventArgs<IDictionary<string, int>>()
            {
                Result = result,
                Mode = resultMode
            });
        }
        /// <summary>
        /// 设置返回默认结果(Result=0), 并返回当前页面
        /// </summary>
        protected void SetResult()
        {
            this.SetResult(new Dictionary<string, int>() { { "Result", 0 } });
        }
        /// <summary>
        /// 设置返回结果, 并返回当前页面
        /// </summary>
        protected void SetResult(IDictionary<string, int> result)
        {
            if (result == null || result.Count <= 0)
                return;

            if (Result == null)
                Result = new Dictionary<string, int>(result);
            else
            {
                foreach (var item in result)
                {
                    Result[item.Key] = item.Value;
                }
            }

            RaiseCompleted(ResultPageMode.Command, this.Result);

            SubmitToPLAQ(() =>
            {
                #region GoBack
                try
                {
                    if (NavigationService.CanGoBack)
                        NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    //LogHelper.LogError("SetResult-NavigationService.GoBack-ex:{0}", ex);
                    ex = null;
                }
                #endregion
            });
        }
        #endregion
    }
}
