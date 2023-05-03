using System;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using MyContract;
using System.Windows.Documents;

namespace TextInputAbility
{
    public class MyText: IShape
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public string Name => "Text";
        

        public Color ShapeColor { get; set; } = Colors.Transparent;
        public int Thickness { get; set; } = -1;
        private DoubleCollection? Stroke;
        private string textContent = "";
        private FontFamily fontFamily = null;
        private int fontSize = 0;
        bool isBold = false;
        bool isItalic = false;
        bool isUnderline = false;

        

        public void UpdateStart(Point p)
        {
            Start = p;
        }
        public void UpdateEnd(Point p)
        {
            End = p;
        }
        public void UpdateFontFamily(FontFamily ff)
        {
            fontFamily = ff;
        }
        public void UpdateFontSize(int size)
        {
            fontSize = size;
        }
        public void UpdateBold(bool bold)
        {
            isBold = bold;
        }
        public void UpdateItalic(bool italic)
        {
            isItalic= italic;
        }
        public void UpdateUnderline(bool underline)
        {
            isUnderline= underline;
        }

        public void SetContent(string text)
        {
            textContent = text;
        }

        public UIElement Draw(Color color, int thickness, DoubleCollection stroke, string source, string content)
        {
            //handle color and thickness of redraw shape, only assign once
            if (ShapeColor == Colors.Transparent) { ShapeColor = color; }
            if (Thickness == -1) { Thickness = thickness; }
            if (Stroke == null) { Stroke = stroke; }

            double width = Math.Abs(End.X - Start.X);
            double height = Math.Abs(End.Y - Start.Y);
            double left = Math.Min(Start.X, End.X); // Use Math.Min to determine the left position
            double top = Math.Min(Start.Y, End.Y); // Use Math.Min to determine the top position

            TextBlock shape = new TextBlock();
            
            shape.Width = width;
            shape.Height = height;
            shape.Text = textContent;
            if (fontFamily != null)
            {
                shape.FontFamily = fontFamily;
            }
            
            if (fontSize > 0)
            {
                shape.FontSize = fontSize;
            }
            
            if (isBold)
            {
                shape.FontWeight = FontWeights.Bold;
            }
            
            if (isItalic)
            {
                shape.FontStyle = FontStyles.Italic;
            }
            
            if (isUnderline)
            {
                shape.TextDecorations = TextDecorations.Underline;
            }
            


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