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
    public class MyText
    {
        public string Name => "Text";

        public string Content { get; set; }
        public bool IsFocusable { get; set; }

        public FontFamily FontFamily { get; set; }
        public int FontSize { get; set; }
        public FontWeight FontWeight { get; set; }
        public FontStyle FontStyle { get; set; }
        public TextDecorationCollection TextDecorations { get; set; }
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
