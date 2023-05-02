using MyContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint
{
    public class MyImage
    {
        public string Name => "Image";
        public string ImageSource { get; set; }
        Point Start { get; set; }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public UIElement Draw(Color color, int thickness, DoubleCollection stroke)
        {
            throw new NotImplementedException();
        }

        public void UpdateEnd(Point p)
        {
            throw new NotImplementedException();
        }

        public void UpdateStart(Point p)
        {
            throw new NotImplementedException();
        }
    }
}
