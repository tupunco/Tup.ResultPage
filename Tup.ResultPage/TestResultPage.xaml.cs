using System.Collections.Generic;
using System.Windows.Controls;

using Tup.ResultPage.Controls;

namespace Tup.ResultPage
{
    /// <summary>
    /// 需要返回结果的界面
    /// </summary>
    public partial class TestResultPage : PhoneResultApplicationPage
    {
        public TestResultPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag.ToString();
            var tags = tag.Split(',');
            var res = int.Parse(tags[0]);
            var type = int.Parse(tags[1]);

            SetResult(new Dictionary<string, int>() { 
                {"res", res},
                {"type", type}
            });
        }
    }
}