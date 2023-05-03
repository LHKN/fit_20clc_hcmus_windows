using System;
using System.Windows;
using System.Windows.Media;

namespace MyContract
{
    public interface IShape: ICloneable
    {
        Point Start { get; set; }
        Point End { get; set; }
        string Name { get; }

        Color ShapeColor { get; set; }

        int Thickness { get; set; }

        void UpdateStart(System.Windows.Point p);
        void UpdateEnd(System.Windows.Point p);
        UIElement Draw(System.Windows.Media.Color color, int thickness, DoubleCollection stroke, string source, string content);

    }
}
