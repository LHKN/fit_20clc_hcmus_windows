using System;
using System.Windows;
using System.Windows.Media;

namespace MyContract
{
    public interface IShape: ICloneable
    {
        string Name { get; }

        void UpdateStart(System.Windows.Point p);
        void UpdateEnd(System.Windows.Point p);
        UIElement Draw(System.Windows.Media.Color color, int thickness, DoubleCollection stroke);

    }
}
