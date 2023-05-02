using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using MyContract;
using Paint.Deck;
using Paint.CustomControl;

namespace Paint.AttachedDeck
{
    public partial class AttachedDeck
    {
        public static readonly DependencyProperty ShowResizeAdornerProperty =
            DependencyProperty.RegisterAttached(
                "ShowResizeAdorner",
                typeof(bool),
                typeof(AttachedDeck),
                new FrameworkPropertyMetadata(false, OnShowResizeAdornerChanged)
            );

        public static void SetShowResizeAdorner(UIElement element, bool value)
            => element.SetValue(ShowResizeAdornerProperty, value);

        public static bool GetShowResizeLine(UIElement element)
            => (bool)element.GetValue(ShowResizeAdornerProperty);

        private static void OnShowResizeAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ControlContainer element = (ControlContainer)d;
            IShape nodeVM = (IShape)element.DataContext;

            bool value = (bool)e.NewValue;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);

            if (value)
            {
                if (nodeVM is IShape)
                    HandleAddResizeLineAdorner(element, layer);
                else
                    HandleAddResizeRectangleAdorner(element, layer);
            }

            if (!value)
            {
                if (nodeVM is IShape)
                    HandleRemoveResizeLineAdorner(element, layer);
                else
                    HandleRemoveResizeRectangleAdorner(element, layer);
            }
        }

        private static void HandleAddResizeRectangleAdorner(ControlContainer element, AdornerLayer layer)
        {
            RectangleDeck adorner = new RectangleDeck(element);
            layer.Add(adorner);
        }

        private static void HandleRemoveResizeRectangleAdorner(ControlContainer element, AdornerLayer layer)
        {
            if (layer == null) return;

            System.Windows.Documents.Adorner[] adorners = layer.GetAdorners(element);
            foreach (System.Windows.Documents.Adorner adorner in adorners)
            {
                layer.Remove(adorner);
            }
        }

        private static void HandleAddResizeLineAdorner(ControlContainer element, AdornerLayer layer)
        {
            //LineAdorner adorner = new LineAdorner(element);
            //layer.Add(adorner);
        }

        private static void HandleRemoveResizeLineAdorner(ControlContainer element, AdornerLayer layer)
        {
            if (layer == null) return;

            System.Windows.Documents.Adorner[] adorners = layer.GetAdorners(element);
            foreach (System.Windows.Documents.Adorner adorner in adorners)
            {
                layer.Remove(adorner);
            }
        }

        // Canvas
        public static readonly DependencyProperty ShowCanvasResizeAdornerProperty =
           DependencyProperty.RegisterAttached(
               "ShowCanvasResizeAdorner",
               typeof(bool),
               typeof(AttachedDeck),
               new FrameworkPropertyMetadata(false, OnShowCanvasResizeAdornerChanged)
           );

        public static void SetShowCanvasResizeAdorner(UIElement element, bool value)
            => element.SetValue(ShowCanvasResizeAdornerProperty, value);

        public static bool GetShowCanvasResizeAdorner(UIElement element)
            => (bool)element.GetValue(ShowCanvasResizeAdornerProperty);

        private static void OnShowCanvasResizeAdornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ControlCanvas canvas = (ControlCanvas)d;

            if (canvas == null) return;

            bool value = (bool)e.NewValue;
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(canvas);

            if (value)
            {
                CanvasDeck adorner = new CanvasDeck(canvas);
                layer.Add(adorner);
            }
        }
    }
}

