using MyContract;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Paint.Actions;


namespace Paint.CustomControl
{
    /// <summary>
    /// Interaction logic for ControlItem.xaml
    /// </summary>
    public class ControlItem : ItemsControl
    {
        private Selecting _selectingGesture { get; set; }
        private Actions.Drawing _drawingGesture;

        public ControlCanvas DesignCanvas { get; set; }

        public bool IsDrawing { get; set; }

        static ControlItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ControlItem), new FrameworkPropertyMetadata(typeof(ControlItem)));
        }

        public ControlItem()
        {
            DraggingSelected.Context = this;
            _selectingGesture = new Selecting(this);
            _drawingGesture = new Actions.Drawing(this);

            //DraggingSelected.DragDelta += HandleDragDelta;
        }

        // Curent Node for drawing
        public static readonly DependencyProperty DrawingNodeProperty =
            DependencyProperty.Register(
                "DrawingNode",
                typeof(IShape),
                typeof(ControlItem),
                new UIPropertyMetadata(null)
       );

        public IShape DrawingNode
        {
            get { return (IShape)GetValue(DrawingNodeProperty); }
            set { SetValue(DrawingNodeProperty, value); }
        }

        // Selection Rectngle
        public static readonly DependencyProperty SelectionRectangleProperty =
             DependencyProperty.Register(
                 "SelectionRectangle",
                 typeof(Rect),
                 typeof(ControlItem),
                 new UIPropertyMetadata(null)
        );

        public Rect SelectionRectangle
        {
            get { return (Rect)GetValue(SelectionRectangleProperty); }
            set { SetValue(SelectionRectangleProperty, value); }
        }

        // Is any item is dragging
        public static readonly DependencyProperty IsDraggingProperty =
            DependencyProperty.Register(
                "IsDragging",
                typeof(bool),
                typeof(ControlItem),
                new UIPropertyMetadata(false)
       );

        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            set { SetValue(IsDraggingProperty, value); }
        }

        // Selecting
        public static readonly DependencyProperty IsAreaSelectingProperty =
             DependencyProperty.Register(
                 "IsAreaSelecting",
                 typeof(bool),
                 typeof(ControlItem),
                 new UIPropertyMetadata(false)
        );

        public bool IsAreaSelecting
        {
            get { return (bool)GetValue(IsAreaSelectingProperty); }
            set { SetValue(IsAreaSelectingProperty, value); }
        }

        // Selected Items
        public static readonly DependencyProperty SelectedItemsProperty =
             DependencyProperty.Register(
                 "SelectedItems",
                 typeof(ObservableCollection<IShape>),
                 typeof(ControlItem),
                 new UIPropertyMetadata(new ObservableCollection<IShape>())
        );

        public ObservableCollection<IShape> SelectedItems
        {
            get { return (ObservableCollection<IShape>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ControlItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ControlItem
            {
                RenderTransform = new TransformGroup
                {
                    Children = new TransformCollection(
                        new Transform[]
                        {
                            new ScaleTransform(),
                            new TranslateTransform()
                        })
                }
            };
        }


        private void HandleDragDelta(ControlItem sender, Point point)
        {
            IsDragging = true;

            foreach (var item in SelectedItems)
            {
                item.Left += point.X;
                item.Top += point.Y;
            }
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            bool isSelectByOnePoint = false;

            if (DrawingNode != null)
            {
                IsDrawing = true;
                DrawingNode.IsDrawing = true;
                _drawingGesture.OnMouseDown(e.GetPosition(this));
            }

            if (!IsDrawing && !IsDragging && !IsAreaSelecting)
            {
                isSelectByOnePoint = _selectingGesture.SelectByMouseDown(e);
            }

            if (!IsDrawing && !isSelectByOnePoint)
            {
                IsAreaSelecting = true;
                _selectingGesture.OnMouseDownStartAreaSelect(e.GetPosition(this));
            }
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            _selectingGesture.SelectByMouseDoubleClick(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            if (IsDrawing)
            {
                _drawingGesture.OnMouseMove(e.GetPosition(this));
            }

            if (IsAreaSelecting && !IsDragging)
            {
                _selectingGesture.OnMouseMoveAreaSelecting(e.GetPosition(this));
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (!IsDragging)
            {
                _selectingGesture.SelectByMouseUp(e);
            }

            if (IsDrawing)
            {
                _drawingGesture.OnMouseUp(e.GetPosition(this));
                IsDrawing = false;
            }

            if (IsAreaSelecting)
            {
                _selectingGesture.OnMouseUpEndSelecting(e.GetPosition(this));
                IsAreaSelecting = false;
            }

            IsDrawing = false;
            IsDragging = false;
            IsAreaSelecting = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //DesignCanvas = (ControlCanvas)GetTemplateChild("PART_Canvas");
        }

        private void EventAnalyzeOnButtonDown(MouseButtonEventArgs e)
        {
            //if (DrawingNode != null)
            //{
            //    IsDrawing = true;
            //    DrawingNode.IsDrawing = true;
            //}
        }
    }
}