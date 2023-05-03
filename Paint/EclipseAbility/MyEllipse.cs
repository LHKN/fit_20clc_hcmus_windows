using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using MyContract;
using System.Text;

namespace EllipseAbility
{
    public class MyEllipse : IShape
    {
        private const char minor_separator_1 = '!';
        private const char minor_separator_2 = ';';
        public Point Start { get; set; }
        public Point End { get; set; }

        public string Name => "Ellipse";

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
            MyEllipse shape = new MyEllipse();
            shape.ShapeColor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(details[1]);
            shape.Thickness = Convert.ToInt32(details[2]);
            shape.Start = System.Windows.Point.Parse(details[3]);
            shape.End = System.Windows.Point.Parse(details[4]);
            shape.Stroke = DoubleCollection.Parse(details[5]);

            return shape;
        }

    }
}
