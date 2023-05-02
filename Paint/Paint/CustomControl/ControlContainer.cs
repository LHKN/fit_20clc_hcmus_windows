using Paint.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Paint.CustomControl
{
    /// <summary>
    /// Interaction logic for ControlContainer.xaml
    /// </summary>
    public class ControlContainer : ContentControl
    {
        //bool IsSelected { get; set; }
        static ControlContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ControlContainer), new FrameworkPropertyMetadata(typeof(ControlContainer)));
        }

        //public ScaleTransform ScaleTransform => RenderTransform is TransformGroup group ? group.Children.OfType<ScaleTransform>().FirstOrDefault() : null;
        //public TranslateTransform TranslateTransform => RenderTransform is TransformGroup group ? group.Children.OfType<TranslateTransform>().FirstOrDefault() : null;
        //public RotateTransform RotateTransform => RenderTransform is TransformGroup group ? group.Children.OfType<RotateTransform>().FirstOrDefault() : null;

        // Apply Transform 
        public static DependencyProperty ApplyTransformProperty =
            DependencyProperty.RegisterAttached(
                "ApplyTransform",
                typeof(Transform),
                typeof(ControlContainer),
                new FrameworkPropertyMetadata(default(Transform), OnApplyTransformChanged)
            );

        public static void SetApplyTransform(UIElement element, Transform value)
        {
            element.SetValue(ApplyTransformProperty, value);
        }

        public static Transform GetApplyTransform(UIElement element)
        {
            return (Transform)element.GetValue(ApplyTransformProperty);
        }

        private static void OnApplyTransformChanged(ControlContainer d, DependencyPropertyChangedEventArgs e)
        {
            var designItemContainer = Utils.Control.GetParentControl<ControlContainer>(d);
            designItemContainer.ApplyTransform((Transform)e.NewValue);
        }

        public void ApplyTransform(Transform apply)
        {
            if (apply != null)
                RenderTransform = apply.Clone();
        }

        // Host => DesignItemsControl
        public ControlItem? Host => (ControlItem)ItemsControl.ItemsControlFromItemContainer(this);

        // ID
        public static readonly DependencyProperty IDProperty =
                DependencyProperty.Register("ID", typeof(double), typeof(ControlContainer));

        public Guid ID
        {
            get { return (Guid)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        // zIndex
        public static readonly DependencyProperty ZIndexProperty =
                DependencyProperty.Register("ZIndex", typeof(double), typeof(ControlContainer));

        public int ZIndex
        {
            get { return (int)GetValue(ZIndexProperty); }
            set { SetValue(ZIndexProperty, value); }
        }

        // Top
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(double), typeof(ControlContainer));

        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        // Left
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(ControlContainer));

        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(TopProperty, value); }
        }

        // Is Selected
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected",
                                        typeof(bool),
                                        typeof(ControlContainer),
                                        new FrameworkPropertyMetadata(false));
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            DraggingSelected.SetIsPossibleDragging((ControlContainer)e.OriginalSource, true);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            DraggingSelected.SetIsPossibleDragging((ControlContainer)e.OriginalSource, false);
        }
    }
}



