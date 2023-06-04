using MyContract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace MyHeart
{
    public class MyHeart : IShape
    {

        private const char minor_separator_1 = '!';
        private const char minor_separator_2 = ';';
        public Point Start { get; set; }
        public Point End { get; set; }
        public string Name => "Heart";
        public Color ShapeColor { get; set; } = Colors.Transparent;
        public int Thickness { get; set; } = -1;
        public DoubleCollection? Stroke { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement Draw(Color color, int thickness, DoubleCollection stroke, string source)
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

        public string FromShapeToString()
        {
            string constructed_string = "";

            //storage structure: <Type>:<ShapeColor>;<Thickness>;<Start>;<End>;<Stroke>|....
            //                      0  :     1      ;      2    ;   3   ;  4  ;    5   ;     6
            constructed_string = new StringBuilder().Append(Name).Append(minor_separator_1).Append(ShapeColor.ToString()).
                Append(minor_separator_2).Append(Thickness).Append(minor_separator_2).Append(Start).
                Append(minor_separator_2).Append(End).Append(minor_separator_2).Append(Stroke!.ToString()).ToString();

            return constructed_string;
        }

        public IShape FromStringToShape(string constructed_str)
        {
            if (constructed_str == null)
            {
                throw new ArgumentNullException("Constructed string is null");
            }

            string[] details = constructed_str.Split(new char[] { minor_separator_1, minor_separator_2 });
            MyHeart shape = new MyHeart();
            shape.ShapeColor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(details[1]);
            shape.Thickness = Convert.ToInt32(details[2]);
            shape.Start = System.Windows.Point.Parse(details[3]);
            shape.End = System.Windows.Point.Parse(details[4]);
            shape.Stroke = DoubleCollection.Parse(details[5]);

            return shape;
        }
    }
}
