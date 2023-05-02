using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using MyContract;

namespace LineAbility
{
    public class MyLine : IShape
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

        public string Name => "Line";

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
