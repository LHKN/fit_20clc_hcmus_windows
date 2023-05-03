using System;
using System.Windows.Media;
using System.Windows;
using MyContract;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace ImageAbility
{
    public class MyImage : IShape
    {
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
    }
}
