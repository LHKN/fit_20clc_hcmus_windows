using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using MyContract;
using System.Windows.Shapes;
using System.Windows.Ink;

namespace RectangleAbility
{
    public class MyRectangle : IShape
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public string Name => "Rectangle";
        public Color ShapeColor = Colors.Transparent;
        public int Thickness = -1;
        private DoubleCollection? Stroke;

        public void UpdateStart(Point p)
        {
            Start = p;
        }
        public void UpdateEnd(Point p)
        {
            End = p;
        }

        public UIElement Draw(Color color, int thickness, DoubleCollection stroke)
        {
            //handle color and thickness of redraw shape, only assign once
            if (ShapeColor == Colors.Transparent) { ShapeColor = color; }
            if (Thickness == -1) { Thickness = thickness; }
            if (Stroke == null) { Stroke = stroke; }

            double width = Math.Abs(End.X - Start.X);
            double height = Math.Abs(End.Y - Start.Y);
            double left = Math.Min(Start.X, End.X); // Use Math.Min to determine the left position
            double top = Math.Min(Start.Y, End.Y); // Use Math.Min to determine the top position

            var shape = new Rectangle()
            {
                Width = width,
                Height = height,
                Stroke = new SolidColorBrush(ShapeColor),
                StrokeThickness = Thickness,
                StrokeDashArray = Stroke
            };

            Canvas.SetLeft(shape, left);
            Canvas.SetTop(shape, top);
            return shape;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
