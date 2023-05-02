using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace MyContract
{
    public interface IShape: ICloneable
    {
        double Top { get; set; }
        double Left { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        int ZIndex { get; set;  }
        double RotateAngle { get;}
        Point TransformOrigin { get; set;  } 
       
        public bool IsSelected { get; set; }
        public bool IsDrawing { get; set; }
        public bool IsCommitChanged { get; set; }

        string Name { get; }
        Point Start { get; }
        Point End { get; }
        Color ShapeColor { get; }
        int Thickness { get; }
        DoubleCollection? Stroke { get; }

        void UpdateStart(System.Windows.Point p);
        void UpdateEnd(System.Windows.Point p);
        UIElement Draw(System.Windows.Media.Color color, int thickness, DoubleCollection stroke);

    }
}
