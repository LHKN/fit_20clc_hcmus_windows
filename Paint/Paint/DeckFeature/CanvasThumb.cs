using Paint.CustomControl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;

namespace Paint.DeckFeature
{
    class CanvasThumb : System.Windows.Controls.Primitives.Thumb
    {
        private ControlCanvas _canvas { get; set; }

        public CanvasThumb()
        {
            DragStarted += ResizeThumb_DragStarted;
            DragDelta += ResizeThumb_DragDelta;
        }

        private void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            _canvas = (ControlCanvas)DataContext;
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (_canvas == null)
                return;

            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    UpdateBottomResize(e);
                    break;

                default:
                    break;
            }

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Right:
                    UpdateRightResize(e);
                    break;
                default:
                    break;
            }
        }

        private void UpdateBottomResize(DragDeltaEventArgs e)
        {
            Debug.WriteLine("dfjhdfhdfjhdf43434");
            double deltaVertical = Math.Min(-e.VerticalChange, _canvas.ActualHeight);
            _canvas.Height -= deltaVertical;
        }

        private void UpdateRightResize(DragDeltaEventArgs e)
        {
            double deltaHorizontal = Math.Min(-e.HorizontalChange, _canvas.ActualHeight);
            _canvas.Width -= deltaHorizontal;
        }
    }
}

