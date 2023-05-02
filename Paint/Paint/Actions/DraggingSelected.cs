using System.Windows.Input;
using System.Windows;
using Paint.CustomControl;
using MyContract;

namespace Paint.Actions
{
    internal static class DraggingSelected
    {
        public delegate void DragDeltaEventHandler(ControlContainer sender, Point point);
        public static event DragDeltaEventHandler DragDelta;

        public static ControlItem Context { get; set; }

        public static bool IsPossibleDragging { get; private set; }
        public static DependencyProperty IsPossibleDraggingProperty = DependencyProperty.RegisterAttached
            (
                "IsPossibleDragging",
                typeof(bool),
                typeof(ControlContainer),
                new PropertyMetadata(OnIsPossibleDraggingChanged)
            );

        public static void SetIsPossibleDragging(UIElement element, bool value) => element.SetValue(IsPossibleDraggingProperty, value);
        public static bool GetIsPossibleDragging(UIElement element) => (bool)element.GetValue(IsPossibleDraggingProperty);

        private static Point _initialPosition;

        private static void OnIsPossibleDraggingChanged(ControlContainer d, DependencyPropertyChangedEventArgs e)
        {
            ControlContainer container = (ControlContainer)d;

            if (container != null)
            {
                bool IsPossibleDragging = (bool)e.NewValue;

                if (IsPossibleDragging)
                {
                    container.MouseDown += OnSelectedContainerClicked;
                    container.MouseMove += OnSelectedContainerMove;
                    container.MouseUp += OnSelectedContainerReleased;
                }
                else
                {
                    container.MouseDown -= OnSelectedContainerClicked;
                    container.MouseMove -= OnSelectedContainerMove;
                    container.MouseUp -= OnSelectedContainerReleased;
                }
            }
        }

        private static void OnSelectedContainerClicked(object sender, MouseButtonEventArgs e)
        {
            _initialPosition = e.GetPosition(Context.ControlCanvas);
            var container = (ControlContainer)sender;
            container.CaptureMouse();
        }

        private static void OnSelectedContainerMove(object sender, MouseEventArgs e)
        {
            var container = (ControlContainer)sender;
            var nodeVM = (IShape)container.DataContext;

            if (container.IsMouseCaptured && e.LeftButton == MouseButtonState.Pressed)
            {
                //nodeVM.IsCommitChanged = false;

                Point currentPosition = e.GetPosition(Context.ControlCanvas);

                if (_initialPosition == currentPosition) return;

                DragDelta?.Invoke((ControlContainer)sender, new Point(currentPosition.X - _initialPosition.X, currentPosition.Y - _initialPosition.Y));
                _initialPosition = currentPosition;
            }
        }

        private static void OnSelectedContainerReleased(object sender, MouseButtonEventArgs e)
        {
            var container = (ControlContainer)sender;
            var nodeVM = (IShape)container.DataContext;

            container.ReleaseMouseCapture();
            IsPossibleDragging = false;
            //nodeVM.IsCommitChanged = true;
        }
    }
}
