using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using Paint.CustomControl;

namespace Paint.Utils
{
    public class Control
    {
        public static T GetParentControl<T>(DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);

            while (parent != null && parent.GetType() != typeof(T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return (T)parent;
        }

        public static T GetParentControl<T>(DependencyObject child, Func<DependencyObject, bool> predicate) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);

            while (parent != null && predicate(parent))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return (T)parent;
        }
    }
}
