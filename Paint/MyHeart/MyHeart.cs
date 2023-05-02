using MyContract;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace MyHeart
{
    public class MyHeart : IShape
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
        public string Name => "Heart";

        Color IShape.ShapeColor => ShapeColor;
        int IShape.Thickness => Thickness;
        DoubleCollection? IShape.Stroke => Stroke;

        public Color ShapeColor = Colors.Transparent;
        public int Thickness = -1;
        private DoubleCollection? Stroke;

        public object Clone()
        {
            return MemberwiseClone();
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


            // Create a Path object to represent the heart shape
            var path = new Path()
            {
                Stroke = new SolidColorBrush(ShapeColor),
                StrokeThickness = Thickness,
                StrokeDashArray = Stroke
            };

            var geometry = new PathGeometry();

            // Iterate over t from 0 to 2*pi with a small step
            for (double t = 0; t <= 2 * Math.PI; t += 0.01)
            {
                double x = 17 * Math.Pow(Math.Sin(t), 3);
                double y = 15 * Math.Cos(t) - 5 * Math.Cos(2 * t) - 2 * Math.Cos(3 * t) - Math.Cos(4 * t);

                double screenX = width / 2 + x * (width / 40);
                double screenY = height / 2 - y * (height / 40);

                var point = new Point(screenX, screenY);

                if (t == 0)
                {
                    // Move to the first point
                    geometry.Figures.Add(new PathFigure(point, new List<PathSegment>(), false));
                }
                else
                {
                    // Draw a line to the current point
                    geometry.Figures[0].Segments.Add(new LineSegment(point, true));
                }
            }

            path.Data = geometry;

            // Set the Canvas.Left and Canvas.Top attached properties to position the heart shape
            Canvas.SetLeft(path, left);
            Canvas.SetTop(path, top);

            return path;
        }

        public void UpdateEnd(Point p)
        {
            End = p;
        }

        public void UpdateStart(Point p)
        {
            Start = p;
        }
    }
}
