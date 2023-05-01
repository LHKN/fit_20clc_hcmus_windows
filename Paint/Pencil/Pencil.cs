using System;
using System.Windows;
using System.Windows.Media;
using MyContract;



namespace Pencil
{
    public class Pencil : IShape
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public string Name => "Pencil";
        public Color ShapeColor { get; set; } = Colors.Transparent;
        public int Thickness { get; set; } = -1;

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement Draw(Color color, int thickness)
        {


            return null;
        }

        public void UpdateEnd(Point p)
        {
            End = p; ;
        }

        public void UpdateStart(Point p)
        {
            Start= p;
        }
    }
}
