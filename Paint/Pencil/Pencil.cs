using System;
using System.Text;
using System.Windows;
using System.Windows.Media;
using MyContract;



namespace Pencil
{
    public class Pencil : IShape
    {
        private const char minor_separator_1 = '!';
        private const char minor_separator_2 = ';';
        public Point Start { get; set; }
        public Point End { get; set; }
        public string Name => "Pencil";
        public Color ShapeColor { get; set; } = Colors.Transparent;
        public int Thickness { get; set; } = -1;
        public DoubleCollection Stroke { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void UpdateEnd(Point p)
        {
            End = p; ;
        }

        public void UpdateStart(Point p)
        {
            Start= p;
        }

        public UIElement Draw(System.Windows.Media.Color color, int thickness, DoubleCollection stroke, string source, string content)
        {
            return null;
        }

        public UIElement Draw(Color color, int thickness, DoubleCollection stroke, string source)
        {
            throw new NotImplementedException();
        }

        public string FromShapeToString()
        {
            string constructed_string = "";

            //storage structure: <Type>:<ShapeColor>;<Thickness>;<Start>;<End>;<Stroke>|....
            //                      0  :     1      ;      2    ;   3   ;  4  ;    5   ;     6
            constructed_string = new StringBuilder().Append(Name).Append(minor_separator_1).Append(ShapeColor.ToString()).
                Append(minor_separator_2).Append(Thickness).Append(minor_separator_2).Append(Start).
                Append(minor_separator_2).Append(End).Append(minor_separator_2).Append("None").ToString();

            return constructed_string;
        }

        public IShape FromStringToShape(string constructed_str)
        {
            if (constructed_str == null)
            {
                throw new ArgumentNullException("Constructed string is null");
            }

            string[] details = constructed_str.Split(new char[] { minor_separator_1, minor_separator_2 });
            Pencil shape = new Pencil();
            shape.ShapeColor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(details[1]);
            shape.Thickness = Convert.ToInt32(details[2]);
            shape.Start = System.Windows.Point.Parse(details[3]);
            shape.End = System.Windows.Point.Parse(details[4]);
            /*shape.Stroke = DoubleCollection.Parse(details[5]);*/

            return shape;
        }
    }
}
