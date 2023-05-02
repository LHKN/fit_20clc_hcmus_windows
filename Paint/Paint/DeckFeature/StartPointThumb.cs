using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using Paint.CustomControl;
using MyContract;

namespace Paint.DeckFeature
{
    class StartPointThumb : System.Windows.Controls.Primitives.Thumb
    {
        private ControlContainer container { get; set; }
        private IShape nodeVM { get; set; }

        public StartPointThumb()
        {
            DragStarted += LineStartPointThumb_DragStarted;
            DragDelta += LineStartPointThumb_DragDelta;

            Style = (Style)FindResource("ResizeThumbStyle");
        }

        private void LineStartPointThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            container = (ControlContainer)DataContext;
            nodeVM = (IShape)container.DataContext;
        }

        private void LineStartPointThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (nodeVM != null)
            {
                //nodeVM.X1 += e.HorizontalChange;
                //nodeVM.Y1 += e.VerticalChange;
            }
        }
    }
}
