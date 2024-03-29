﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using GalaSoft.MvvmLight;

using Microsoft.Phone.Controls;

using Tup.ResultPage.Utils;

namespace Tup.ResultPage.Controls
{
    /// <summary>
    /// Base PhoneApplicationPage Base 页面
    /// </summary>
    public class PhoneBaseApplicationPage : PhoneApplicationPage, ICleanup
    {
        private BaseApplicationWraper m_PageWraper = null;

        /// <summary>
        /// 
        /// </summary>
        public PhoneBaseApplicationPage()
        {
            if (DesignerProperties.IsInDesignTool)
                return;

            m_PageWraper = new BaseApplicationWraper(this);
        }
        /// <summary>
        /// 页面导航退出清除 可能的信息
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (!DesignerProperties.IsInDesignTool)
            {
                if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
                    Cleanup();
            }

            base.OnNavigatedFrom(e);
        }

        #region Utils
        /// <summary>
        /// 网络是否 连接中
        /// </summary>
        /// <param name="ignoreToast">是否忽略 Toast 提示</param>
        /// <returns>连接中返回 true, 未连接返回 false 同时提示'网络未连接'信息</returns>
        public bool IsNetworkConnected(bool ignoreToast = false)
        {
            return m_PageWraper.IsNetworkConnected(ignoreToast);
        }
        /// <summary>
        /// 延迟动作
        /// </summary>
        /// <param name="action"></param>
        /// <remarks>
        /// 200 秒后执行, 有的时候需要等待页面动画执行完毕后再执行动作, 可以使用本函数包装执行.
        /// </remarks>
        public void DeferredAction(Action action)
        {
            DeferredAction(action, 200);//INFO 200 毫秒可以跳过页面动画
        }
        /// <summary>
        /// 延迟动作
        /// </summary>
        /// <param name="action"></param>
        /// <param name="deferDillisecond">延迟毫秒数</param>
        public void DeferredAction(Action action, int deferDillisecond)
        {
            m_PageWraper.DeferredAction(action, deferDillisecond);
        } 
        #endregion

        #region IsPageLoad 状态/PLAQ 处理
        /// <summary>
        /// 当前页面是否已经 Load 完成
        /// </summary>
        public bool IsPageLoaded
        {
            get
            {
                return m_PageWraper.IsPageLoaded;
            }
        }

        #region PLAQ
        /// <summary>
        /// 提交执行动作在 Page_Loaded 时执行[同步执行]
        /// </summary>
        /// <param name="action"></param>
        /// <remarks>
        /// 如果 已经 Page Loaded 就直接执行.
        /// </remarks>
        public void SubmitToPLAQ(Action action)
        {
            m_PageWraper.SubmitToPLAQ(action);
        }
        #endregion
        #endregion

        #region ICleanup 成员
        /// <summary>
        /// 页面 NavigationMode.Back 的时候某些清空动作
        /// </summary>
        public virtual void Cleanup()
        {
        }
        #endregion
    }
    /// <summary>
    /// Base UserControl Base 类
    /// </summary>
    public class BaseUserControl : UserControl, ICleanup
    {
        private BaseApplicationWraper m_PageWraper = null;
        /// <summary>
        /// 
        /// </summary>
        public BaseUserControl()
        {
            m_PageWraper = new BaseApplicationWraper(this);
        }

        #region Utils
        /// <summary>
        /// 网络是否 连接中
        /// </summary>
        /// <param name="ignoreToast">是否忽略 Toast 提示</param>
        /// <returns>连接中返回 true, 未连接返回 false 同时提示'网络未连接'信息</returns>
        public bool IsNetworkConnected(bool ignoreToast = false)
        {
            return m_PageWraper.IsNetworkConnected(ignoreToast);
        }
        /// <summary>
        /// 延迟动作
        /// </summary>
        /// <param name="action"></param>
        /// <remarks>
        /// 200 秒后执行, 有的时候需要等待页面动画执行完毕后再执行动作, 可以使用本函数包装执行.
        /// </remarks>
        public void DeferredAction(Action action)
        {
            DeferredAction(action, 200);//INFO 200 毫秒可以跳过页面动画
        }
        /// <summary>
        /// 延迟动作
        /// </summary>
        /// <param name="action"></param>
        /// <param name="deferDillisecond">延迟毫秒数</param>
        public void DeferredAction(Action action, int deferDillisecond)
        {
            m_PageWraper.DeferredAction(action, deferDillisecond);
        } 
        #endregion

        #region IsPageLoad 状态/PLAQ 处理
        /// <summary>
        /// 当前页面是否已经 Load 完成
        /// </summary>
        public bool IsPageLoaded
        {
            get
            {
                return m_PageWraper.IsPageLoaded;
            }
        }

        #region PLAQ
        /// <summary>
        /// 提交执行动作在 Page_Loaded 时执行[同步执行]
        /// </summary>
        /// <param name="action"></param>
        /// <remarks>
        /// 如果 已经 Page Loaded 就直接执行.
        /// </remarks>
        public void SubmitToPLAQ(Action action)
        {
            m_PageWraper.SubmitToPLAQ(action);
        }
        #endregion
        #endregion

        #region ICleanup 成员
        /// <summary>
        /// 
        /// </summary>
        public virtual void Cleanup()
        {
        }
        #endregion
    }
    /// <summary>
    /// Base FrameworkElement 包装
    /// </summary>
    internal class BaseApplicationWraper
    {
        /// <summary>
        /// 当前页面
        /// </summary>
        private FrameworkElement m_Element = null;

        #region Page
        private PhoneApplicationPage _page;
        private Frame _rootVisual;
        /// <summary>
        /// 
        /// </summary>
        private PhoneApplicationPage Page
        {
            get { return _page ?? (_page = RootVisual.FindControl<PhoneApplicationPage>()); }
        }
        /// <summary>
        /// 
        /// </summary>
        private Frame RootVisual
        {
            get { return _rootVisual ?? (_rootVisual = Application.Current.RootVisual as Frame); }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cElement"></param>
        /// <param name="cOnUedasCmfuTracker"></param>
        public BaseApplicationWraper(FrameworkElement cElement)
        {
            ThrowHelper.ThrowIfNull(cElement, "cElement");

            this.m_Element = cElement;

            if (!DesignerProperties.IsInDesignTool)
            {
                //IsPageLoad 状态
                m_Element.Loaded += PhoneResultApplicationPage_Loaded;
                m_Element.Unloaded += PhoneResultApplicationPage_Unloaded;
            }
        }

        #region Utils
        /// <summary>
        /// 网络是否 连接中
        /// </summary>
        /// <param name="ignoreToast">是否忽略 Toast 提示</param>
        /// <returns>连接中返回 true, 未连接返回 false 同时提示'网络未连接'信息</returns>
        public bool IsNetworkConnected(bool ignoreToast = false)
        {
            if (DesignerProperties.IsInDesignTool)
                return true;

            //var state = NetworkMonitorServices.IsConnected();
            //if (state)
            //    return true;

            //if (!ignoreToast)
            //{
            //    state.Toast(LResources.ToastPrompt_NetworkServiceFailed_Disconnect_Message,
            //                LResources.ToastPrompt_NetworkServiceFailed_Title,
            //                ToastType.Failed);
            //}

            return false;
        }

        /// <summary>
        /// 延迟动作
        /// </summary>
        /// <param name="action"></param>
        /// <param name="deferDillisecond">延迟毫秒数</param>
        public void DeferredAction(Action action, int deferDillisecond)
        {
            ThrowHelper.ThrowIfNull(action, "action");

            System.Threading.ThreadPool.QueueUserWorkItem(_ =>
            {
                System.Threading.Thread.Sleep(deferDillisecond);

                m_Element.Dispatcher.BeginInvoke(() =>
                {
                    action();
                });
            });
        }
        #endregion

        #region IsPageLoad 状态/PLAQ 处理
        /// <summary>
        /// 当前页面是否已经 Load 完成
        /// </summary>
        public bool IsPageLoaded { get; private set; }

        #region PageLoaded
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneResultApplicationPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsPageLoaded = true;

            //执行 PLAQ 内所有动作
            ExecutePLAQ();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneResultApplicationPage_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsPageLoaded = false;
        }
        #endregion

        #region PLAQ
        /// <summary>
        /// Page_Loaded_Action_Queue/PLAQ
        /// </summary>
        private Queue<Action> m_PageLoadedActionQueue = new Queue<Action>();
        /// <summary>
        /// 提交执行动作在 Page_Loaded 时执行[同步执行]
        /// </summary>
        /// <param name="action"></param>
        /// <remarks>
        /// 如果 已经 Page Loaded 就直接执行.
        /// </remarks>
        public void SubmitToPLAQ(Action action)
        {
            ThrowHelper.ThrowIfNull(action, "action");

            lock (m_PageLoadedActionQueue)
            {
                if (this.IsPageLoaded)
                    action();
                else
                    m_PageLoadedActionQueue.Enqueue(action);
            }
        }
        /// <summary>
        /// 执行 QLAQ 内所有动作[同步执行]
        /// </summary>
        private void ExecutePLAQ()
        {
            if (DesignerProperties.IsInDesignTool)
                return;

            if (m_PageLoadedActionQueue.Count <= 0)
                return;

            lock (m_PageLoadedActionQueue)
            {
                while (m_PageLoadedActionQueue.Count > 0)
                {
                    (m_PageLoadedActionQueue.Dequeue())();
                }
            }
        }
        #endregion
        #endregion
    }
}
