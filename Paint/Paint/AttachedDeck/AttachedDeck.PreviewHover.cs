using System;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Input;
using MyContract;
using Paint.Deck;
using Paint.CustomControl;

namespace Paint.AttachedDeck
{
    public partial class AttachedDeck
    {
        // For Rectangle
        public static readonly DependencyProperty HasHoverAdornerProperty =
            DependencyProperty.RegisterAttached(
                "HasHoverAdorner",
                typeof(bool),
                typeof(AttachedDeck),
                new FrameworkPropertyMetadata(false, OnHasHoverAdornerChanged)
            );

        public static void SetHasHoverAdorner(UIElement element, bool value)
            => element.SetValue(HasHoverAdornerProperty, value);

        public static bool GetHasHoverLine(UIElement element)
            => (bool)element.GetValue(HasHoverAdornerProperty);

        private static void OnHasHoverAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ControlContainer element = (ControlContainer) d;
            bool value = (bool)e.NewValue;


            if (value)
            {
                element.MouseEnter += OnMouseEnterRectangle;
                element.MouseLeave += OnMouseLeaveRectangle;
            }
            else
            {
                element.MouseEnter -= OnMouseEnterRectangle;
                element.MouseLeave -= OnMouseLeaveRectangle;
            }
        }

        private static void OnMouseEnterRectangle(object sender, MouseEventArgs e)
        {
            ControlContainer element = (ControlContainer)sender;
            IShape nodeVM = (IShape)element.DataContext;

            if (nodeVM == null) return;
            if (nodeVM.IsDrawing || nodeVM.IsSelected) return;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
            RectangleHoverDeck adorner = new RectangleHoverDeck(element);

            layer.Add(adorner);
            _currentRectHoverAdorner = adorner;
        }

        private static void OnMouseLeaveRectangle(object sender, MouseEventArgs e)
        {
            ControlContainer element = (ControlContainer)sender;
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);

            if (layer != null && _currentRectHoverAdorner != null)
            {
                layer.Remove(_currentRectHoverAdorner);
            }
        }

        // For Line
        public static readonly DependencyProperty HasLineHoverAdornerProperty =
                DependencyProperty.RegisterAttached(
                    "HasLineHoverAdorner",
                    typeof(bool),
                    typeof(AttachedDeck),
                    new FrameworkPropertyMetadata(false, OnHasLineHoverAdornerChanged)
                );

        public static void SetHasLineHoverAdorner(UIElement element, bool value)
            => element.SetValue(HasLineHoverAdornerProperty, value);

        public static bool GetHasLineHoverAdorner(UIElement element)
            => (bool)element.GetValue(HasLineHoverAdornerProperty);

        private static void OnHasLineHoverAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ControlContainer element = (ControlContainer)d;
            bool value = (bool)e.NewValue;

            if (value)
            {
                element.MouseEnter += OnMouseEnterLine;
                element.MouseLeave += OnMouseLeaveLine;
            }
            else
            {
                element.MouseEnter -= OnMouseEnterLine;
                element.MouseLeave -= OnMouseLeaveLine;
            }
        }

        private static void OnMouseEnterLine(object sender, MouseEventArgs e)
        {
            ControlContainer element = (ControlContainer)sender;
            IShape nodeVM = (IShape)element.DataContext;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);

            //var adorner = new LineHoverAdorner(line);
            //var highlightLine = new Line
            //{
            //    Stroke = Brushes.DodgerBlue,
            //    StrokeThickness = 3,
            //    X1 = line.X1,
            //    X2 = line.X2,
            //    Y1 = line.Y1,
            //    Y2 = line.Y2
            //};
            //adorner.Container.Content = highlightLine;
            //layer.Add(adorner);
            //_currentLineAdorner = adorner;
        }

        private static void OnMouseLeaveLine(object sender, MouseEventArgs e)
        {
            ControlContainer element = (ControlContainer)sender;
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);

            if (layer != null && _currentLineHoverAdorner != null)
            {
                layer.Remove(_currentLineHoverAdorner);
            }
        }
    }
}

