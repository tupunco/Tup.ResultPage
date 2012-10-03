using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Tup.ResultPage.Controls;

namespace Tup.ResultPage
{
    public partial class MainPage : PhoneResultReceiveApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            HookPageNavigatedFrom<TestResultPage>(e, res =>
            {
                SubmitToPLAQ(() =>
                        {
                            if (res.Mode == ResultPageMode.Command && res.Result != null && res.Result.Count > 0)
                            {
                                var resRes = res.Result["res"];
                                var resType = res.Result["type"];

                                MessageBox.Show(string.Format("--返回结果:res:{0}-type:{1}-", resRes, resType));
                            }
                            else
                            {
                                MessageBox.Show("--硬件返回-");
                            }
                        });
            });

            base.OnNavigatedFrom(e);
        }
        /// <summary>
        /// 需要返回结果的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTestResultPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/TestResultPage.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 模拟登录页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUserLoginPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/UserLoginPage.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 强制登录页面测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTestAuthorizePage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/TestAuthorizePage.xaml", UriKind.Relative));
        }
        /// <summary>
        /// 不强制登录页面测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTestCanAuthorizePage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/TestCanAuthorizePage.xaml", UriKind.Relative));
        }
    }
}