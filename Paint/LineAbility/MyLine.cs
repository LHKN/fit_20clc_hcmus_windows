using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using MyContract;

namespace LineAbility
{
    public class MyLine : IShape
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public string Name => "Line";

        public Color ShapeColor { get; set; } = Colors.Transparent;
        public int Thickness { get; set; } = -1;
        public DoubleCollection? Stroke { get; set; }

        public void UpdateStart(Point p)
        {
            Start = p;
        }
        public void UpdateEnd(Point p)
        {
            End = p;
        }

        public UIElement Draw(Color color, int thickness, DoubleCollection stroke, string source)
        {
            //handle color and thickness of redraw shape, only assign once
            if (ShapeColor == Colors.Transparent) { ShapeColor = color; }
            if (Thickness == -1) { Thickness = thickness; }
            if (Stroke == null) { Stroke = stroke; }

            return new Line()
            {
                X1 = Start.X,
                Y1 = Start.Y,
                X2 = End.X,
                Y2 = End.Y,
                Stroke = new SolidColorBrush(ShapeColor),
                StrokeThickness = Thickness,
                StrokeDashArray = Stroke
            };
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
