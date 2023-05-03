using System;
using System.Windows.Media;
using System.Windows;
using MyContract;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Text;

namespace ImageAbility
{
    public class MyImage : IShape
    {
        private const char minor_separator_1 = '!';
        private const char minor_separator_2 = ';';
        public Point Start { get; set; }
        public Point End { get; set; }
        public string Name => "Image";
        public string ImageSource { get; set; }
        public Color ShapeColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Thickness { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DoubleCollection? Stroke { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public UIElement Draw(Color color, int thickness, DoubleCollection stroke, string source)
        {
            if (ImageSource == null) { ImageSource = source; }

            double left = Math.Min(Start.X, End.X); // Use Math.Min to determine the left position
            double top = Math.Min(Start.Y, End.Y); // Use Math.Min to determine the top position

            var shape = new Image
            {
                Width = Math.Abs(End.X - Start.X),
                Height = Math.Abs(End.Y - Start.Y),
                Source = new BitmapImage(new Uri(ImageSource)),
            };

            Canvas.SetLeft(shape, left);
            Canvas.SetTop(shape, top);
            return shape;
        }

        public object Clone()
        {
            return MemberwiseClone();
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

            //storage structure: <Type>:<ShapeColor>;<Thickness>;<Start>;<End>;<Stroke>;<ImageSource>....
            //                      0  :     1      ;      2    ;   3   ;  4  ;    5   ;     6
            constructed_string = new StringBuilder().Append(Name).Append(minor_separator_1).Append("None").
                Append(minor_separator_2).Append("None").Append(minor_separator_2).Append(Start).
                Append(minor_separator_2).Append(End).Append(minor_separator_2).Append("None").Append(minor_separator_2).Append(ImageSource).ToString();


            return constructed_string;
        }

        public IShape FromStringToShape(string constructed_str)
        {
            if(constructed_str == null)
            {
                throw new ArgumentNullException("Constructed string is null");
            }

            string[] details = constructed_str.Split(new char[] {minor_separator_1, minor_separator_2});
            MyImage image = new MyImage();
            image.Start = System.Windows.Point.Parse(details[3]);
            image.End = System.Windows.Point.Parse(details[4]);
            image.ImageSource = details[6];

            return image;
        }
    }
}
