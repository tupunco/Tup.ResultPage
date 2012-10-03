using System;
using System.Collections.Generic;

namespace Tup.ResultPage.Controls
{
    /// <summary>
    /// 结果页面接口
    /// </summary>
    public interface IResultPage<TResult>
    {
        /// <summary>
        /// 结果
        /// </summary>
        IDictionary<string, TResult> Result { get; }
        /// <summary>
        /// 页面完成时事件
        /// </summary>
        event EventHandler<ResultPageEventArgs<IDictionary<string, TResult>>> Completed;
    }
    /// <summary>
    /// 结果页面 结果模式
    /// </summary>
    public enum ResultPageMode
    {
        /// <summary>
        /// 用户返回按钮触发
        /// </summary>
        Back = 0,
        /// <summary>
        /// 用户其他命令出发
        /// </summary>
        Command = 1
    }
    /// <summary>
    /// 结果页面 结果参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultPageEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 页面结果模式
        /// </summary>
        public ResultPageMode Mode { get; set; }
        /// <summary>
        /// 结果值
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[ResultPageEventArgs ResultPageMode:{0} Result:{1}]", Mode, Result);
        }
    }
}
