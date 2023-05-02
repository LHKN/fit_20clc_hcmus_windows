using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using Paint.CustomControl;
using MyContract;

namespace Paint.DeckFeature
{
    class EndPointThumb : System.Windows.Controls.Primitives.Thumb
    {
        private ControlContainer container { get; set; }
        private IShape nodeVM { get; set; }

        public EndPointThumb()
        {
            DragStarted += EndPointThumb_DragStarted;
            DragDelta += EndPointThumb_DragDelta;

            Style = (Style)FindResource("EndPointThumb");
        }

        private void EndPointThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            container = (ControlContainer)DataContext;
            nodeVM = (IShape)container.DataContext;
        }

        private void EndPointThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (nodeVM != null)
            {
                //nodeVM.X2 += e.HorizontalChange;
                //nodeVM.Y2 += e.VerticalChange;
            }
        }
    }
}
