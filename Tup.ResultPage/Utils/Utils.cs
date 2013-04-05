using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Tup.ResultPage.Utils
{
    /// <summary>
    /// 
    /// </summary>
    static class Utils
    {
        /// <summary>
        /// 查找 DependencyObject 子对象
        /// </summary>
        /// <typeparam name="TDependencyObject">待操作对象类型</typeparam>
        /// <param name="rootDependencyObject">根对象</param>
        /// <param name="name">已知待查找对象名称, 为空会被忽略</param>
        /// <returns>查找的结果对象</returns>
        public static TDependencyObject FindControl<TDependencyObject>(this DependencyObject rootDependencyObject, string name = null)
            where TDependencyObject : FrameworkElement
        {
            ThrowHelper.ThrowIfNull(rootDependencyObject, "rootDependencyObject");

            var queue = new Queue<DependencyObject>();
            queue.Enqueue(rootDependencyObject);
            DependencyObject current = null;
            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                for (int i = VisualTreeHelper.GetChildrenCount(current) - 1; 0 <= i; i--)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    if (child is TDependencyObject)
                    {
                        if (string.IsNullOrEmpty(name))
                            return child as TDependencyObject;
                        else if (((FrameworkElement)child).Name == name)
                            return child as TDependencyObject;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
        }
    }
}
