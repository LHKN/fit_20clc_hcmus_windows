using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using MyContract;

namespace EllipseAbility
{
    public class MyEllipse : IShape
    {
        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int ZIndex { get; set; }
        public double RotateAngle { get; set; }
        public Point TransformOrigin { get; set; }

        public bool IsSelected { get; set; }
        public bool IsDrawing { get; set; }
        public bool IsCommitChanged { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public string Name => "Ellipse";

        Color IShape.ShapeColor => ShapeColor;
        int IShape.Thickness => Thickness;
        DoubleCollection? IShape.Stroke => Stroke;

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

            var shape = new Ellipse()
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
